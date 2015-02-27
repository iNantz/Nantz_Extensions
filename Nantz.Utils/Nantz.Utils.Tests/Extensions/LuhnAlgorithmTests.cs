using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nantz.Utils.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Nantz.Utils.Tests
{
    [TestClass()]
    public class LuhnAlgorithmTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Must be a valid number")]
        public void InvalidNumberTest()
        {
            Assert.AreEqual(false, "x79927398713x".LuhnCheck());
        }

        [TestMethod()]
        public void LuhnCheckStringTest()
        {
            Assert.AreEqual(false, "79927398712".LuhnCheck());
            Assert.AreEqual(true, "79927398713".LuhnCheck());
        }

        [TestMethod()]
        public void LuhnCheckStrucTest()
        {
            long lInt = 79927398713;
            Assert.AreEqual(true, lInt.LuhnCheck());
            lInt = 79927398712;
            Assert.AreEqual(false, lInt.LuhnCheck());

            long lLong = 79927398713;
            Assert.AreEqual(true, lLong.LuhnCheck());
            lLong = 79927398712;
            Assert.AreEqual(false, lLong.LuhnCheck());

            decimal lDecimal = 79927398713;
            Assert.AreEqual(true, lDecimal.LuhnCheck());
            lDecimal = 79927398712;
            Assert.AreEqual(false, lDecimal.LuhnCheck());

            double lDouble = 79927398713;
            Assert.AreEqual(true, lDouble.LuhnCheck());
            lDouble = -79927398713;
            Assert.AreEqual(false, lDouble.LuhnCheck());
        }
    }
}
