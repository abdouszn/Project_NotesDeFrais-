using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project_NotesDeFrais.Models
{
    [Table("Customers")]
    public class CustomersModel
    {
        public CustomersModel()
        {
            this.Expanses = new HashSet<ExpansesModel>();
            this.Projects = new HashSet<ProjectsModel>();
        }

        public System.Guid Customer_ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

      
        public virtual ICollection<ExpansesModel> Expanses { get; set; }
       
   
        public virtual ICollection<ProjectsModel> Projects { get; set; }
    }
}