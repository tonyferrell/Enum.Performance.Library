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

using System;
using System.Collections.Generic;

namespace Enum.Performance.Library
{
    public static class EnumManager<TEnum> where TEnum : struct
    {
        private static readonly Type enumType = typeof(TEnum);
        private static readonly Dictionary<string, TEnum> stringToEnum;
        private static readonly Dictionary<TEnum, string> enumToString;
        private static readonly bool typeIsEnum;

        static EnumManager()
        {
            typeIsEnum = enumType.IsEnum;
            if (!typeIsEnum)
            {
                stringToEnum = new Dictionary<string, TEnum>(0);
                enumToString = new Dictionary<TEnum, string>(0);
                return;
            }

            TEnum[] values = (TEnum[])System.Enum.GetValues(enumType);

            int length = values.Length;
            stringToEnum = new Dictionary<string, TEnum>(length);
            enumToString = new Dictionary<TEnum, string>(length);

            for(int i = 0; i < length; ++i)
            {
                TEnum enumValue = values[i];
                string enumText = enumValue.ToString();

                stringToEnum[enumText] = enumValue;
                enumToString[enumValue] = enumText;
            }
        }

        public static bool Initalize()
        {
            // For now, calling this just kicks the static constructor
            return enumType.IsEnum;
        }

        public static bool TryParse(string stringValue, out TEnum enumValue)
        {
            return stringToEnum.TryGetValue(stringValue, out enumValue);
        }

        public static bool TryGetString(TEnum enumValue, out string stringValue)
        {
            return enumToString.TryGetValue(enumValue, out stringValue);
        }
    }
}
