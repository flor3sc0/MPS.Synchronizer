using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MPS.Synchronizer.Domain.Common;

namespace MPS.Synchronizer.Persistence.Common;

internal static class ConfigurationHelper
{
    private const string IxPref = "IX";

    /// <summary>
    /// автоматическое построение индексов
    /// исключаются поля PK, FK, ранее индексированные, связи отношения, помеченные атрибутом SkipIndexGeneration
    /// </summary>
    /// <param name="builder"></param>
    /// <typeparam name="T"></typeparam>
    internal static void AutoGenerateIndexes<T>(this EntityTypeBuilder<T> builder)
        where T : class
    {
        var properties = builder.Metadata.GetProperties();

        var tableName = builder.Metadata.GetTableName();

        foreach (var property in properties)
        {
            if (property.IsPrimaryKey())
            {
                continue;
            }

            if (property.IsForeignKey())
            {
                continue;
            }

            if (property.IsIndex() || property.IsIndexerProperty())
            {
                continue;
            }

            if (property.PropertyInfo == null || property.PropertyInfo.PropertyType.IsClass)
            {
                continue;
            }

            if (Attribute.IsDefined(property.PropertyInfo, typeof(SkipIndexGenerationAttribute)))
            {
                continue;
            }


            builder.CreateIndex(tableName, property.GetColumnName(), property.PropertyInfo.Name);
        }
    }

    private static void CreateIndex<T>(this EntityTypeBuilder<T> builder, string tableName, string columnName, string propName)
        where T : class
    {
        if (string.IsNullOrEmpty(propName))
        {
            return;
        }
        
        var name = BuildCustomName(IxPref, tableName, columnName);
        builder.HasIndex(new[] { propName }, name);
    }

    private static string BuildCustomName(string prefix, string tableName, string propName)
    {
        if (string.IsNullOrEmpty(propName) || string.IsNullOrEmpty(tableName))
        {
            throw new ArgumentNullException(nameof(BuildCustomName));
        }

        return Template(prefix, tableName, propName);
    }

    private static string Template(string prefix, string tableName, string propName) => $"{prefix}_{tableName}_{propName}";
}
