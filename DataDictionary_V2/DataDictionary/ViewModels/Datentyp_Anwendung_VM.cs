using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataDictionary.ViewModels
{
    public class Datentyp_Anwendung_VM
    {
        public int DatentypID { get; set; }
        public string DatentypName { get; set; }
        public bool Assigned { get; set; }
    }
}