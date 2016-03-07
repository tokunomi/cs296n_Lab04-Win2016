using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommInfo.Models
{
    public class Member
    {
        List<Message> message = new List<Message>();
        public int MemberID { get; set; }
        public int MessageID { get; set; }
        public string NameFirst { get; set; }
        public string NameLast { get; set; }
        public string Username { get; set; }
        public string email { get; set; }
    }
}