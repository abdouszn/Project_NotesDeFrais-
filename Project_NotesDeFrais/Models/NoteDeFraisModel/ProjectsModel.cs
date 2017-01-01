using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project_NotesDeFrais.Models
{
    [Table("Projects")]
    public class ProjectsModel
    {
        public ProjectsModel()
        {
            this.Expanses = new HashSet<ExpansesModel>();
        }

        public System.Guid Project_ID { get; set; }

        [Display(Name = "Nom")]
        [StringLength(20)]
        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "le nom ne doit contenir que des caracteres.")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        
        public string Description { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "veuillez inserer une double")]
        public double Budget { get; set; }
        public System.Guid Customer_ID { get; set; }
        public System.Guid Pole_ID { get; set; }

        public virtual CustomersModel Customers { get; set; }
        public virtual ICollection<ExpansesModel> Expanses { get; set; }
        public virtual PolesModel Poles { get; set; }
        public virtual List<Customers> CustomersList { get; set; }
        public virtual List<Poles> PolesList { get; set; }
    }
}