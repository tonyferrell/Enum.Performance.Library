using Enum.Performance.Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Enum.Performance.UnitTests.Functional
{
    [TestClass]
    public class PerformanceParserTests
    {

        [TestMethod]
        public void PerformanceParser_Should_Handle_Duplicates_After_Normalization()
        {
            Dictionary<string, SimilarEnum> nameToSimilarEnumMap;

            Assert.IsTrue(
                    EnumPerformanceParser.TryCreateNameToEnumMap(out nameToSimilarEnumMap, x => x.ToUpperInvariant()),
                    "Unable to parse");

            Assert.AreEqual(1, nameToSimilarEnumMap.Count, "More than one value was written to the dictionary");

            Assert.IsTrue(
                nameToSimilarEnumMap.ContainsKey(SimilarEnum.One.ToString().ToUpperInvariant()),
                "Normalization isn't applied properly");
        }

        [TestMethod]
        public void PerformanceParser_Should_Parse_Each_Item_In_String_to_Enum_Map()
        {
            Dictionary<string, SmallEnum> nameToSmallEnumMap;

            Array values = System.Enum.GetValues(typeof(SmallEnum));
            Assert.IsTrue(
                EnumPerformanceParser.TryCreateNameToEnumMap<SmallEnum>(out nameToSmallEnumMap),
                "Unable to parse");

            Assert.AreEqual(values.Length, nameToSmallEnumMap.Count, "Output doesn't match input size");

            for (int i = 0; i < values.Length; ++i)
            {
                SmallEnum value = (SmallEnum)values.GetValue(i);
                string valueText = value.ToString();
                Assert.IsTrue(
                    nameToSmallEnumMap.ContainsKey(valueText),
                    "Value is missing: " + valueText);
            }
        }

        [TestMethod]
        public void PerformanceParser_Should_Parse_Each_Item_In_Enum_to_String_Map()
        {
            Dictionary<LargeEnum, string> nameToLargeEnumMap;

            Array values = System.Enum.GetValues(typeof(LargeEnum));
            Assert.IsTrue(
                EnumPerformanceParser.TryCreateEnumToNameMap<LargeEnum>(out nameToLargeEnumMap),
                "Unable to parse");

            Assert.AreEqual(values.Length, nameToLargeEnumMap.Count, "Output doesn't match input size");

            for (int i = 0; i < values.Length; ++i)
            {
                LargeEnum value = (LargeEnum)values.GetValue(i);
                Assert.IsTrue(
                    nameToLargeEnumMap.ContainsKey(value),
                    "Value is missing: " + value.ToString());
            }
        }

        [TestMethod]
        public void PerformanceParser_Should_Fail_On_Non_Enumeration()
        {
            Dictionary<string, dummyValueType> stringToDummyResult;
            Assert.IsFalse(EnumPerformanceParser.TryCreateNameToEnumMap<dummyValueType>(out stringToDummyResult), "Non-Enum type didn't cause failure.");
            Dictionary<dummyValueType, string> dummyResultToString;
            Assert.IsFalse(EnumPerformanceParser.TryCreateEnumToNameMap<dummyValueType>(out dummyResultToString), "Non-Enum type didn't cause failure.");
        }


        private struct dummyValueType
        {
            private int one;
            private int two;

            public int One
            {
                get { return one; }
                set { one = value; }
            }

            public int Two
            {
                get { return two; }
                set { two = value; }
            }
        }
    }

}
