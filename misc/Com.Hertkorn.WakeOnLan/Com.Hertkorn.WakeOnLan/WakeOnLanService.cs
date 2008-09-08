using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Text.RegularExpressions;
using Com.Hertkorn.WakeOnLan.DO;

namespace Com.Hertkorn.WakeOnLan
{
    public class WakeOnLanService
    {
        public void SendWakeOnLanToMac(string mac)
        {
            MacAddress address = new MacAddress(mac);

            WakeOnLan(address);
        }

        private static readonly byte[] START_SIGNAL = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };

        private static void WakeOnLan(MacAddress macAddress)
        {
            // Das WOL Signal wird als Broadcast verschickt.
            // Es enthält 6x 0xFF, direkt danach folgt 16x die MAC Adresse.

            using (UdpClient client = new UdpClient())
            {
                client.Connect(IPAddress.Broadcast, 0);

                byte[] wolSignal = new byte[6 + MacAddress.MAC_ADDRESS_LENGTH * 16];

                // Startsignal einfügen
                START_SIGNAL.CopyTo(wolSignal, 0);

                // Die Mac-Adresse wird 16x in das WOL Signal angehängt
                for (int i = 0; i < 16; i++)
                {
                    macAddress.Address.CopyTo(wolSignal, START_SIGNAL.Length + i * MacAddress.MAC_ADDRESS_LENGTH);
                }

                // Signal senden
                client.Send(wolSignal, wolSignal.Length);
            }
        }
    }
}
