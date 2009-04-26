using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO;
using System.Reflection;
using System.Windows.Threading;

namespace Com.Hertkorn.OnlineStopwatch
{
    public partial class Page : UserControl
    {
        DispatcherTimer m_requestUpdate = new DispatcherTimer();
        DateTime m_targetDateTime = DateTime.MaxValue;
        TimeSpan m_initialTimeSpan = new TimeSpan(1, 2, 10);

        public Page()
        {
            InitializeComponent();

            m_requestUpdate.Tick += new EventHandler(m_requestUpdate_Tick);
            m_requestUpdate.Interval = new TimeSpan(0, 0, 0, 0, 100);

            Hours.Text = string.Format("{0:d2}", m_initialTimeSpan.Hours);
            Minutes.Text = string.Format("{0:d2}", m_initialTimeSpan.Minutes);
            Seconds.Text = string.Format("{0:d2}", m_initialTimeSpan.Seconds);
        }

        void m_requestUpdate_Tick(object sender, EventArgs e)
        {
            if (false)
            {
                Ring.Stop();
                Ring.Play();
            }
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            m_requestUpdate.Start();
            SetUIState();
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            m_requestUpdate.Stop();
            SetUIState();
        }

        private void SetUIState()
        {
            Ring.Stop();
            Start.IsEnabled = !m_requestUpdate.IsEnabled;
            Stop.IsEnabled = m_requestUpdate.IsEnabled;
        }

        private void Hours_LostFocus(object sender, RoutedEventArgs e)
        {
            ParseHours();
        }

        private void Minutes_LostFocus(object sender, RoutedEventArgs e)
        {
            ParseMinutes();
        }

        private void Seconds_LostFocus(object sender, RoutedEventArgs e)
        {
            ParseSeconds();
        }

        private void ParseHours()
        {
            SafeUpdate(Hours, 23, (parsedValue) =>
            {
                return new TimeSpan(parsedValue, m_initialTimeSpan.Minutes, m_initialTimeSpan.Seconds);
            });
        }

        private void ParseMinutes()
        {
            SafeUpdate(Minutes, 59, (parsedValue) =>
            {
                return new TimeSpan(m_initialTimeSpan.Hours, parsedValue, m_initialTimeSpan.Seconds);
            });
        }

        private void ParseSeconds()
        {
            SafeUpdate(Seconds, 59, (parsedValue) =>
            {
                return new TimeSpan(m_initialTimeSpan.Hours, m_initialTimeSpan.Minutes, parsedValue);
            });
        }

        private void SafeUpdate(TextBox textBox, int maxValue, Func<int, TimeSpan> createTimeSpan)
        {
            if (!m_requestUpdate.IsEnabled)
            {
                try
                {
                    int parsedValue = ParseUserInput(textBox, maxValue);

                    m_initialTimeSpan = createTimeSpan(parsedValue);

                    UpdateUI();
                }
                catch (Exception ex)
                {
                    ErrorMessage.Text = ex.Message;
                }
            }
        }

        private int ParseUserInput(TextBox textBox, int maxValue)
        {
            int currentValue;

            if (int.TryParse(textBox.Text, out currentValue))
            {
                if (currentValue >= 0)
                {
                    if (currentValue <= maxValue)
                    {

                        return currentValue;
                    }
                    else
                    {
                        throw new Exception("The maximal values allowed is " + maxValue.ToString());
                    }
                }
                else
                {
                    throw new Exception("Nevative Values are not allowed");
                }
            }
            else
            {
                throw new Exception("Could not parse the Input '" + textBox.Text + "'");
            }
        }
        
        private void UpdateUI()
        {
            Hours.Text = string.Format("{0:d2}", m_initialTimeSpan.Hours);
            Minutes.Text = string.Format("{0:d2}", m_initialTimeSpan.Minutes);
            Seconds.Text = string.Format("{0:d2}", m_initialTimeSpan.Seconds);
        }
    }
}
