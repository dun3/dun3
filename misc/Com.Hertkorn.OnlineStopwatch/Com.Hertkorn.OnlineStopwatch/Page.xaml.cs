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
        TimeSpan m_initialTimeSpan = TimeSpan.Zero;
        TimeSpan m_remainingTimeSpan = TimeSpan.Zero;
        bool m_isSet = false;

        public Page()
        {
            InitializeComponent();

            m_requestUpdate.Tick += new EventHandler(m_requestUpdate_Tick);
            m_requestUpdate.Interval = new TimeSpan(0, 0, 0, 0, 100);

            SetUIState();
            SetUIToTimeSpan(TimeSpan.Zero);
        }

        void m_requestUpdate_Tick(object sender, EventArgs e)
        {
            lock (m_syncLock)
            {
                m_remainingTimeSpan = m_targetDateTime - DateTime.Now;

                if (IsNegative(m_remainingTimeSpan))
                {
                    m_requestUpdate.Stop();
                    SetUIState();

                    Ring.Play();

                    m_remainingTimeSpan = m_initialTimeSpan;
                    SetUIToTimeSpan(m_initialTimeSpan);
                }
                else
                {
                    SetUIToTimeSpan(m_remainingTimeSpan);
                }
            }
        }

        private bool IsNegative(TimeSpan timeSpan)
        {
            return timeSpan.CompareTo(TimeSpan.Zero) < 0;
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            SafeUpdate(() =>
            {
                return new TimeSpan(ParseHours(), ParseMinutes(), ParseSeconds());
            });

            SetUIToTimeSpan(m_remainingTimeSpan);
            m_targetDateTime = DateTime.Now.Add(m_remainingTimeSpan);

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
            Reset.IsEnabled = !m_requestUpdate.IsEnabled;

            Hours.IsReadOnly = m_requestUpdate.IsEnabled;
            Minutes.IsReadOnly = m_requestUpdate.IsEnabled;
            Seconds.IsReadOnly = m_requestUpdate.IsEnabled;

            if (!m_requestUpdate.IsEnabled)
            {
                Start.Focus();
            }
            else
            {
                Stop.Focus();
            }
        }
         
        private int ParseHours()
        {
            return ParseUserInput(Hours, 23);
        }

        private int ParseMinutes()
        {
            return ParseUserInput(Minutes, 59);
        }

        private int ParseSeconds()
        {
            return ParseUserInput(Seconds, 59);
        }

        private void SafeUpdate(Func<TimeSpan> createTimeSpan)
        {
            if (!m_requestUpdate.IsEnabled)
            {
                if (!m_isSet)
                {
                    try
                    {
                        lock (m_syncLock)
                        {
                            m_initialTimeSpan = createTimeSpan();
                            m_remainingTimeSpan = m_initialTimeSpan;
                        }
                    }
                    catch (Exception ex)
                    {
                        ErrorMessage.Text = ex.Message;
                    }
                    m_isSet = true;
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

        private void SetUIToTimeSpan(TimeSpan timeSpan)
        {
            Hours.Text = string.Format("{0:d2}", timeSpan.Hours);
            Minutes.Text = string.Format("{0:d2}", timeSpan.Minutes);
            Seconds.Text = string.Format("{0:d2}", timeSpan.Seconds);
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            m_remainingTimeSpan = TimeSpan.Zero;
            m_initialTimeSpan = TimeSpan.Zero;
            m_isSet = false;
            SetUIToTimeSpan(TimeSpan.Zero);
        }

        private void Hours_GotFocus(object sender, RoutedEventArgs e)
        {
            Hours.SelectAll();
        }

        private void Minutes_GotFocus(object sender, RoutedEventArgs e)
        {
            Minutes.SelectAll();
        }

        private void Seconds_GotFocus(object sender, RoutedEventArgs e)
        {
            Seconds.SelectAll();
        }

        private static object m_syncLock = new object();
        private void PlusOneMinute_Click(object sender, RoutedEventArgs e)
        {
            lock (m_syncLock)
            {
                m_isSet = true;
                m_initialTimeSpan = m_initialTimeSpan.Add(new TimeSpan(0, 1, 0));
                m_remainingTimeSpan = m_remainingTimeSpan.Add(new TimeSpan(0, 1, 0));
                m_targetDateTime = DateTime.Now.Add(m_remainingTimeSpan);
                SetUIToTimeSpan(m_remainingTimeSpan);
            }
        }
    }
}
