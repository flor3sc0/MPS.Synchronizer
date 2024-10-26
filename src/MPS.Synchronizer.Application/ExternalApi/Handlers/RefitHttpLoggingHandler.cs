using Serilog;
using System.Net.Http.Headers;
using System.Text;

namespace MPS.Synchronizer.Application.ExternalApi.Handlers;

public class RefitHttpLoggingHandler(HttpMessageHandler innerHandler = null)
    : DelegatingHandler(innerHandler ?? new HttpClientHandler())
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var req = request;
        var id = Guid.NewGuid().ToString();
        var msgRequest = $"[{id} -  Request]";
        var outputMessage = new StringBuilder();

        outputMessage.Append(await GetRequestMessage(msgRequest, req, cancellationToken));

        var start = DateTime.Now;

        var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

        var end = DateTime.Now;

        outputMessage.AppendLine($"{msgRequest} Duration: {end - start}");

        outputMessage.Append(await GetResponseMessage(id, response, req, cancellationToken));
        Log.Information(outputMessage.ToString());
        return response;
    }

    private async Task<StringBuilder> GetResponseMessage(
        string id,
        HttpResponseMessage response,
        HttpRequestMessage req,
        CancellationToken cancellationToken)
    {
        var outputMessage = new StringBuilder();
        var msgResponse = $"[{id} - Response]";
        outputMessage.AppendLine($"{msgResponse}=========Start=========");

        var resp = response;

        outputMessage.AppendLine(
            $"{msgResponse} {req.RequestUri?.Scheme.ToUpper()}/{resp.Version} {(int)resp.StatusCode} {resp.ReasonPhrase}");

        foreach (var header in resp.Headers)
        {
            outputMessage.AppendLine($"{msgResponse} {header.Key}: {string.Join(", ", header.Value)}");
        }

        if (resp.Content != null)
        {
            foreach (var header in resp.Content.Headers)
            {
                outputMessage.AppendLine($"{msgResponse} {header.Key}: {string.Join(", ", header.Value)}");
            }

            if (resp.Content is StringContent ||
                IsTextBasedContentType(resp.Headers) ||
                IsTextBasedContentType(resp.Content.Headers))
            {
                var start = DateTime.Now;
                var result = await resp.Content.ReadAsStringAsync(cancellationToken);
                var content = result.Length > 255 ? $"{string.Join(string.Empty, result.Take(255))}..." : result;
                var end = DateTime.Now;

                outputMessage.AppendLine($"{msgResponse} Content:");
                outputMessage.AppendLine($"{msgResponse} {content}");
                outputMessage.AppendLine($"{msgResponse} Duration: {end - start}");
            }
        }

        outputMessage.AppendLine($"{msgResponse}==========End==========");

        return outputMessage;
    }

    private async Task<StringBuilder> GetRequestMessage(
        string msgRequest,
        HttpRequestMessage req,
        CancellationToken cancellationToken)
    {
        var outputMessage = new StringBuilder();
        outputMessage.AppendLine($"{msgRequest}========Start==========");
        outputMessage.AppendLine(
            $"{msgRequest} {req.Method} {req.RequestUri?.PathAndQuery} {req.RequestUri?.Scheme}/{req.Version}");
        outputMessage.AppendLine($"{msgRequest} Host: {req.RequestUri?.Scheme}://{req.RequestUri?.Host}");

        foreach (var header in req.Headers)
        {
            outputMessage.AppendLine($"{msgRequest} {header.Key}: {string.Join(", ", header.Value)}");
        }

        if (req.Content != null)
        {
            foreach (var header in req.Content.Headers)
            {
                outputMessage.AppendLine($"{msgRequest} {header.Key}: {string.Join(", ", header.Value)}");
            }

            if (req.Content is StringContent ||
                IsTextBasedContentType(req.Headers) ||
                IsTextBasedContentType(req.Content.Headers))
            {
                var result = await req.Content.ReadAsStringAsync(cancellationToken);
                var content = result.Length > 255 ? $"{string.Join(string.Empty, result.Take(255))}..." : result;
                outputMessage.AppendLine($"{msgRequest} Content:");
                outputMessage.AppendLine($"{msgRequest} {result}");
            }
        }

        outputMessage.AppendLine($"{msgRequest}==========End==========");

        return outputMessage;
    }

    private readonly string[] _types = ["html", "text", "xml", "json", "txt", "x-www-form-urlencoded"];

    private bool IsTextBasedContentType(HttpHeaders headers)
    {
        if (!headers.TryGetValues("Content-Type", out var values))
        {
            return false;
        }

        var header = string.Join(" ", values).ToLowerInvariant();

        return _types.Any(t => header.Contains(t));
    }
}