using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Chapter6_2
{

    // A mock state used for testing the state system
    public class MockState : IGameObject
    {
        public bool HasProcessBeenCalled { get; set; }
        public bool HasRenderBeenCalled { get; set; }

        public MockState()
        {
            HasProcessBeenCalled = false;
            HasRenderBeenCalled = false;
        }

        #region IGameObject Members

        public void Update(double elapsedTime)
        {
            HasProcessBeenCalled = true;
        }

        public void Render()
        {
            HasRenderBeenCalled = true;
        }

        #endregion
    }

    [TestFixture]
    public class Test_StateSystem
    {
        [Test]
        public void TestAddedStateExists()
        {
            StateSystem stateSystem = new StateSystem();
            stateSystem.AddState("splash", new SplashScreenState(stateSystem));

            // Does the added function now exist?
            Assert.IsTrue(stateSystem.Exists("splash"));
        }

        [Test]
        public void TestMostRecentlyAddedStateExists()
        {
            StateSystem stateSystem = new StateSystem();
            stateSystem.AddState("splash1", new SplashScreenState(stateSystem));
            stateSystem.AddState("splash2", new SplashScreenState(stateSystem));
            Assert.IsTrue(stateSystem.Exists("splash2"));
        }

        [Test]
        public void TestFirstAddedStateExists()
        {
            StateSystem stateSystem = new StateSystem();
            stateSystem.AddState("splash1", new SplashScreenState(stateSystem));
            stateSystem.AddState("splash2", new SplashScreenState(stateSystem));
            Assert.IsTrue(stateSystem.Exists("splash1"));
        }

        [Test]
        public void TestRenderSelectedState()
        {
            StateSystem stateSystem = new StateSystem();
            MockState mock = new MockState();
            stateSystem.AddState("mock", mock);
            stateSystem.ChangeState("mock");
            stateSystem.Render();
            // State wasn't changed to mock so it shouldn't be 
            // rendered.
            Assert.IsTrue(mock.HasRenderBeenCalled);
        }

        [Test]
        public void TestProcessSelectedState()
        {
            StateSystem stateSystem = new StateSystem();
            MockState mock = new MockState();
            stateSystem.AddState("mock", mock);
            stateSystem.ChangeState("mock");
            stateSystem.Update(0);
            Assert.IsTrue(mock.HasProcessBeenCalled);
        }

        [Test]
        public void TestProcessNoSelectedState()
        {
            StateSystem stateSystem = new StateSystem();
            MockState mock = new MockState();
            stateSystem.AddState("mock", mock);
            stateSystem.Update(0);
            // State wasn't changed to mock so it shouldn't be 
            // processed.
            Assert.IsFalse(mock.HasProcessBeenCalled);
        }

        [Test]
        public void TestRenderNoSelectedState()
        {
            StateSystem stateSystem = new StateSystem();
            MockState mock = new MockState();
            stateSystem.AddState("mock", mock);
            stateSystem.Render();
            // State wasn't changed to mock so it shouldn't be 
            // rendered.
            Assert.IsFalse(mock.HasRenderBeenCalled);
        }

        [Test]
        public void TestChangeStates()
        {
            StateSystem stateSystem = new StateSystem();
            MockState mock1 = new MockState();
            MockState mock2 = new MockState();
            stateSystem.AddState("mock1", mock1);
            stateSystem.AddState("mock2", mock2);
            stateSystem.ChangeState("mock1");
            stateSystem.ChangeState("mock2");
            stateSystem.Render();
            // State wasn't changed to mock so it shouldn't be 
            // rendered.
            Assert.IsFalse(mock1.HasRenderBeenCalled);
            Assert.IsTrue(mock2.HasRenderBeenCalled);
        }
    }

}
