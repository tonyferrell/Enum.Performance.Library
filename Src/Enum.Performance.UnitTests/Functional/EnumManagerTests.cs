using Enum.Performance.Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enum.Performance.UnitTests.Functional
{
    [TestClass]
    public class EnumManagerTests
    {
        private const SmallEnum oneValue = SmallEnum.One;
        private readonly string oneString = SmallEnum.One.ToString();

        [TestMethod]
        public void EnumManager_Should_Parse_Strings_Without_Initalization()
        {
            SmallEnum parsedValue;
            Assert.IsTrue(EnumManager<SmallEnum>.TryParse(oneString, out parsedValue), "Parsing failed with existing value");

            Assert.AreEqual(oneValue, parsedValue, oneString + " parsed to unexpected value " + oneValue.ToString());
        }

        [TestMethod]
        public void EnumManager_Should_GetString_For_Valid_Value_Without_Initalization()
        {
            string parsedValue;
            Assert.IsTrue(
                EnumManager<SmallEnum>.TryGetString(SmallEnum.One, out parsedValue),
                "String retrieval failed for known enum.");

            Assert.AreEqual(oneString, parsedValue, oneValue.ToString() + " converted to unexpected string " + parsedValue);
        }

        [TestMethod]
        public void EnumManager_Should_Return_False_When_Parsing_Unknown_Strings()
        {
            SmallEnum parsedValue;
            Assert.IsFalse(EnumManager<SmallEnum>.TryParse("garbageValue", out parsedValue), "Parsing did not fail for garbage string");
        }

        [TestMethod]
        public void EnumManager_Should_Return_False_For_Invalid_Enum_Values()
        {
            string parsedValue;

            SmallEnum invalidEnum = (SmallEnum)1000;
            Assert.IsFalse(
                EnumManager<SmallEnum>.TryGetString(invalidEnum, out parsedValue),
                "String retrieval didn't failed for excessive value.");
        }
    }
}
