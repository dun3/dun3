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
using System.Collections.Generic;
using Com.Hertkorn.OnlineStopwatch.VM;
using System.Windows.Threading;

namespace Com.Hertkorn.OnlineStopwatch.Validator
{
    public class ClockValidator : INotifyPropertyChanged
    {
        public static readonly string PROPERTY_NAME_INVALID = "Invalid";
        private Dictionary<string, string> m_errors;
        private List<string> m_propertiesToValidate;
        private bool m_fireValidated;

        private Clock m_data;

        public const string PROPERTY_NAME_HOURS = "Hours";
        public bool InvalidHours
        {
            get
            {
                return m_errors.ContainsKey(PROPERTY_NAME_HOURS);
            }
            set
            {
                if (value)
                {
                    RegisterError(PROPERTY_NAME_HOURS, "Hour must be greater or equals 0 and less or equals 23");
                }
                else
                {
                    ClearError(PROPERTY_NAME_HOURS);
                }
            }
        }

        private bool ParseInt(string s, int minValue, int maxValue)
        {
            int parsed;
            if (int.TryParse(s, out parsed))
            {
                return ((parsed >= minValue) && (parsed < maxValue));
            }
            else
            {
                return false;
            }
        }

        private void ValidateHours(string hours)
        {
            InvalidHours = ParseInt(hours, 0, 24);
        }

        public const string PROPERTY_NAME_MINUTES = "Minutes";
        public bool InvalidMinutes
        {
            get
            {
                return m_errors.ContainsKey(PROPERTY_NAME_MINUTES);
            }
            set
            {
                if (value)
                {
                    RegisterError(PROPERTY_NAME_MINUTES, "Minutes must be greater or equals 0 and less or equals 59");
                }
                else
                {
                    ClearError(PROPERTY_NAME_MINUTES);
                }
            }
        }

        private void ValidateMinutes(string minutes)
        {
            InvalidMinutes = ParseInt(minutes, 0, 60);
        }

        public const string PROPERTY_NAME_SECONDS = "Seconds";
        public bool InvalidSeconds
        {
            get
            {
                return m_errors.ContainsKey(PROPERTY_NAME_SECONDS);
            }
            set
            {
                if (value)
                {
                    RegisterError(PROPERTY_NAME_SECONDS, "Seconds must be greater or equals 0 and less or equals 59");
                }
                else
                {
                    ClearError(PROPERTY_NAME_SECONDS);
                }
            }
        }

        private void ValidateSeconds(string seconds)
        {
            InvalidSeconds = ParseInt(seconds, 0, 60);
        }

        public ClockValidator(Clock clock)
        {
            m_data = clock;
            m_data.PropertyChanged += new PropertyChangedEventHandler(Clock_PropertyChanged);
            // default valid
            m_errors = new Dictionary<string, string>();

        }

        public ClockValidator(Clock clock, bool defaultInvalid)
            : this(clock)
        {
            if (defaultInvalid)
            {
                m_errors.Add(PROPERTY_NAME_HOURS, PROPERTY_NAME_HOURS);
                m_errors.Add(PROPERTY_NAME_MINUTES, PROPERTY_NAME_MINUTES);
                m_errors.Add(PROPERTY_NAME_SECONDS, PROPERTY_NAME_SECONDS);
            }
        }

        public static readonly string PROPERTY_NAME_ISVALID = "IsValid";

        public bool IsValid
        {
            get
            {
                return (m_errors.Keys.Count == 0);
            }
            set
            {
                OnPropertyChanged(PROPERTY_NAME_ISVALID);
                Dispatcher.BeginInvoke(() => m_data.RaisePropertyChanged("Validator"));
            }
        }

        void Clock_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsValid")
            {
                return;
            }

            switch (e.PropertyName)
            {
                case PROPERTY_NAME_HOURS:
                    ValidateHours(m_data.Hours);
                    break;
                case PROPERTY_NAME_MINUTES:
                    ValidateMinutes(m_data.Minutes);
                    break;
                case PROPERTY_NAME_SECONDS:
                    ValidateSeconds(m_data.Seconds);
                    break;

            }
        }

        public void Validate()
        {
            m_propertiesToValidate = new List<string>() { 
                PROPERTY_NAME_INVALID + PROPERTY_NAME_HOURS, 
                PROPERTY_NAME_INVALID + PROPERTY_NAME_MINUTES, 
                PROPERTY_NAME_INVALID + PROPERTY_NAME_SECONDS };
            //
            ValidateHours(m_data.Hours);
            ValidateMinutes(m_data.Minutes);
            ValidateSeconds(m_data.Seconds);
        }

        public bool AllPropertiesValidated(string propertyValidated)
        {
            m_propertiesToValidate.Remove(propertyValidated);
            return (m_propertiesToValidate.Count == 0);
        }

        public event EventHandler<EventArgs> Validated;

        protected void OnValidated()
        {
            if (null != Validated)
            {
                Validated(this, new EventArgs());
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            //
            if (m_fireValidated && AllPropertiesValidated(name))
            {
                m_fireValidated = false;
                Dispatcher.BeginInvoke(() => OnValidated());
            }
        }

        public string this[string propertyName]
        {
            get
            {
                if (m_errors.ContainsKey(propertyName))
                {
                    return m_errors[propertyName];
                }
                else
                {
                    return null;
                }
            }
        }

        public void RegisterError(string propertyName, string message)
        {
            if (m_errors.ContainsKey(propertyName))
            {
                m_errors[propertyName] = message;
            }
            else
            {
                m_errors.Add(propertyName, message);
            }
            OnPropertyChanged(PROPERTY_NAME_INVALID + propertyName);
            Dispatcher.BeginInvoke(() => IsValid = false);
        }

        public void ClearError(string propertyName)
        {
            if (m_errors.ContainsKey(propertyName))
            {
                m_errors.Remove(propertyName);
            }
            OnPropertyChanged(PROPERTY_NAME_INVALID + propertyName);
            Dispatcher.BeginInvoke(() => IsValid = true);
        }

        public Dispatcher Dispatcher
        {
            get { return Application.Current.RootVisual.Dispatcher; }
        }
    }
}
