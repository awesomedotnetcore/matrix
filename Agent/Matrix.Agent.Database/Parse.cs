using System;

namespace Matrix.Agent.Database
{
    public static class Parse
    {
        public static bool Bool(dynamic o)
        {
            var result = false;

            if (o != null && !bool.TryParse(o.ToString(), out result))
            {
                string format = o?.ToString();

                if (!string.IsNullOrEmpty(format))
                    result = format.Equals("1");
            }

            return result;
        }

        public static Guid Guid(dynamic o)
        {
            var result = System.Guid.Empty;

            System.Guid.TryParse(o.ToString(), out result);

            return result;
        }

        public static DateTime DateTime(dynamic o)
        {
            var result = System.DateTime.MinValue;

            System.DateTime.TryParse(o.ToString(), out result);

            return result;
        }
    }
}