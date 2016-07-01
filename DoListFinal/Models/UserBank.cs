using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoListFinal.Models
{
    public class UserBank
    {

        public int ID { get; set; }
        public string AppUserID { get; set; }
        //public Dictionary<string, List<List_Items>> user_dict = new Dictionary<string, List<List_Items>>();
        //public virtual List<List_Items> udict {get; set;} 

        public string Item_indexes { get; set; }
    }
}