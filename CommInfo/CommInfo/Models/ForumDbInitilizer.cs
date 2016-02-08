using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace CommInfo.Models
{
    //public class ForumDbInitilizer : DropCreateDatabaseAlways<CommInfoContext>
    public class ForumDbInitilizer : DropCreateDatabaseIfModelChanges<CommInfoContext>
    {
        Forum forum = new Forum();
        protected override void Seed(CommInfoContext context)
        {
            //DateTime seedDate = new DateTime().ToLongDateString(2016, 01, 01);
            DateTime seedDate = new DateTime(2016, 01, 01);

            // Fora seed
            Forum frm001 = new Forum { ForumID = 01, ForumName = "Seed 1" };
            Forum frm002 = new Forum { ForumID = 02, ForumName = "Seed 2" };

            // Thread ("Topics") seed
            Thread trd001 = new Thread { ThreadID = 01, Topic = "Test Topic 1" };
            Thread trd002 = new Thread { ThreadID = 02, Topic = "Test Topic 2"};

            // Messages seed
            Message msg01 = new Message { Date = seedDate, From = "John", Subject = "Did you see the game?" };
            Message msg02 = new Message { Date = seedDate, From = "David", Subject = "Meeting at Aiea Library this Friday" };
            Message msg03 = new Message { Date = seedDate, From = "Carrie", Subject = "Bookclub meeting this week?" };
            Message msg04 = new Message { Date = seedDate, From = "Nani", Subject = "Pearl Harbor Park Picnic" };
            trd001.Messages.Add(msg01);
            trd001.Messages.Add(msg02);
            trd001.Messages.Add(msg03);
            trd002.Messages.Add(msg04);
            frm001.Threads.Add(trd001);
            frm002.Threads.Add(trd002);
            context.Fora.Add(frm001);
            context.Fora.Add(frm002);
            //context.Threads.Add(trd001);
            //forum.Threads.Add(trd001);

            base.Seed(context);  // default
        }
    }
}