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

        public override ApplicationDbContext toggle_complete(ApplicationDbContext working_db)
        {
            Completed_List_Items marked_complete = new Completed_List_Items(Description, priority, User_ID);
            working_db.List_Items.Remove(this);
            working_db.Completed_List_Items.Add(marked_complete);
            return working_db;
        }

    }
}