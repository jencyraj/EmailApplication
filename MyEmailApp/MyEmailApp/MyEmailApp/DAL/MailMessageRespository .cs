using Lime.Domain;
using MyEmailApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MyEmailApp.DAL
{
    public class MailMessageRespository : IMailMessageRepository
    {
        private static List<Lime.Domain.MailMessage> mailMessages;

        public MailMessageRespository()
        {
            if (mailMessages == null)
                LoadJson();
        }
        public bool Delete(int id)
        {
            if (mailMessages.Exists(s=>s.Id == id))
            {
                mailMessages.Remove(mailMessages.Where(s => s.Id == id).FirstOrDefault());
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<Lime.Domain.MailMessage> Find(Expression<Func<Lime.Domain.MailMessage, bool>> filter)
        {
            return mailMessages.Where(filter.Compile());
        }

        public IEnumerable<Lime.Domain.MailMessage> GetAll()
        {
            return mailMessages.Take(100);
        }

        public Lime.Domain.MailMessage GetById(int id)
        {
           
            return mailMessages.Where(s=>s.Id == id).FirstOrDefault();
        }

        public void LoadJson()
        {
            mailMessages = new List<Lime.Domain.MailMessage>();
            using (StreamReader r = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/mail-messages.json")))
            {
                string json = r.ReadToEnd();
                var localModel = JsonConvert.DeserializeObject<MailMessageList>(json);

                foreach(var message in localModel.MailMessage)
                {
                    Lime.Domain.MailMessage msg = new Lime.Domain.MailMessage();
                    msg.Id = message.Id;
                    msg.IsSent = message.IsSent == 0 ? true:false;
                    msg.PublicationId = message.PublicationId;
                    msg.Name = message.Name;
                    msg.ModifiedDate = message.ModifiedDate;
                    mailMessages.Add(msg);
                }
            }
        }
    }
}