using System;
using System.IO;
using System.Messaging;
using System.Threading;

namespace Matrix.Messaging
{
    public class MsmqReader
    {
        public event EventHandler<MsmqMessageEventArgs> MessageReceived;

        public bool Listening { get; private set; }

        private MessageQueue Queue { get; set; }

        private Thread QueueMonitor { get; set; }

        public MsmqReader(string queue)
        {
            Queue = new MessageQueue(queue);

            QueueMonitor = new Thread(new ThreadStart(Listen));
        }

        public void Start()
        {
            Listening = true;
            QueueMonitor.Start();
        }

        public void Stop()
        {
            Listening = false;

            QueueMonitor.Join();
        }

        private void Listen()
        {
            try
            {
                while (Listening) { ReceiveMessage(); }
            }
            catch (Exception e)
            {
            }
        }

        private void ReceiveMessage()
        {
            try
            {
                if (Queue != null)
                {
                    Message msg = Queue.Receive(new TimeSpan(0, 0, 1)); // 1 sec timeout

                    if (msg != null)
                    {
                        msg.Formatter = new BinaryMessageFormatter();

                        byte[] data = new byte[(int)msg.BodyStream.Length];

                        using (StreamReader reader = new StreamReader(msg.BodyStream))
                            reader.BaseStream.Read(data, 0, (int)reader.BaseStream.Length);

                        if (MessageReceived != null)
                            MessageReceived(this, new MsmqMessageEventArgs(data));
                    }
                }
            }
            catch (MessageQueueException msmqEx)
            {
                if (msmqEx.ErrorCode != -2147467259) // time-out expired exception error code
                    throw;
            }
            catch (Exception e)
            {
            }
        }
    }
}