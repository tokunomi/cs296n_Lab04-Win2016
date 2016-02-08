using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommInfo.Models
{
    public class MessageViewModel
    {
        Thread thread = new Thread();

        public int MessageID { get; set; }
        public int ThreadID { get; set; }
        public DateTime Date { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }  // redundant?
        public string Body { get; set; }

        public Thread ThreadItem 
        {
            get
            {
                return thread;
            }

            set
            {
                thread = value;
            }
        }
    }
}