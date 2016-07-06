using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoListFinal.Models
{
    public class Uncompleted_List_Item : List_Items
    {
         
        public Uncompleted_List_Item(string Descript, int prior, string UsId)
        {
            Description = Descript;
            priority = prior;
            User_ID = UsId;
            is_complete = false;

        }

        public Uncompleted_List_Item()
        {
            is_complete = false;
        }

    }
}