using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project_NotesDeFrais.Models
{
    [Table("ExpanseReports")]
    public class ExpanseReportsModel
    {
        public ExpanseReportsModel()
        {
            this.Expanses = new HashSet<ExpansesModel>();
        }

        public System.Guid ExpanseReport_ID { get; set; }
        public System.Guid Employee_ID { get; set; }
        public System.Guid Author_ID { get; set; }

        [Display(Name = "Date de création")]
        public System.DateTime CreationDate { get; set; }

        [Display(Name = "Année")]
        public int Year { get; set; }

        [Display(Name = "Mois")]
        public int Month { get; set; }

        [Display(Name = "Statut")]
        public int StatusCode { get; set; }

        // ?
        public Nullable<System.DateTime> ManagerValidationDate { get; set; }

        // ?
        public Nullable<System.DateTime> AccountingValidatationDate { get; set; }

        [Display(Name = "Total hors taxe")]
        public double Total_HT { get; set; }

        [Display(Name = "Total TVA")]
        public double Total_TVA { get; set; }

        [Display(Name = "Total TTC")]
        public double Total_TTC { get; set; }

        [Display(Name = "Commentaire du manager")]
        public string ManagerComment { get; set; }

        [Display(Name = "Commentaire du comptable")]
        public string AccountingComment { get; set; }

        public virtual EmployeesModel Employees { get; set; }
        public virtual EmployeesModel Employees1 { get; set; }
        public virtual ICollection<ExpansesModel> Expanses { get; set; }
    }
}