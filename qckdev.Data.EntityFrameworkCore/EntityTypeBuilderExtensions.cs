using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microsoft.EntityFrameworkCore
{

    /// <summary>
    /// Relational database specific extension methods for <see cref="Microsoft.EntityFrameworkCore.Metadata.Builders.PropertyBuilder"/>.
    /// </summary>
    public static class EntityTypeBuilderExtensions
    {

        /// <summary>
        /// Configures the <see cref="string.TrimEnd(char[])"/> conversion for this property.
        /// </summary>
        /// <param name="builder">The <see cref="PropertyBuilder"/></param>
        /// <returns>The same builder instance so that multiple configuration calls can be chained.</returns>
        public static PropertyBuilder<string> TrimEnd(this PropertyBuilder<string> builder)
        {
            return builder.HasConversion(str => str, str => str.TrimEnd());
        }

    }
}
