namespace AracServis.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Islem")]
    public partial class Islem
    {
        public int islemID { get; set; }

        public int? islemTurID { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? girisTarihi { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? cikisTarihi { get; set; }

        public string aciklama { get; set; }

        public int? aracBilgiID { get; set; }

        public virtual AracBilgi AracBilgi { get; set; }

        public virtual IslemTur IslemTur { get; set; }
    }
}
