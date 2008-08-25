using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Text.RegularExpressions;

namespace Com.Hertkorn.WakeOnLan
{
    public class WakeOnLanService
    {
        public void SendToMac(string mac)
        {
            if (!IsMAC(mac)) { throw new ArgumentException("'{0}' does not represent a valid MAC Address", "smac"); }

            //Umwandeln der MAC Adresse von String zu byte
            string[] macSplit = mac.Split(':');
            byte[] macAdresse = new byte[6];

            for (int x = 0; x < macAdresse.Length; x++)
            {
                macAdresse[x] = byte.Parse(macSplit[x], System.Globalization.NumberStyles.HexNumber);
            }

            WakeOnLan(macAdresse);
        }

        private static byte[] STARTSIGNAL = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };
        public static readonly Regex MAC_ADRESS_PATTERN = new Regex(@"(([a-f]|[0-9]|[A-F]){2}\:){5}([a-f]|[0-9]|[A-F]){2}\b", RegexOptions.Compiled);

        public static bool IsMAC(string mac)
        {
            return MAC_ADRESS_PATTERN.IsMatch(mac);
        }

        private static void WakeOnLan(byte[] macAddress)
        {
            //Das WOL Signal wird als Broadcast verschickt.
            //Es enthält 6x den Wert FF, direkt danach folgt 16x die MAC Adresse.

            using (UdpClient wolClient = new UdpClient())
            {
                wolClient.Connect(IPAddress.Broadcast, 0);

                //Startsignal 6x(FF) + 16x(Mac-Adresse) = 102bytes

                byte[] WOLSignal = new byte[102];

                // Startsignal einfügen
                STARTSIGNAL.CopyTo(WOLSignal, 0);

                // Die Mac-Adresse wird 16x in das WOL Signal angehängt
                for (int i = 0; i < 16; i++)
                {
                    macAddress.CopyTo(WOLSignal, (i + 1) * 6);
                }

                //Signal senden
                wolClient.Send(WOLSignal, WOLSignal.Length);
            }
        }
    }
}
