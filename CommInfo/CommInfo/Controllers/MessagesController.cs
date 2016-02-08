using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CommInfo.Models;

namespace CommInfo.Controllers
{
    public class MessagesController : Controller
    {
        private CommInfoContext db = new CommInfoContext();

        // GET: Messages
        public ActionResult Index()
        {
            return View(GetThreadsAndMessages(0));
            //return View(db.Messages.ToList());
        }

        // GET: Messages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MessageViewModel message = GetMessageAndThread(id);
            //Message message = db.Messages.Find(id);


            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // GET: Messages/Create
        public ActionResult Create()
        {
            ViewBag.FromList =
                new SelectList(db.Messages.OrderBy(m => m.From), "MessageID", "From");

            ViewBag.ThreadList =
                new SelectList(db.Threads.OrderBy(t => t.Topic), "ThreadID", "Topic");

            return View();
        }

        // POST: Messages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MessageID,Date,From,Subject,Body, ThreadItem, ThreadList, FromList")] MessageViewModel messageVM, int? ThreadList, int FromList)
        {
            if (ModelState.IsValid)
            {
                Message messageFrom = (from m in db.Messages
                                   where m.MessageID == FromList
                                   select m).FirstOrDefault();

                Thread thread = (from t in db.Threads
                                 where t.ThreadID == ThreadList
                                 select t).FirstOrDefault();

                if (thread == null)
                {
                    db.Threads.Add(new Thread { Topic = messageVM.ThreadItem.Topic }); //
                }

                Message message = new Message()
                {
                    MessageID = messageVM.MessageID,
                    Date = messageVM.Date,
                    From = messageVM.From,
                    Subject = messageVM.Subject,
                    ThreadID = messageVM.ThreadID
                };

                db.Messages.Add(message);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(messageVM);
        }

        // GET: Messages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // POST: Messages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MessageID,Date,To,From,Subject,Body")] Message message)
        {
            if (ModelState.IsValid)
            {
                db.Entry(message).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(message);
        }

        // GET: Messages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Message message = db.Messages.Find(id);
            db.Messages.Remove(message);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        // GET: Messages/Search
        public ActionResult Search()
        {
            return View();
        }


        // POST: Messages/Search
        [HttpPost]
        public ActionResult Search(string searchTerm)
        {
            List<MessageViewModel> messageVMs = new List<MessageViewModel>();
            // Get a list of messages whose Subject matches the search term
            var messages = from m in db.Messages
                           where m.Subject.Contains(searchTerm)
                               select m;

            //
            foreach (Message m in messages)
            {
                // get the threads
                var thread = (from t in db.Threads
                              where t.ThreadID == m.ThreadID
                              select t).FirstOrDefault();
                // Create a view model for the message and put the list of view models
                messageVMs.Add(new MessageViewModel() { Subject = m.Subject,
                                                        ThreadItem = thread,
                                                        Date = m.Date,
                                                        From = m.From,
                                                        Body = m.Body,
                                                        MessageID = m.MessageID
                });
            }

            // if there is only one message, display it
            if (messageVMs.Count == 1)
            {
                return View("Details", messageVMs[0]);
            }

            // if there are more than one message, put it in a list
            else
            {
                return View("Index", messageVMs);
            }

        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // ///////////////////////////////////////////////////////
        private List<MessageViewModel> GetThreadsAndMessages(int? messageId)
        {
            var messages = new List<MessageViewModel>();

            var threads = from thread in db.Threads.Include("Messages")
                         select thread;

            foreach (Thread t in threads)
            {
                foreach (Message m in t.Messages)
                {
                    if (m.MessageID == messageId || 0 == messageId)
                    {
                        var messageVM = new MessageViewModel();
                        messageVM.Date = m.Date;
                        messageVM.MessageID = m.MessageID;
                        messageVM.From = m.From;
                        messageVM.Subject = m.Subject;
                        messageVM.ThreadItem = t;
                        messages.Add(messageVM);
                    }
                }
            }
            return messages;
        }

        // for Message Details; returns a single message
        private MessageViewModel GetMessageAndThread(int? messageId)  
        {
            MessageViewModel MessageVm = (from m in db.Messages
                                    join t in db.Threads on m.ThreadID equals t.ThreadID
                                    where m.MessageID == messageId
                                    select new MessageViewModel
                                    {
                                        MessageID = m.MessageID,
                                        Date = m.Date,
                                        From = m.From,
                                        Subject = m.Subject,
                                        ThreadItem = t
                                    }).FirstOrDefault();
            return MessageVm;
        }

    }
}
