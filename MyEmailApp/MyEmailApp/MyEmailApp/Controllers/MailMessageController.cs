using Lime.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyEmailApp.Controllers
{
    public class MailMessageController : ApiController
    {
        private IMailMessageRepository mailMessageRepository;

        public MailMessageController()
        {
            this.mailMessageRepository = new DAL.MailMessageRespository();
        }
        public MailMessageController(IMailMessageRepository mailMessageRepository)
        {
            this.mailMessageRepository = mailMessageRepository;
        }

        // GET api/<controller>
        public IEnumerable<MailMessage> Get()
        {
            return mailMessageRepository.GetAll();
        }

        // GET api/<controller>/5
        [HttpGet]
        public MailMessage Get_id(int id)
        {
            return mailMessageRepository.GetById(id);
        }

        [HttpGet]
        public IEnumerable<MailMessage> Get(string Name)
        {
            return mailMessageRepository.Find(c => c.Name.ToLower().Contains(Name.ToLower()));
        }

        // PUT api/<controller>/5
        public void Put([FromBody] MailMessage mailMessage)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            bool isDeleted = mailMessageRepository.Delete(id);
        }
    }
}