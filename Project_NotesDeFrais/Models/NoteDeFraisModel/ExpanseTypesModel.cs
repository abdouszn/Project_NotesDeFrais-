using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project_NotesDeFrais.Models
{
    [Table("ExpanseTypes")]
    public class ExpanseTypesModel
    {
        public ExpanseTypesModel()
        {
            this.Expanses = new HashSet<ExpansesModel>();
        }

        public System.Guid ExpenseType_ID { get; set; }
        public string Name { get; set; }
        public Nullable<double> Ceiling { get; set; }
        public bool Fixed { get; set; }
        public bool OnlyManagers { get; set; }
        public System.Guid Tva_ID { get; set; }

        public virtual ICollection<ExpansesModel> Expanses { get; set; }
        public virtual TvasModel Tvas { get; set; }
    }
}