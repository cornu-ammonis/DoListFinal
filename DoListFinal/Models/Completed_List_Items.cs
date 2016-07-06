using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoListFinal.Models
{
    public class Completed_List_Items : List_Items
    {

        public Completed_List_Items(string Descript, int prior, string UsId)
        {
            Description = Descript;
            priority = prior;
            User_ID = UsId;
            is_complete = true;
        }

        public Completed_List_Items()
        {

        }
    }
}