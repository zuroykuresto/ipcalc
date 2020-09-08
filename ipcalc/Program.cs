using IPCalc;
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ipcalc
{

    class Program
    {

        static void Main(string[] args)
        {
            if (args.Length == 0) return;

            char[] delimIpCidr = { '/' };
            int cidr;
            string[] words = args[0].Split(delimIpCidr);
            if (words.Length == 2)
            {
                
                Regex ipregx = new Regex(@"^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])$");
                MatchCollection result = ipregx.Matches(words[0]);
                if (result.Count > 0)
                {
                    char[] delimIp = { '.' };
                    string[] ipS = words[0].Split(delimIp);
                    InternetProtocolAddress ip = new InternetProtocolAddress(byte.Parse(ipS[0]), byte.Parse(ipS[1]), byte.Parse(ipS[2]), byte.Parse(ipS[3]));
                    if (Int32.TryParse(words[1], out cidr))
                    {
                        cidr = Convert.ToInt32(words[1]);
                        if (cidr < 32)
                        {
                            IPCalculation ipc = new IPCalculation(ip, byte.Parse(Convert.ToString(cidr)));
                            Console.Write($"Network address: {ipc.getNetworkAddress()}\n");
                            Console.Write($"Network mask: {ipc.getNetmask()}\n");
                            Console.Write($"Totallys host available {ipc.getHostnumber()}, {ipc.getfirstAddress()} - {ipc.getLastAddress()}\n");
                            Console.Write($"Broudcast address: {ipc.getBroadcastAddress()}\n");
                        }
                        else Console.WriteLine("Wrong CIDR must be in dound of x.x.x.x/32");
                    }
                    else Console.WriteLine("Wrong CIDR must be in dound of x.x.x.x/32");
                }
                else Console.WriteLine("Wrong IP");
            }
            else Console.WriteLine("Wrong string format, use: \"xxx.xxx.xxx.xxx/xx\"\n Example: 192.168.0.1/24");
            Console.ReadLine();
        }
    }
}
