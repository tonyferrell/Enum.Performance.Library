using Enum.Performance.Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enum.Performance.UnitTests.Performance
{
    [TestClass]
    public class EnumManagerPerformanceTests
    {
        const int ITERATIONS = 1000000;
        const int UNIQUE_VALUES = 100;

        private static readonly Random rand = new Random();
        private static readonly string[] validStrings;
        private static readonly LargeEnum[] validEnums;

        static EnumManagerPerformanceTests()
        {
            GetRandomStringValues(UNIQUE_VALUES, out validStrings, out validEnums);
        }

        private static void GetRandomStringValues(int numValues, out string[] validStrings, out LargeEnum[] validEnums)
        {
            const int max = (int)LargeEnum.OneHundred + 1;
            validStrings = new string[numValues];
            validEnums = new LargeEnum[numValues];

            for (int i = 0; i < numValues; ++i)
            {
                LargeEnum nextEnum = (LargeEnum)rand.Next(max);
                validEnums[i] = nextEnum;
                validStrings[i] = nextEnum.ToString();
            }
        }

        [TestMethod]
        public void EnumManager_PreInitalized_TryParse_Performance()
        {
            EnumManager<LargeEnum>.Initalize();
            LargeEnum value;
            LargeEnum otherValue = LargeEnum.One;
            int length = validStrings.Length;
            for (int i = 0; i < ITERATIONS; ++i)
            {
                EnumManager<LargeEnum>.TryParse(validStrings[i % length], out value);
                otherValue = value;
            }

            Console.WriteLine(otherValue);
        }

        [TestMethod]
        public void EnumManager_PreInitalized_TryGetString_Performance()
        {
            EnumManager<LargeEnum>.Initalize();

            string value;
            string otherValue = String.Empty;

            int length = validStrings.Length;
            for (int i = 0; i < ITERATIONS; ++i)
            {
                EnumManager<LargeEnum>.TryGetString(validEnums[i % length], out value);
                otherValue = value;
            }

            Console.WriteLine(otherValue);
        }
    }
}
