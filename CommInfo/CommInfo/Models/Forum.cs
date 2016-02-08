using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommInfo.Models
{
    public class Forum
    {
        List<Thread> threads = new List<Thread>();

        public int ForumID { get; set; }
        public string ForumName { get; set; }

        public List<Thread> Threads 
        { 
            get { return threads;} 
        }

    }
}