//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Project_NotesDeFrais.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Projects
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Projects()
        {
            this.Expanses = new HashSet<Expanses>();
        }
    
        public System.Guid Project_ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Budget { get; set; }
        public System.Guid Customer_ID { get; set; }
        public System.Guid Pole_ID { get; set; }
    
        public virtual Customers Customers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Expanses> Expanses { get; set; }
        public virtual Poles Poles { get; set; }
    }
}