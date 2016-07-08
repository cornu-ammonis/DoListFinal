using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DoListFinal.Models
{
    public  class List_Items
    {

        public int ID { get; set; }
        public string Description { get; set; }
        [Display(Name ="Priority (1 to 4)")]
        public int priority { get; set; }
        public string User_ID { get; set; }
        [Display(Name = "Complete")]
        public bool is_complete { get; set; }

        public virtual ApplicationDbContext toggle_complete(ApplicationDbContext working)
        {
            return working;
        }
       
        public ApplicationDbContext implement_edit(ApplicationDbContext working_db)
        {
            if (this.is_complete == true)
            {
                Completed_List_Items complete_edited = new Completed_List_Items(this.Description, this.priority, this.User_ID);
                working_db.List_Items.Remove(working_db.List_Items.Find(this.ID));
                working_db.Completed_List_Items.Add(complete_edited);
                return working_db;

            }
            else if (this.is_complete == false)
            {
                Uncompleted_List_Item uncomplete_edited = new Uncompleted_List_Item(this.Description, this.priority, this.User_ID);
                working_db.List_Items.Remove(working_db.List_Items.Find(this.ID));
                working_db.Uncompleted_List_Items.Add(uncomplete_edited);
                return working_db;
            }
            return working_db;
        }
    }

    
}