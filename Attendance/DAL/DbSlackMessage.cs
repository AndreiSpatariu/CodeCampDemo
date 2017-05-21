using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebChat.DAL
{
    public class DbSlackMessage
    {
        [Key]
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Text { get; set; }
    }
}
