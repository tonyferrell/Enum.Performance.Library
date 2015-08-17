using System;
using System.Collections.Generic;

namespace Enum.Performance.Library
{
    public static class EnumPerformanceParser
    {
        /// <summary>
        /// Creates a mapping from a TEnum to TEnum.ToString().
        /// </summary>
        /// <remarks>
        /// The underlying methods used in this function have a high performance cost. When keeping performance in mind
        /// you should call this function once and cache the result
        /// </remarks>
        /// <typeparam name="TEnum">An enumerable type to create the mapping for.</typeparam>
        /// <param name="result">Populated mapping from TEnum value to their string representations.</param>
        /// <returns>True if the given type is an Enum, False otherwise.</returns>
        public static bool TryCreateEnumToNameMap<TEnum>(out Dictionary<TEnum, string> result)
            where TEnum : struct
        {
            var inputType = typeof(TEnum);

            if (!inputType.IsEnum)
            {
                result = null;
                return false;
            }

            result = new Dictionary<TEnum, string>();

            TEnum[] values = (TEnum[])System.Enum.GetValues(inputType);
            foreach(TEnum enumValue in values)
            {
                result[enumValue] = enumValue.ToString();
            }

            return true;
        }

        /// <summary>
        /// Tries to create a map from a normalized(TEnum.ToString()) to its value.
        /// Any duplicates under normalization will be overwritten with the later value.
        /// 
        /// Example:
        ///   TEnum: enum MyOneEnum { One, one }
        ///   normalizer: x => x.ToUpperInvariant();
        ///  
        ///   This will result in the dictionary { { "ONE", MyOneEnum.one } }.
        /// 
        /// To avoid this, you should ensure that your enum values are unique after normalization (or avoid it).
        /// </summary>
        /// <remarks>
        /// The underlying methods used in this function have a high performance cost. When keeping performance in mind
        /// you should call this function once and cache the result
        /// </remarks>
        /// <typeparam name="TEnum">An enumerable type to create the mapping for.</typeparam>
        /// <param name="result">Populated mapping from normalized name to TEnum value </param>
        /// <param name="normalizer">Funct[ion to normalize your input, based on your parsing constraints. Default is no normalization.</param>
        /// <returns>True if the map is created. False if TEnum is not an enum.</returns>
        public static bool TryCreateNameToEnumMap<TEnum>(
            out Dictionary<string, TEnum> result,
            Func<string, string> normalizer = null)
            where TEnum : struct
        {
            var inputType = typeof(TEnum);

            if (!inputType.IsEnum)
            {
                result = null;
                return false;
            }

            result = new Dictionary<string, TEnum>();

            TEnum[] values = (TEnum[])System.Enum.GetValues(inputType);
            foreach(TEnum enumValue in values)
            {
                string enumText = enumValue.ToString();
                string key = normalizer == null ? enumText : normalizer(enumText);

                result[key] = enumValue;
            }

            return true;
        }
    }

}
