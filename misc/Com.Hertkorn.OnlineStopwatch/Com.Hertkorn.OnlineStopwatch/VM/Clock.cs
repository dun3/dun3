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
using System.ComponentModel;
using Com.Hertkorn.OnlineStopwatch.Validator;

namespace Com.Hertkorn.OnlineStopwatch.VM
{
    public class Clock : INotifyPropertyChanged
    {
        public Clock()
        {
            m_validator = new ClockValidator(this);
        }

        public Clock(string hours, string minutes, string seconds)
            : this()
        {
            m_hours = hours;
            m_minutes = minutes;
            m_seconds = seconds;
        }


        private string m_hours;
        public string Hours
        {
            get { return m_hours; }
            set
            {
                if (m_hours == value) { return; }
                m_hours = value;
                RaisePropertyChanged("Hours");
            }
        }

        private string m_minutes;
        public string Minutes
        {
            get { return m_minutes; }
            set
            {
                if (m_minutes == value) { return; }
                m_minutes = value;
                RaisePropertyChanged("Minutes");
            }
        }

        private string m_seconds;
        public string Seconds
        {
            get { return m_seconds; }
            set
            {
                if (m_seconds == value) { return; }
                m_seconds = value;
                RaisePropertyChanged("Seconds");
            }
        }

        private ClockValidator m_validator;
        public ClockValidator Validator
        {
            get
            {
                return m_validator;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        internal void RaisePropertyChanged(string name)
        {
            OnPropertyChanged(name);
        }
    }
}
