using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEmailApp.Models
{
    public class MailMessage
    {
        public int Id { get; set; }
        public int IsSent { get; set; }
        public int PublicationId { get; set; }
        public string Name { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Status { get; set; }
    }
}