namespace MPS.Synchronizer.Domain.Common;

/// <summary>
/// При наличии на свойстве модели игнорируется создание индекса на поле
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class SkipIndexGenerationAttribute : Attribute;