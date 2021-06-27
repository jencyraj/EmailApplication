using Lime.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MyEmailApp.Controllers
{
    public class HomeController : Controller
    {
        string apiUrl = "http://localhost:52267/api/MailMessage";
        public ActionResult Index()
        {
            List<MailMessage> mailMessages = GetMailMessages();
            return View(mailMessages);
        }

        private List<MailMessage> GetMailMessages()
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    webClient.BaseAddress = apiUrl;
                    var json = webClient.DownloadString("");
                    var list = JsonConvert.DeserializeObject<List<MailMessage>>(json);
                    return list.ToList();
                }
            }
            catch (WebException ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult Index(string name)
        {
            List<MailMessage> mailMessages = SearchMailMessages(name);
            return View(mailMessages);
        }

        private List<MailMessage> SearchMailMessages(string name)
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    webClient.QueryString.Add("Name", name);
                    webClient.BaseAddress = apiUrl;
                    string json = webClient.DownloadString("");
                    var list = JsonConvert.DeserializeObject<List<MailMessage>>(json);
                    return list.ToList();
                }
            }
            catch (WebException ex)
            {
                throw ex;
            }          
        }
        public ActionResult Delete(int id)
        {
            if(id==0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MailMessageController mailmsg = new MailMessageController();
            mailmsg.Delete(id);
            return RedirectToAction("Index");
        }
        public ActionResult Detail(int id)
        {
            List<MailMessage> mailMessages = GetmailById(id);
            return View(mailMessages);



        }
        public List<MailMessage> GetmailById(int id)
        {
            MailMessageController mailmsg = new MailMessageController();
            var mailMessage = mailmsg.Get_id(id);
            List<MailMessage> mailinfo = new List<MailMessage>();
            mailinfo.Add(new MailMessage { Id = mailMessage.Id, Name = mailMessage.Name, ModifiedDate = mailMessage.ModifiedDate, IsSent = mailMessage.IsSent });
            return mailinfo;
        }
       


    }
}
