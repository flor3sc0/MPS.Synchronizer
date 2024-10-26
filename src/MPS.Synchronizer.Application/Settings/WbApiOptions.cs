using System.ComponentModel.DataAnnotations;

namespace MPS.Synchronizer.Application.Settings;

public class WbApiOptions
{
    [Required(AllowEmptyStrings = false)]
    public string Statistics { get; set; }
}