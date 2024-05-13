namespace AracServis.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Kullanici")]
    public partial class Kullanici
    {
        public int kullaniciID { get; set; }

        [StringLength(50)]
        public string ad { get; set; }

        [StringLength(50)]
        public string soyad { get; set; }

        [StringLength(50)]
        public string eposta { get; set; }

        [StringLength(50)]
        public string sifre { get; set; }

        public int unvanID { get; set; }

        [StringLength(50)]
        public string telefon { get; set; }

        [StringLength(500)]
        public string adres { get; set; }

        [StringLength(50)]
        public string cinsiyet { get; set; }

        [StringLength(11)]
        public string kimlikNo { get; set; }

        public virtual Unvan Unvan { get; set; }
    }
}
