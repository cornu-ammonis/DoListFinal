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

        public override ApplicationDbContext toggle_complete(ApplicationDbContext working_db)
        {
            Uncompleted_List_Item marked_complete = new Uncompleted_List_Item(Description, priority, User_ID);
            working_db.List_Items.Remove(this);
            working_db.Uncompleted_List_Items.Add(marked_complete);
            return working_db;

        }
    
    }
}