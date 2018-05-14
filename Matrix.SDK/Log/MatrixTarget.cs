using Matrix.Threading;
using NLog;
using NLog.Config;
using NLog.Targets;
using System;

namespace Matrix.SDK.Log
{
    [Target("Matrix")]
    public sealed class MatrixTarget : TargetWithLayout
    {
        public static void Register()
        {
            Register<MatrixTarget>("Matrix");
        }

        [RequiredParameter]
        public Guid Application { get; set; }

        [RequiredParameter]
        public string Network { get; set; }

        [RequiredParameter]
        public string Endpoint { get; set; }

        private XLog Logger { get; set; }

        public MatrixTarget()
        {
            Logger = new XLog(Application, Network, Endpoint);
        }

        protected override void Write(LogEventInfo log)
        {
            var message = Layout.Render(log);

            if (!string.IsNullOrEmpty(message))
            {
                switch (log.Level.Name)
                {
                    default:
                    case "Trace":
                        Async.Execute(() => Logger.Trace(message));
                        break;

                    case "Info":
                        Async.Execute(() => Logger.Info(message));
                        break;

                    case "Debug":
                        Async.Execute(() => Logger.Debug(message));
                        break;

                    case "Warn":
                        Async.Execute(() => Logger.Warn(message));
                        break;

                    case "Error":
                    case "Fatal":
                    case "Critical":
                        Async.Execute(() => Logger.Error(message));
                        break;
                }
            }
        }
    }
}