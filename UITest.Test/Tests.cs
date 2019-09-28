using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using UITest.ViewModel;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace UITest.Test
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        CalculateViewModel _calculateViewModel = new CalculateViewModel();

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void WelcomeTextIsDisplayed()
        {
            //AppResult[] results = app.WaitForElement(c => c.Marked("Welcome to Xamarin.Forms!"));
            app.Screenshot("First screen.");

            //Assert.IsTrue(results.Any());
            Assert.Pass();
        }

        [Test]
        [TestCase("5","7",12d)]
        [TestCase("7.5","7",14d)]
        [TestCase("7","7.5",14.5d)]
        [TestCase("ksfldjfldsj","7.5",7.5d)]
        [TestCase("ksfldjfldsj","slkfslkdflkjfkdf",0d)]
        [TestCase("25.5ksfldjfldsj","slkfslkdflkjfkdf",25d)]
        [TestCase("ksfldjfldsj","25.5.5slkfslkdflkjfkdf",25.5d)]
        [TestCase("25.5ksfldjfldsj","25.5.5slkfsl",50.5d)]
        [TestCase("25.5k","25.5.5slkfsl",50.5d)]
        public void TestPlus(string num1, string num2 ,double result)
        {
            _calculateViewModel.Num1 = num1;
            _calculateViewModel.Num2 = num2;
            DateTime date = DateTime.Now;
            while (DateTime.Now < date.AddSeconds(2))
            {
                _calculateViewModel.Num1 = _calculateViewModel.Num1;
                _calculateViewModel.Num2 = _calculateViewModel.Num2;
            }
            _calculateViewModel.PlusMethod();
            var model = _calculateViewModel.Result;

            Assert.AreEqual(model, result);
        }

        [Test]
        [TestCase("5", "7", 12d)]
        [TestCase("7.5", "7", 14d)]
        [TestCase("7", "7.5", 14.5d)]
        [TestCase("ksfldjfldsj", "7.5", 7.5d)]
        [TestCase("ksfldjfldsj", "slkfslkdflkjfkdf", 0d)]
        [TestCase("25.5ksfldjfldsj", "slkfslkdflkjfkdf", 25d)]
        [TestCase("ksfldjfldsj", "25.5.5slkfslkdflkjfkdf", 25.5d)]
        [TestCase("25.5ksfldjfldsj", "25.5.5slkfsl", 50.5d)]
        [TestCase("25.5k", "25.5.5slkfsl", 50.5d)]
        public void TestPlusUI(string num1, string num2, double result)
        {

            app.EnterText("EntryNum01", num1);
            app.EnterText("EntryNum02", num2);
            app.Tap("PlusButton");

            var model = Double.Parse(app.Query("LabelResult").SingleOrDefault().Text);
            Assert.AreEqual(model, result);
        }

        [Test]
        public void TestMinus(string num1, string num2, double result)
        {
            app.EnterText("EntryNum01", num1);
            app.EnterText("EntryNum02", num2);
            app.Tap("PlusButton");

            var model = Double.Parse(app.Query("LabelResult").SingleOrDefault().Text);
            Assert.AreEqual(model, result);
        }

        [Test]
        public void TestFuntion()
        {
            app.Repl();
        }
    }
}
