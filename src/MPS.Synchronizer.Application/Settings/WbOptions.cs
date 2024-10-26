using System.ComponentModel.DataAnnotations;

namespace MPS.Synchronizer.Application.Settings;

public class WbOptions : IValidatableObject
{
    public const string Wb = "Wb";

    [Required]
    public WbApiOptions Api { get; set; }

    [Required]
    public List<LegalEntityOptions> LegalEntities { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var validationResults = new List<ValidationResult>();
        Validator.TryValidateObject(Api, new ValidationContext(Api), validationResults);
        LegalEntities.ForEach(le => Validator.TryValidateObject(le, new ValidationContext(le), validationResults));
        return validationResults;
    }
}