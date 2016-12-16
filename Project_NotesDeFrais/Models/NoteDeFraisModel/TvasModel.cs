using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project_NotesDeFrais.Models
{
   [Table("Tvas")]
    public class TvasModel
    {
        public TvasModel()
        {
            this.ExpanseTypes = new HashSet<ExpanseTypesModel>();
        }

        public System.Guid TVA_ID { get; set; }

        [Display(Name = "Nom du produit")]
        [Required(ErrorMessage = "champs obligatoire")]
        [StringLength(20)]
        public string Name { get; set; }

        [Required(ErrorMessage = "champs obligatoire")]
        [Display(Name = "Valeur TVA du produit")]
        [Range(0, double.MaxValue, ErrorMessage = "veuillez inserer une double")]
        public double Value { get; set; }
        public virtual ICollection<ExpanseTypesModel> ExpanseTypes { get; set; }
    }
}