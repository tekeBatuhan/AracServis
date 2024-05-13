namespace AracServis.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AracBilgi")]
    public partial class AracBilgi
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AracBilgi()
        {
            Islem = new HashSet<Islem>();
        }

        public int aracBilgiID { get; set; }

        [StringLength(50)]
        public string marka { get; set; }

        [StringLength(50)]
        public string model { get; set; }

        [StringLength(50)]
        public string sasiNo { get; set; }

        [StringLength(50)]
        public string plaka { get; set; }

        public int musteriID { get; set; }

        public virtual Musteri Musteri { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Islem> Islem { get; set; }
    }
}
