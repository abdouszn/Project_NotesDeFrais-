using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_NotesDeFrais.Models.NoteDeFraisModel
{
    public class RolesModel
    {

        
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
            public RolesModel()
            {
                this.AspNetUsers = new HashSet<AspNetUsers>();
            }

            public string Id { get; set; }
            public string Name { get; set; }

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
            public virtual ICollection<AspNetUsers> AspNetUsers { get; set; }
    

    }
}