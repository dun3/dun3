using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Com.Hertkorn.OnlineStopwatch
{
    public class IntegerTextBox : TextBox
    {
        public IntegerTextBox()
            : base()
        {
            CheckValid();
        }

        private bool CheckValid()
        {
            try
            {
                int parsed;
                if (int.TryParse(Text, out parsed))
                {
                    if (parsed < MinValue)
                    {
                        m_errorMessage = "Smaller than MinValue";
                    }
                    else if (parsed > MaxValue)
                    {
                        m_errorMessage = "Larger than MaxValue";
                    }
                    else
                    {
                        m_errorMessage = "";
                        return true;
                    }
                }
                else
                {
                    m_errorMessage = "Not a parsable number '" + Text + "'";
                }
            }
            catch (Exception ex)
            {
                m_errorMessage = ex.ToString();
            }
            return false;
        }

        public int IntValue
        {
            get
            {
                if (CheckValid())
                {
                    try
                    {
                        return int.Parse(Text);
                    }
                    catch (Exception ex)
                    {
                        throw new IntegerTextBoxParseException("Parse Error", ex);
                    }
                }
                else
                {
                    throw new IntegerTextBoxParseException(ErrorMessage);
                }
            }
        }

        private int m_minValue;
        public int MinValue
        {
            get { return m_minValue; }
            set { m_minValue = value; }
        }

        private int m_maxValue;
        public int MaxValue
        {
            get { return m_maxValue; }
            set { m_maxValue = value; }
        }

        public bool IsValid
        {
            get { return CheckValid(); }
        }

        private string m_errorMessage = "";
        public string ErrorMessage
        {
            get
            {
                CheckValid();
                return m_errorMessage;
            }
        }
    }
}
