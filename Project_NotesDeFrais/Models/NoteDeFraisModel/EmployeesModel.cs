﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
           
        }
        public System.Guid Employee_ID { get; set; }
        public string User_ID { get; set; }

        [Display(Name = "Prénom")]
        public string FirstName { get; set; }

        [Display(Name = "Nom")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Téléphone")]
        public string Telephone { get; set; }

        public Nullable<System.Guid> Pole_ID { get; set; }
        public virtual PolesModel Poles { get; set; }
       
        public virtual ICollection<ExpanseReportsModel> ExpanseReports { get; set; }
        
        public virtual ICollection<ExpanseReportsModel> ExpanseReports1 { get; set; }
       
        public virtual ICollection<PolesModel> Poles1 { get; set; }
        public virtual AspNetUsers AspNetUsers { get; set; }
    }
}