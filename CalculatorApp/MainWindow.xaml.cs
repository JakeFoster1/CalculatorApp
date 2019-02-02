using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CalculatorApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Calculator.Calculator myCalc;
        private bool hasComputed = false;
        private bool hasNum1 = false;

        public MainWindow()
        {
            InitializeComponent();
            Text_Box.Text = "0";
            myCalc = new Calculator.Calculator();
        }

        private void Click_Num(object sender, RoutedEventArgs e)
        {
            string output = sender.ToString();
            output = output.Substring(output.Length - 1, 1);

            if(hasComputed)
            {
                Text_Box.Text = string.Empty;
                Text_Box.Text = output;
                hasComputed = false;
            }
            else if (hasNum1)
            {
                Text_Box.Text = string.Empty;
                Text_Box.Text = output;
                hasNum1 = false;
            }
            else if(!(output == "0" && Text_Box.Text == "0"))
            {
                Text_Box.Text += output;
            }

            if (Text_Box.Text.Substring(0, 1) == "0" && Text_Box.Text.Length > 1)
            {
                Text_Box.Text = Text_Box.Text.Substring(1, Text_Box.Text.Length-1);
            }
        }

        private void Click_Decimal(object sender, RoutedEventArgs e)
        {
            if(!Text_Box.Text.Contains('.'))
            {
                Text_Box.Text += '.';
            }
        }

        private void Click_Backspace(object sender, RoutedEventArgs e)
        {
            if(Text_Box.Text != string.Empty)
            {
                Text_Box.Text = Text_Box.Text.Substring(0, Text_Box.Text.Length - 1);
            }
        }

        private void Click_Clear(object sender, RoutedEventArgs e)
        {
            Text_Box.Text = "0";
        }

        private void Click_Operation(object sender, RoutedEventArgs e)
        {
            string operation;

            if (myCalc.GetOperation() == ' ')
            {
                operation = sender.ToString();
                operation = operation.Substring(operation.Length - 1, 1);

                myCalc.EnterNum1(decimal.Parse(Text_Box.Text));

                switch (operation)
                {
                    case "+":
                        myCalc.EnterOperation('+');
                        break;
                    case "-":
                        myCalc.EnterOperation('-');
                        break;
                    case "X":
                        myCalc.EnterOperation('*');
                        break;
                    case "÷":
                        myCalc.EnterOperation('/');
                        break;
                }

                hasNum1 = true;
            }
        }

        private void Click_PlusMinus(object sender, RoutedEventArgs e)
        {
            if(Text_Box.Text == "0")
            {
                // Do nothing
            }
            else if(Text_Box.Text.Contains("-"))
            {
                Text_Box.Text = Text_Box.Text.Substring(1, Text_Box.Text.Length - 1);
            }
            else
            {
                Text_Box.Text = "-" + Text_Box.Text;
            }
        }

        private void Click_Equals(object sender, RoutedEventArgs e)
        {
            if (myCalc.GetOperation() != ' ')
            {
                myCalc.EnterNum2(decimal.Parse(Text_Box.Text));

                string output = myCalc.Compute().ToString();

                if (myCalc.isDivideByZero())
                {
                    Text_Box.Text = "DIVIDE BY ZERO ERROR";
                }
                else if (myCalc.isOverflowed())
                {
                    Text_Box.Text = "OVERFLOW ERROR";
                }
                else if (myCalc.isUnderflowed())
                {
                    Text_Box.Text = "UNDERFLOW ERROR";
                }
                else
                {
                    Text_Box.Text = output;
                }

                myCalc.Clear();
                hasComputed = true;
            }
        }
    }
}
