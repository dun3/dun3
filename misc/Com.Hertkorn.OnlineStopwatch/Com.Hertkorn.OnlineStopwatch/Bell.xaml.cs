using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Com.Hertkorn.OnlineStopwatch
{
	public partial class Bell
	{
		public Bell()
		{
			this.InitializeComponent();

			// Insert code required on object creation below this point.            
		}

        public void StartRing()
        {
            Reset();
            Ring.Begin();
        }

        public void Reset()
        {
            Ring.Stop();
            Ring.Seek(TimeSpan.Zero);
        }
	}
}