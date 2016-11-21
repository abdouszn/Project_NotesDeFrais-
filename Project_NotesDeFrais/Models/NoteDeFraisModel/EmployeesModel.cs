using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project_NotesDeFrais.Models
{
    [Table("Employees")]
    public class EmployeesModel
    {
        public EmployeesModel()
        {
            this.ExpanseReports = new HashSet<ExpanseReportsModel>();
            this.ExpanseReports1 = new HashSet<ExpanseReportsModel>();
            this.Poles1 = new HashSet<PolesModel>();
        }

        public System.Guid Employee_ID { get; set; }
        public string User_ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public Nullable<System.Guid> Pole_ID { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }
        public virtual PolesModel Poles { get; set; }
       
        public virtual ICollection<ExpanseReportsModel> ExpanseReports { get; set; }
        
        public virtual ICollection<ExpanseReportsModel> ExpanseReports1 { get; set; }
       
        public virtual ICollection<PolesModel> Poles1 { get; set; }
    }
}