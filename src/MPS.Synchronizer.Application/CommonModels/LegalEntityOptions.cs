using System.ComponentModel.DataAnnotations;

namespace MPS.Synchronizer.Application.CommonModels;

public class LegalEntityOptions
{
    [Required(AllowEmptyStrings = false)]
    public string Token { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string Name { get; set; }

    [Required]
    public LegalEntityStatisticsOptions Statistics { get; set; }

    //[Required]
    //public LegalEntityStatisticsOptions Adverts { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var validationResults = new List<ValidationResult>();
        Validator.TryValidateObject(Statistics, new ValidationContext(Statistics), validationResults);
        return validationResults;
    }
}