using System;

namespace Matrix.Messaging
{
    public class MsmqMessageEventArgs : EventArgs
    {
        public DateTime Timestamp { get; set; }

        public byte[] Data { get; set; }

        public MsmqMessageEventArgs(byte[] data)
        {
            Timestamp = DateTime.Now;
            Data = data;
        }
    }
}