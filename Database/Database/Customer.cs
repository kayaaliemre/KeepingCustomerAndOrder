//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            this.Billing = new HashSet<Billing>();
            this.Setup = new HashSet<Setup>();
        }
    
        public int customer_id { get; set; }
        public string customer_name { get; set; }
        public string customer_surname { get; set; }
        public int customer_gender { get; set; }
        public string customer_adress { get; set; }
        public string customer_job { get; set; }
        public int customer_soft { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Billing> Billing { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual Softwares Softwares { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Setup> Setup { get; set; }
    }
}