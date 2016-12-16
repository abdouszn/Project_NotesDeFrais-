using System;
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
        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "le prenom ne doit contenir que des caracteres.")]
        [StringLength(20)]
        [Required(ErrorMessage = "prenom obligatoire")]
        public string FirstName { get; set; }

        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "le nom ne doit contenir que des caracteres.")]
        [Display(Name = "Nom")]
        [Required(ErrorMessage = "nom obligatoire")]
        [StringLength(20)]
        //[RegularExpression(@"[a-zA-Z]*)", ErrorMessage = "Please enter a valid email address.")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "adresse mail obligatoire")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Display(Name = "Téléphone")]
        [Required(ErrorMessage = "numero de telephone obligatoire")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "le format est invalide.")]
        public string Telephone { get; set; }
        public Nullable<System.Guid> Pole_ID { get; set; }
        public virtual PolesModel Poles { get; set; }
       
        public virtual ICollection<ExpanseReportsModel> ExpanseReports { get; set; }
        
        public virtual ICollection<ExpanseReportsModel> ExpanseReports1 { get; set; }
       
        public virtual ICollection<PolesModel> Poles1 { get; set; }
        public virtual AspNetUsers AspNetUsers { get; set; }
    }
}