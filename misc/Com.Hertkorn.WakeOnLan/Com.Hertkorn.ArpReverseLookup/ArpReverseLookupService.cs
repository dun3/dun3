using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Com.Hertkorn.ArpReverseLookup
{
    public class ArpReverseLookupService
    {
        public List<string> GetAllKnownMacAddress()
        {
            Process processPing = new Process();

            ProcessStartInfo startinfoPing = new ProcessStartInfo("ping", "-n 2 -w 1000");
            startinfoPing.CreateNoWindow = true;
            startinfoPing.ErrorDialog = false;
            startinfoPing.UseShellExecute = false;
            startinfoPing.RedirectStandardError = true;
            startinfoPing.RedirectStandardOutput = true;

            processPing.StartInfo = startinfoPing;
            processPing.Start();

            processPing.WaitForExit(10000);

            string output = processPing.StandardOutput.ReadToEnd();
            string error = processPing.StandardError.ReadToEnd();

            Process process = new Process();

            ProcessStartInfo startinfo = new ProcessStartInfo("arp", "-a");
            startinfo.CreateNoWindow = true;
            startinfo.ErrorDialog = false;
            startinfo.UseShellExecute = false;
            startinfo.RedirectStandardError = true;
            startinfo.RedirectStandardOutput = true;

            process.StartInfo = startinfo;
            process.Start();

            process.WaitForExit(1000);
            string arpOutput = process.StandardOutput.ReadToEnd();

            return ParseArpOutput(arpOutput);
        }

        //private static readonly Regex VALID_LINE = new Regex(@"^\s*(?<ip>[0-9]{1-3}\.[0-9]{1-3}\.[0-9]{1-3}\.[0-9]{1-3})\s+(?<mac>[0-9a-f]{2}-[0-9a-f]{2}-[0-9a-f]{2}-[0-9a-f]{2}-[0-9a-f]{2}-[0-9a-f]{2})\s+.*$", RegexOptions.Compiled | RegexOptions.Multiline);
        private static readonly Regex VALID_LINE = new Regex(@"^\s*(?<ip>[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3})\s+(?<mac>[0-9a-f]{2}-[0-9a-f]{2}-[0-9a-f]{2}-[0-9a-f]{2}-[0-9a-f]{2}-[0-9a-f]{2})\s+.*$", RegexOptions.Compiled | RegexOptions.Multiline);

        // Interface: 172.28.53.219 --- 0x2
        // Internet Address      Physical Address      Type
        // 172.28.52.1           00-00-0c-07-ac-34     dynamic   
        // 172.28.52.2           00-13-5f-61-4b-fc     dynamic   
        // 172.28.52.3           00-1e-7a-b2-ae-c0     dynamic   
        // 172.28.52.30          00-04-75-c7-4b-17     dynamic   
        // 172.28.52.140         00-19-b9-2e-9a-ed     dynamic   
        private List<string> ParseArpOutput(string arpOutput)
        {
            MatchCollection splits = VALID_LINE.Matches(arpOutput);
            Dictionary<string, string> mapping = new Dictionary<string, string>();
            foreach (Match item in splits)
            {
                mapping.Add(item.Groups["mac"].Value, item.Groups["ip"].Value);
            }

            throw new NotImplementedException();
        }
    }
}
