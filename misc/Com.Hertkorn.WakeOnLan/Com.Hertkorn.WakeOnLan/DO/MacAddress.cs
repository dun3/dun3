using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Com.Hertkorn.WakeOnLan.DO
{
    public class MacAddress
    {
        public MacAddress(byte[] address)
        {
            if (!IsValidMAC(address)) { throw new ArgumentException("The byte array is not a valid MAC address", "address"); }

            m_address = address;
        }

        public MacAddress(string address)
        {
            if (!IsValidMAC(address)) { throw new ArgumentException("'{0}' is not a valid MAC address", "address"); }

            m_address = ConvertMacToBytes(address);
        }

        private readonly byte[] m_address;
        public byte[] Address
        {
            get { return m_address; }
        }

        public override string ToString()
        {
            return string.Format("{0:X2}-{1:X2}-{2:X2}-{3:X2}-{4:X2}-{5:X2}", m_address[0], m_address[1], m_address[2], m_address[3], m_address[4], m_address[5]);
        }

        private static byte[] ConvertMacToBytes(string mac)
        {
            string[] macSplit = mac.Split(':', '-');
            byte[] macAdresse = new byte[MAC_ADDRESS_LENGTH];

            for (int x = 0; x < macAdresse.Length; x++)
            {
                macAdresse[x] = byte.Parse(macSplit[x], NumberStyles.HexNumber);
            }
            return macAdresse;
        }

        private static readonly Regex MAC_ADDRESS_PATTERN = new Regex(@"(([A-Fa-f0-9]){2}[\:\-]){5}([A-Fa-f0-9]){2}", RegexOptions.Compiled);

        public static readonly int MAC_ADDRESS_LENGTH = 6;

        public static bool IsValidMAC(byte[] address)
        {
            return (address.Length == MAC_ADDRESS_LENGTH);
        }

        public static bool IsValidMAC(string mac)
        {
            return MAC_ADDRESS_PATTERN.IsMatch(mac);
        }
    }
}
