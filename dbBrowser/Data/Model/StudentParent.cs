//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace dbBrowser.Data.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class StudentParent
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StudentParent()
        {
            this.FamilyRelations = new HashSet<FamilyRelation>();
        }
    
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public System.DateTime Birthday { get; set; }
        public string Address { get; set; }
        public int Id { get; private set; }
        public GenderType Gender { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FamilyRelation> FamilyRelations { get; set; }
    }
}
