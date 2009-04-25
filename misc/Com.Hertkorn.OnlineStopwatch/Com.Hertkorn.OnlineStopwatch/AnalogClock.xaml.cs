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

namespace Com.Hertkorn.OnlineStopwatch
{
    public partial class AnalogClock : UserControl
    {
        public int Hour
        {
            get { return (int)this.GetValue(HourProperty); }
            set { this.SetValue(HourProperty, value); }
        }
        public static readonly DependencyProperty HourProperty = DependencyProperty.Register("Hour", typeof(int), typeof(AnalogClock), new PropertyMetadata(0));

        public int Minute
        {
            get { return (int)this.GetValue(MinuteProperty); }
            set { this.SetValue(MinuteProperty, value); }
        }
        public static readonly DependencyProperty MinuteProperty = DependencyProperty.Register("Minute", typeof(int), typeof(AnalogClock), new PropertyMetadata(0));

        public int Second
        {
            get { return (int)this.GetValue(SecondProperty); }
            set { this.SetValue(SecondProperty, value); }
        }
        public static readonly DependencyProperty SecondProperty = DependencyProperty.Register("Second", typeof(int), typeof(AnalogClock), new PropertyMetadata(0));

        public int Millisecond
        {
            get { return (int)this.GetValue(MillisecondProperty); }
            set { this.SetValue(MillisecondProperty, value); }
        }
        public static readonly DependencyProperty MillisecondProperty = DependencyProperty.Register("Millisecond", typeof(int), typeof(AnalogClock), new PropertyMetadata(0));
        
        private bool m_isReverse = false;
        public bool IsReverse
        {
            get { return m_isReverse; }
            set { m_isReverse = value; }
        }

        public AnalogClock()
        {
            InitializeComponent();
        }

        public void Canvas_Loaded(object sender, EventArgs e)
        {
            // Find the appropriate angle (in degrees) for the hour hand
            // based on the current time.
            double hourangle = (((double)Hour) / 12) * 360 + Minute / 2;

            // The transform is already rotated 116.5 degrees to make the hour hand be
            // in the 12 o'clock position. You must build this already existing angle
            // into the hourangle.
            hourangle += 180;

            // The same as for the minute angle.
            double minangle = (((double)Minute) / 60) * 360;
            minangle += 180;

            // The same for the second angle.
            double secangle = (((double)Second) / 60) * 360;
            secangle += 180;

            // The same for the millisecond angle.
            double millisecangle = (((double)Millisecond) / 1000) * 360;
            secangle += 180;

            // Set the beginning of the animation (From property) to the angle 
            // corresponging to the current time.
            hourAnimation.From = hourangle;

            // Set the end of the animation (To property)to the angle 
            // corresponding to the current time PLUS 360 degrees. Thus, the
            // animation will end after the clock hand moves around the clock 
            // once. Note: The RepeatBehavior property of the animation is set
            // to "Forever" so the animation will begin again as soon as it completes.

            int angle = 360;
            if (IsReverse)
            {
                angle = -angle;
            }

            hourAnimation.To = hourangle + angle;

            // Same as with the minute animation.
            minuteAnimation.From = minangle;
            minuteAnimation.To = minangle + angle;

            // Same as with the second animation.
            secondAnimation.From = secangle;
            secondAnimation.To = secangle + angle;

            // Same as with the millisecond animation.
            millisecondAnimation.From = millisecangle;
            millisecondAnimation.To = millisecangle + angle;

            // Start the storyboard.
            clockStoryboard.Begin();
        }
    }
}
