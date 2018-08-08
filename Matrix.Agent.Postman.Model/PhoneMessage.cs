using System;
using System.Collections.Generic;

namespace Matrix.Agent.Postman.Model
{
    public class PhoneMessage
    {
        public Guid Id { get; set; }

        public Guid Application { get; set; }

        public string From { get; set; }

        public List<string> To { get; set; }

        public string Message { get; set; }

        public PhoneMessage()
        {
            To = new List<string>();
        }
    }
}