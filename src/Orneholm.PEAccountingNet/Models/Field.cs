using Orneholm.PEAccountingNet.Models.Native;

namespace Orneholm.PEAccountingNet.Models
{
    public class Field
    {
        /// <summary>
        /// Set when reading.
        /// Ignored when creating/updating
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Alias uniquely identifies a field.
        /// </summary>
        public string Alias { get; set; }
        public string Value { get; set; }

        internal static Field FromNative(field native)
        {
            return new Field
            {
                Name = native.name,
                Alias = native.alias,
                Value = native.value
            };
        }

        internal field ToNative()
        {
            return new field
            {
                name = Name,
                alias = Alias,
                value = Value
            };
        }
    }
}