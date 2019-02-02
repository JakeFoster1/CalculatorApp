using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Library.Test
{
    [TestFixture]
    public class CalculatorTest
    {
        public const string MinValue = "-79228162514264337593543950335";
        public const string MaxValue = "79228162514264337593543950335";

        [Test]
        public void When_Overflow([Values(MaxValue)] decimal inputNum1,
            [Values(MaxValue)] decimal inputNum2,
            [Values('+', '*')]char charInput)
        {
            Calculator testCalc = new Calculator();
            testCalc.EnterNum1(inputNum1);
            testCalc.EnterOperation(charInput);
            testCalc.EnterNum2(inputNum2);

            testCalc.Compute();

            Assert.AreEqual(true, testCalc.isOverflowed());
        }

        [Test]
        public void When_Underflow([Values(MinValue)] decimal inputNum1,
            [Values(MaxValue)] decimal inputNum2,
            [Values('-')]char charInput)
        {
            Calculator testCalc = new Calculator();
            testCalc.EnterNum1(inputNum1);
            testCalc.EnterOperation(charInput);
            testCalc.EnterNum2(inputNum2);

            testCalc.Compute();

            Assert.AreEqual(true, testCalc.isUnderflowed());
        }

        [Test]
        public void When_DivideByZero([Values(MaxValue)] decimal inputNum1,
            [Values(0)] decimal inputNum2,
            [Values('/')]char charInput)
        {
            Calculator testCalc = new Calculator();
            testCalc.EnterNum1(inputNum1);
            testCalc.EnterOperation(charInput);
            testCalc.EnterNum2(inputNum2);

            testCalc.Compute();

            Assert.AreEqual(true, testCalc.isDivideByZero());
        }

        [Test]
        public void When_1st_Number_Entered_decimal(
            [Values(MinValue, "-1.165485513691523", "-1.0", "0.0", "1.0", "1.165485513691523", MaxValue)] string input)
        {
            Calculator testCalc = new Calculator();

            testCalc.EnterNum1(decimal.Parse(input));

            Assert.AreEqual(testCalc.GetNum1(), decimal.Parse(input));
        }

        [Test]
        public void When_2nd_Number_Entered_decimal(
            [Values(MinValue, "-1.165485513691523", "-1.0", "0.0", "1.0", "1.165485513691523", MaxValue)] string input)
        {
            Calculator testCalc = new Calculator();

            testCalc.EnterNum2(decimal.Parse(input));

            Assert.AreEqual(testCalc.GetNum2(), decimal.Parse(input));
        }

        [Test]
        public void When_Operation_Entered(
            [Values('+', '-', '/', '*')]char charInput)
        {
            Calculator testCalc = new Calculator();
            testCalc.EnterOperation(charInput);

            Assert.AreEqual(testCalc.GetOperation(), charInput);
        }

        [Test]
        public void When_Computing_Both_Ints_Exist(
            [Values(int.MinValue, -1, 0, 1, int.MaxValue)]int inputNum1,
            [Values(int.MinValue, -1, 0, 1, int.MaxValue)]int inputNum2,
            [Values('+', '-', '/', '*')]char charInput)
        {
            Calculator testCalc = new Calculator();
            testCalc.EnterNum1(inputNum1);
            testCalc.EnterOperation(charInput);
            testCalc.EnterNum2(inputNum2);

            decimal expected;

            switch (charInput)
            {
                case '+':
                    try
                    {
                        expected = (decimal)inputNum1 + (decimal)inputNum2;
                    }
                    catch (System.OverflowException exception)
                    {
                        expected = 0;
                    }
                    break;
                case '-':
                    try
                    {
                        expected = (decimal)inputNum1 - (decimal)inputNum2;
                    }
                    catch (System.OverflowException exception)
                    {
                        expected = 0;
                    }
                    break;
                case '/':
                    try
                    {
                        expected = (decimal)inputNum1 / (decimal)inputNum2;
                    }
                    catch (System.DivideByZeroException exception)
                    {
                        expected = 0;
                    }
                    break;
                case '*':
                    try
                    {
                        expected = (decimal)inputNum1 * (decimal)inputNum2;
                    }
                    catch (System.OverflowException exception)
                    {
                        expected = 0;
                    }
                    break;
                default:
                    expected = (decimal)inputNum1;
                    break;
            }

            Assert.AreEqual(testCalc.Compute(), expected);
        }

        [Test]
        public void When_Computing_Both_decimals_Exist(
            [Values(MinValue, -1.165485513691523, -1.0, 0.0, 1.0, 1.165485513691523, MaxValue)]decimal inputNum1,
            [Values(MinValue, -1.165485513691523, -1.0, 0.0, 1.0, 1.165485513691523, MaxValue)]decimal inputNum2,
            [Values('+', '-', '/', '*')]char charInput)
        {
            Calculator testCalc = new Calculator();
            testCalc.EnterNum1(inputNum1);
            testCalc.EnterOperation(charInput);
            if (inputNum2 == 0 && charInput == '/')
                inputNum2 += 1;
            testCalc.EnterNum2(inputNum2);

            decimal expected;

            switch (charInput)
            {
                case '+':
                    try
                    {
                        expected = (decimal)inputNum1 + (decimal)inputNum2;
                    }
                    catch (System.OverflowException exception)
                    {
                        expected = 0;
                    }
                    break;
                case '-':
                    try
                    {
                        expected = (decimal)inputNum1 - (decimal)inputNum2;
                    }
                    catch (System.OverflowException exception)
                    {
                        expected = 0;
                    }
                    break;
                case '/':
                    expected = (decimal)inputNum1 / (decimal)inputNum2;
                    break;
                case '*':
                    try
                    {
                        expected = (decimal)inputNum1 * (decimal)inputNum2;
                    }
                    catch (System.OverflowException exception)
                    {
                        expected = 0;
                    }
                    break;
                default:
                    expected = (decimal)inputNum1;
                    break;
            }

            Assert.AreEqual(testCalc.Compute(), expected);
        }
    }
}
