using EasyNetQ;
using System;
using System.Collections.Generic;
using System.Net;

namespace Matrix.Agent.Messages
{
    [Queue("HeartBeat", ExchangeName = "HeartBeat")]
    public class HeartBeat : Message
    {
        public string Hostname { get; set; }

        public List<string> Address { get; set; }

        public string OS { get; set; }

        public List<string> Information { get; set; }

        public List<string> Debug { get; set; }

        public List<string> Warnings { get; set; }

        public List<string> Errors { get; set; }

        public HeartBeat()
        {
            Information = new List<string>();
            Debug = new List<string>();
            Warnings = new List<string>();
            Errors = new List<string>();
            Address = new List<string>();

            Hostname = Environment.MachineName;
            OS = Environment.OSVersion.VersionString;

            foreach (var i in Dns.GetHostAddresses(Dns.GetHostName()))
                Address.Add(i.ToString());
        }

        public HeartBeat(Guid app)
            : base(app)
        {
            Information = new List<string>();
            Debug = new List<string>();
            Warnings = new List<string>();
            Errors = new List<string>();
            Address = new List<string>();

            Hostname = Environment.MachineName;
            OS = Environment.OSVersion.VersionString;

            foreach (var i in Dns.GetHostAddresses(Dns.GetHostName()))
                Address.Add(i.ToString());
        }
    }
}