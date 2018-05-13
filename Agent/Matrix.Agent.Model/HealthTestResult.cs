using System;

namespace Matrix.Agent.Model
{
    public class HealthTestResult
    {
        public Exception Exception { get; set; }

        public string Text { get; set; }

        public bool Status { get; set; }
    }
}