using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DoListFinal.Models
{
    public class List_Items
    {

        public int ID { get; set; }
        public string Description { get; set; }
        [Display(Name ="Priority (1 to 4)")]
        public int priority { get; set; }
        public string User_ID { get; set; }
        [Display(Name = "Complete")]
        public bool is_complete { get; set; }




    }
}