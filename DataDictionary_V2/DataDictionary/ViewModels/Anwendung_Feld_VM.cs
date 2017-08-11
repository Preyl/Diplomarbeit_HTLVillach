using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataDictionary.ViewModels
{
    public class Anwendung_Feld_VM
    {
        public int FeldID { get; set; }
        public string FeldName { get; set; }
        public bool Assigned { get; set; }
    }
}