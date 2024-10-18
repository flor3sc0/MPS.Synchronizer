namespace MPS.Synchronizer.Domain.Common;

/// <summary>
/// При наличии на свойстве модели игнорируется создание индекса на поле
/// Только при CodeFirst подходе
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class SkipIndexGenerationAttribute : Attribute;
