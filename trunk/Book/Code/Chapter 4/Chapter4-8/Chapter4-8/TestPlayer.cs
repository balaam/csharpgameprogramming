using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlayerTest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NUnit.Framework;

    namespace PlayerTest
    {
        [TestFixture]
        public class TestPlayer
        {
            [Test]
            public void BasicTest()
            {
                Assert.That(true);
            }

            [Test]
            public void StartsLifeSmall()
            {
                Player player = new Player();
                Assert.False(player.IsEnlarged());
            }

            [Test]
            public void MushroomEnlargesPlayer()
            {
                Player player = new Player();
                player.Eat("mushroom");
                Assert.True(player.IsEnlarged());
            }


        }
    }

}
