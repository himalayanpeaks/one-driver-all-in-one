using System;
using System.Collections.Generic;
using System.Management;

namespace OneDriver.Toolbox
{
    internal class ProcessConnection
    {
        public static ConnectionOptions ProcessConnectionOptions()

        {
#pragma warning disable CA1416 // Validate platform compatibility
            var options = new ConnectionOptions
            {
                Impersonation = ImpersonationLevel.Impersonate,
                Authentication = AuthenticationLevel.Default,
                EnablePrivileges = true
            };

            return options;
        }


        public static ManagementScope ConnectionScope(string machineName, ConnectionOptions options, string path)

        {
            var connectScope = new ManagementScope
            {
                Path = new ManagementPath(@"\\" + machineName + path),
                Options = options
            };
            connectScope.Connect();
            return connectScope;
        }
    }

    public class ComPortInfo

    {
        public string Name { get; set; }

        public string Description { get; set; }

        public static List<ComPortInfo> GetComPortsInfo()

        {
            var comPortInfoList = new List<ComPortInfo>();
            var options = ProcessConnection.ProcessConnectionOptions();
            var connectionScope =
                ProcessConnection.ConnectionScope(Environment.MachineName, options, @"\root\CIMV2");
            var objectQuery = new ObjectQuery("SELECT * FROM Win32_PnPEntity WHERE ConfigManagerErrorCode = 0");
            var comPortSearcher = new ManagementObjectSearcher(connectionScope, objectQuery);
            using (comPortSearcher)
            {
                foreach (ManagementObject obj in comPortSearcher.Get())
                {
                    if (obj != null)
                    {
                        var captionObj = obj["Caption"];
                        if (captionObj != null)
                        {
                            var caption = captionObj.ToString();
                            if (caption.Contains("(COM"))
                            {
                                var comPortInfo = new ComPortInfo
                                {
                                    Name = caption
                                    .Substring(caption.LastIndexOf("(COM", StringComparison.Ordinal))
                                    .Replace("(", string.Empty).Replace(")",
                                        string.Empty),
                                    Description = caption
                                };
                                comPortInfoList.Add(comPortInfo);
                            }
                        }
                    }
                }
            }

            return comPortInfoList;
        }
    }
#pragma warning restore CA1416 // Validate platform compatibility
}