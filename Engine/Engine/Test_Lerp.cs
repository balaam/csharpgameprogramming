using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Engine
{
    [TestFixture]
    class Test_Lerp
    {
        [Test]
        public void TestLerpTenToOne()
        {
            Assert.True(Interpolation.Lerp(5, 0, 10, 0, 1) == 0.5);
        }

        [Test]
        public void TestLerpTenToOneWithMinus()
        {
            Assert.True(Interpolation.Lerp(5, 0, 10, -1, 1) == 0);
        }

        [Test]
        public void TestLerpSameInSameOut()
        {
            Assert.True(Interpolation.Lerp(5, 0, 10, 0, 10) == 5);
        }
    }
}
