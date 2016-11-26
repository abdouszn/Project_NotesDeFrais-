﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project_NotesDeFrais.Models
{
    [Table("Expanses")]
    public class ExpansesModel
    {
        public System.Guid Expanse_ID { get; set; }
        public System.Guid ExpanseReport_ID { get; set; }

        [Display(Name = "Jour")]
        public int Day { get; set; }

        public System.Guid ExpanseType_ID { get; set; }
        public System.Guid Customer_ID { get; set; }
        public System.Guid Project_ID { get; set; }

        [Display(Name = "Montant HT")]
        public double Amount_HT { get; set; }

        [Display(Name = "Montant TVA")]
        public double Amount_TVA { get; set; }

        [Display(Name = "Montant TTC")]
        public double Amount_TTC { get; set; }

        public virtual CustomersModel Customers { get; set; }
        public virtual ExpanseReportsModel ExpanseReports { get; set; }
        public virtual ExpanseTypesModel ExpanseTypes { get; set; }
        public virtual ProjectsModel Projects { get; set; }
    }
}