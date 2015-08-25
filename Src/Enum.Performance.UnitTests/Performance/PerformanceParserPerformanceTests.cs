// TODO: Move this to a build definition.
#define DO_PERF
#if DO_PERF
using Enum.Performance.Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Enum.Performance.UnitTests.Performance
{
    [TestClass]
    public class PerformanceParserPerformanceTests
    {
        [TestMethod]
        public void PerformanceParser_String_to_Enum_without_Normalization_Performance_Tests()
        {
            const int ITERATIONS = 1000;
            Dictionary<string, LargeEnum> stringToLargeEnumMap;
            for (int i = 0; i < ITERATIONS; ++i)
            {
                EnumPerformanceParser.TryCreateNameToEnumMap<LargeEnum>(out stringToLargeEnumMap);
            }
        }

#if !NET20
        [TestMethod]
        public void PerformanceParser_String_to_Enum_with_Normalization_Performance_Tests()
        {
            Func<string, string> upperCaseNormalizer = x => x.ToUpperInvariant();
            const int ITERATIONS = 1000;
            Dictionary<string, LargeEnum> stringToLargeEnumMap;
            for (int i = 0; i < ITERATIONS; ++i)
            {
                EnumPerformanceParser.TryCreateNameToEnumMap<LargeEnum>(out stringToLargeEnumMap, upperCaseNormalizer);
            }
        }
#endif // !NET20

        [TestMethod]
        public void PerformanceParser_Enum_to_String_Performance_Tests()
        {
            const int ITERATIONS = 1000;
            Dictionary<LargeEnum, string> stringToLargeEnumMap;
            for (int i = 0; i < ITERATIONS; ++i)
            {
                EnumPerformanceParser.TryCreateEnumToNameMap<LargeEnum>(out stringToLargeEnumMap);
            }
        }

    }
}

#endif