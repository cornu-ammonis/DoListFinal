using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoListFinal.Models
{
    public class ViewModel
    {
        public IEnumerable<Uncompleted_List_Item> uncompleted_items { get; set; }
        public IEnumerable<Completed_List_Items> completed_items { get; set; }
    }
}