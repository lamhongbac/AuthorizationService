using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLib.Models
{
    public class RequestModel
    {
        public RequestModel()
        {
            ID = 0;
            Number = " ";
        }
        public int ID { get; set; }
        public string Number { get; set; }
    }
}
