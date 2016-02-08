using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommInfo.Models
{
    public class Thread
    {
        List<Message> message = new List<Message>();

        public int ThreadID { get; set; }
        public List<Message> Messages 
        {
            get { return message; }
        }
        public string Topic { get; set; }
    }
}