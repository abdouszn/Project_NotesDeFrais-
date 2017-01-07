using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project_NotesDeFrais.Models
{
    [Table("Poles")]
    public class PolesModel
    {
        public PolesModel()
        {
            this.Employees = new HashSet<EmployeesModel>();
            this.Projects = new HashSet<ProjectsModel>();
        }

        public System.Guid Pole_ID { get; set; }

        [Display(Name = "Nom du pôle")]
        [Required(ErrorMessage = "champs obligatoire")]
        [StringLength(20)]
        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "le nom ne doit contenir que des caracteres.")]
        public string Name { get; set; }

        public System.Guid Manager_ID { get; set; }

        public virtual ICollection<EmployeesModel> Employees { get; set; }
        public virtual EmployeesModel Employees1 { get; set; }
        public virtual ICollection<ProjectsModel> Projects { get; set; }

        public List<AspNetUsers> Manager { get; set; }
    }
}