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
using System.Runtime.Serialization;

namespace Com.Hertkorn.OnlineStopwatch
{
    public class IntegerTextBoxParseException : Exception
    {
        public IntegerTextBoxParseException()
        {
            
        }
        public IntegerTextBoxParseException(string message)
            : base(message)
        {
            
        }
        public IntegerTextBoxParseException(string message, Exception innerException)
            : base(message, innerException)
        {
            
        }


    }
}
