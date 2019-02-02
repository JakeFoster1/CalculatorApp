using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class Calculator
    {
        decimal m_Num1, m_Num2;
        char m_Operation;
        bool m_IsOverflowed, m_IsUnderflowed, m_IsDivideByZero;

        public Calculator()
        {
            m_Num1 = 0m;
            m_Num2 = 0m;
            m_Operation = ' ';
            m_IsOverflowed = false;
            m_IsUnderflowed = false;
            m_IsDivideByZero = false;
        }

        public void Clear()
        {
            m_Num1 = 0m;
            m_Num2 = 0m;
            m_Operation = ' ';
            m_IsOverflowed = false;
            m_IsUnderflowed = false;
            m_IsDivideByZero = false;
        }

        public bool isOverflowed()
        {
            return m_IsOverflowed;
        }

        public bool isUnderflowed()
        {
            return m_IsUnderflowed;
        }

        public bool isDivideByZero()
        {
            return m_IsDivideByZero;
        }

        public decimal GetNum1()
        {
            return m_Num1;
        }

        public decimal GetNum2()
        {
            return m_Num2;
        }

        public char GetOperation()
        {
            return m_Operation;
        }
        
        public void EnterNum1(decimal input)
        {
            m_Num1 = input;
        }

        public void EnterNum2(decimal input)
        {
            m_Num2 = input;
        }

        public void EnterOperation(char input)
        {
            m_Operation = input;
        }

        public decimal Compute()
        {
            switch(m_Operation)
            {
                case '+':
                    try
                    {
                        return m_Num1 + m_Num2;
                    }
                    catch(System.OverflowException exception)
                    {
                        m_IsOverflowed = true;
                        return 0;
                    }
                case '-':
                    try
                    {
                        return m_Num1 - m_Num2;
                    }
                    catch (System.OverflowException exception)
                    {
                        m_IsUnderflowed = true;
                        return 0;
                    }
                case '/':
                    try
                    {
                        return m_Num1 / m_Num2;
                    }
                    catch (System.DivideByZeroException exception)
                    {
                        m_IsDivideByZero = true;
                        return 0;
                    }
                case '*':
                    try
                    {
                        return m_Num1 * m_Num2;
                    }
                    catch (System.OverflowException exception)
                    {
                        m_IsOverflowed = true;
                        return 0;
                    }
                default:
                    return m_Num1;
            }
        }
    }
}
