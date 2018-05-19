using Matrix.Collections.Generic;
using Matrix.Extension;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Matrix.Agent.Messages
{
    public class Message : IMessage
    {
        public Guid Id { get; set; }

        public Guid Application { get; set; }

        public DateTime Timestamp { get; set; }

        public string Program { get; set; }

        public string Version { get; set; }

        public DateTime Build { get; set; }

        public string Fingerprint { get; set; }

        public string Process { get; set; }

        public SerializableDictionary<string, string> Properties { get; set; }

        public List<string> Tags { get; set; }

        public Message()
        {
            Properties = new SerializableDictionary<string, string>();
            Tags = new List<string>();

            Id = Guid.NewGuid();
            Timestamp = DateTime.Now;
            Application = Guid.Empty;

            var asm = Assembly.GetEntryAssembly();

            if (asm != null)
            {
                Version = asm.GetName().Version.ToString(4);
                Build = new FileInfo(asm.Location).CreationTime;
                Fingerprint = File.ReadAllBytes(asm.Location).SHA256();
            }

            Program = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
            Process = System.Diagnostics.Process.GetCurrentProcess().Id.ToString();
        }

        public Message(Guid app)
        {
            Properties = new SerializableDictionary<string, string>();
            Tags = new List<string>();

            Id = Guid.NewGuid();
            Timestamp = DateTime.Now;
            Application = app;

            var asm = Assembly.GetCallingAssembly();

            if (asm != null)
            {
                Version = asm.GetName().Version.ToString(4);
                Build = new FileInfo(asm.Location).CreationTime;
                Fingerprint = File.ReadAllBytes(asm.Location).SHA256();
            }

            var ps = System.Diagnostics.Process.GetCurrentProcess();

            if (ps != null)
            {
                Program = ps.ProcessName;
                Process = ps.Id.ToString();
            }
        }
    }
}