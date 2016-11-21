﻿using System;
using System.Collections.Generic;
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
        public System.DateTime CreationDate { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int StatusCode { get; set; }
        public Nullable<System.DateTime> ManagerValidationDate { get; set; }
        public Nullable<System.DateTime> AccountingValidatationDate { get; set; }
        public double Total_HT { get; set; }
        public double Total_TVA { get; set; }
        public double Total_TTC { get; set; }
        public string ManagerComment { get; set; }
        public string AccountingComment { get; set; }

        public virtual EmployeesModel Employees { get; set; }
        public virtual EmployeesModel Employees1 { get; set; }
        public virtual ICollection<ExpansesModel> Expanses { get; set; }
    }
}