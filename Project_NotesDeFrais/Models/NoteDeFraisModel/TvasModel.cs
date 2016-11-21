using System;
using System.Collections.Generic;
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
        public string Name { get; set; }
        public double Value { get; set; }
        public virtual ICollection<ExpanseTypesModel> ExpanseTypes { get; set; }
    }
}