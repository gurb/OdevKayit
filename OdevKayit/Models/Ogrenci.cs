using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace OdevKayit.Models
{
    public partial class Ogrenci
    {
        [Key]
        public int Id { get; set; }
        public string Isim { get; set; }
        public string Soyisim { get; set; }
        public int? OdevId { get; set; }

        public virtual Odev Odev { get; set; }
    }
}
