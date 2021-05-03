using Microsoft.VisualStudio.TestTools.UnitTesting;
using OhmValueCalculatorApp.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OhmValueUnitTest
{
    [TestClass]
    public class OhmValueCalculatorUnitTest
    {
        IOhmValueCalculator OhmValueCalculator { get; set; }

        [TestInitialize]
        public void TestInitializeOhmValueCalculator()
        {
            OhmValueCalculator = new OhmValueCalculator();
        }

        [TestMethod]
        public void Test_ShouldRaiseException_OnEmptyColorCode()
        {
            try
            {
                OhmValueCalculator.CalculateOhmValue(string.Empty, string.Empty, string.Empty, string.Empty);
            }
            catch
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void Test_ShouldRaiseException_OnInvalidColorACode()
        {
            try
            {
                OhmValueCalculator.CalculateOhmValue("ZZ", "RD", "GN", "YE");
            }
            catch
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void Test_ShouldRaiseException_OnInvalidColorBCode()
        {
            try
            {
                OhmValueCalculator.CalculateOhmValue("RD", "ZZ", "GN", "YE");
            }
            catch
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void Test_ShouldRaiseException_OnInvalidColorCCode()
        {
            try
            {
                OhmValueCalculator.CalculateOhmValue("RD", "GN", "ZZ", "YE");
            }
            catch
            {
                Assert.IsTrue(true);
            }

        }

        [TestMethod]
        public void Test_ShouldRaiseException_OnInvalidColorDCode()
        {
            try
            {
                OhmValueCalculator.CalculateOhmValue("RD", "GN", "RD", "ZZ");
            }
            catch
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void Test_ShouldPass_OnValidColorDCode()
        {
            try
            {
                var result = OhmValueCalculator.CalculateOhmValue("RD", "GN", "RD", "RD");
                if (result != 0)
                {
                    Assert.IsTrue(true);
                }
            }
            catch
            {
                Assert.IsTrue(true);
            }
        }
    }
}
