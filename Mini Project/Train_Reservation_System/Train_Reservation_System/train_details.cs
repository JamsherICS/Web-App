//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Train_Reservation_System
{
    using System;
    using System.Collections.Generic;
    
    public partial class train_details
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public train_details()
        {
            this.booked_ticket = new HashSet<booked_ticket>();
            this.canceled_ticket = new HashSet<canceled_ticket>();
            this.seat_availability = new HashSet<seat_availability>();
            this.train_classes = new HashSet<train_classes>();
        }
    
        public decimal trainNo { get; set; }
        public string trainName { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Status { get; set; }
        public bool isDeleted { get; set; }
        public string FromTiming { get; set; }
        public string ToTiming { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<booked_ticket> booked_ticket { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<canceled_ticket> canceled_ticket { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<seat_availability> seat_availability { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<train_classes> train_classes { get; set; }
    }
}
