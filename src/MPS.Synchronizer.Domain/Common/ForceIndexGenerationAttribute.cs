namespace MPS.Synchronizer.Domain.Common;

/// <summary>
/// При наличии на свойстве модели явно указывает создать индекс на поле, если это возможно
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class ForceIndexGenerationAttribute : Attribute;