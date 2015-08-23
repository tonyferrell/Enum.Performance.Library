/*
 * The MIT License (MIT)
 *
 * Copyright (c) 2015 Tony Ferrell
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of
 * this software and associated documentation files (the "Software"), to deal in
 * the Software without restriction, including without limitation the rights to
 * use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
 * the Software, and to permit persons to whom the Software is furnished to do so,
 * subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
 * FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
 * COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
 * IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
 * CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */
namespace Enum.Performance.Library
{

    using System;
    using System.Collections.Generic;

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
            foreach (TEnum enumValue in values)
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
              out Dictionary<string, TEnum> result
#if !NET20
            , Func<string, string> normalizer = null
#endif
            )
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
            foreach (TEnum enumValue in values)
            {
                string enumText = enumValue.ToString();
#if !NET20
                string key = normalizer == null ? enumText : normalizer(enumText);
#else
                string key = enumText;
#endif

                result[key] = enumValue;
            }

            return true;
        }
    }

}
