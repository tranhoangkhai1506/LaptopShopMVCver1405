﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LaptopShopMVC.Email
{
    public class EmailViewModels
    {
        [Required]
        public string EmailSubject { get; set; }

        public string EmailBody { get; set; }

        [Required]
        public string ReceiverEmailAddress { get; set; }
        public string SenderEmailAddress { get; set; }
        public string SenderPassword { get; set; }
        public string SmtpHostServer { get; set; }
        public int SmtpPort { get; set; }
    }
}