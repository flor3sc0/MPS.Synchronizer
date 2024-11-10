using System.ComponentModel.DataAnnotations;

namespace MPS.Synchronizer.TelegramBot.CommonModels;

public class TelegramBotOptions
{
    public const string TelegramBot = "TelegramBot";

    public long ChatId { get; set; }

    public string Token { get; set; }

    public bool IsEnable => ChatId != default && !string.IsNullOrWhiteSpace(Token);
}