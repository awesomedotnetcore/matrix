using Newtonsoft.Json;
using NLog;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Matrix.Extension
{
    public static class LoggerExtension
    {
        public static void Begin(this ILogger o, [CallerMemberName] string method = null)
        {
            o.Trace(GetTrace(method, "BEGIN"));
        }

        public static void End(this ILogger o, [CallerMemberName] string method = null)
        {
            o.Trace(GetTrace(method, "END"));
        }

        public static void Exception(this ILogger o, Exception e, [CallerMemberName] string method = null)
        {
            o.Trace(GetTrace(method, "ERROR"));
            o.Error(e);
        }

        private static string GetTrace(string method, string status)
        {
            var result = string.Empty;

            var stack = new StackFrame(2);

            var name = $"{stack.GetMethod().DeclaringType.DeclaringType.FullName}.{method}";

            result = JsonConvert.SerializeObject(new { status, name });

            return result;
        }
    }
}