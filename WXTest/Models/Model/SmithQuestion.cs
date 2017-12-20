using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoModule.Models
{
    public class SmithQuestion
    {
        public string QType { get; set; }
        public string[] Items
        {
            get; set;
        }
    }
}