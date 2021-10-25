using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Books.Domain
{
    public class MqMessage
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime Created { get; set; }
    }
}
