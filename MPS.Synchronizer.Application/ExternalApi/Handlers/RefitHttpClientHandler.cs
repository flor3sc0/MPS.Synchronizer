using Serilog;

namespace MPS.Synchronizer.Application.ExternalApi.Handlers;

public class RefitHttpClientHandler : HttpClientHandler
{
    protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        try
        {
            return base.Send(request, cancellationToken);
        }
        catch (Exception e)
        {
            Log.Information(e, $"Ошибка при отправка данных {request.RequestUri}");
            throw;
        }
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        try
        {
            return base.SendAsync(request, cancellationToken);
        }
        catch (Exception e)
        {
            Log.Information(e, $"Ошибка при отправка данных {request.RequestUri}");
            throw;
        }
    }
}