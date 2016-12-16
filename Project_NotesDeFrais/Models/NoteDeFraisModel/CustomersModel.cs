using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Display(Name = "Nom")]
        [StringLength(20)]
        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "le nom ne doit contenir que des caracteres.")]
        [Required(ErrorMessage = "nom obligatoire")]
        public string Name { get; set; }

        [StringLength(20)]
        [Required(ErrorMessage = "prenom obligatoire")]
        [Display(Name = "Code")]
        public string Code { get; set; }

      
        public virtual ICollection<ExpansesModel> Expanses { get; set; }
       
   
        public virtual ICollection<ProjectsModel> Projects { get; set; }
    }
}