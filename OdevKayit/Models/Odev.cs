using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace OdevKayit.Models
{
    public partial class Odev
    {
        public Odev()
        {
            Ogrenci = new HashSet<Ogrenci>();
        }
        [Key]
        public int Id { get; set; }
        public string Baslik { get; set; }
        public string Icerik { get; set; }
        public DateTime Baslangic { get; set; }
        public DateTime Bitis { get; set; }

        public virtual ICollection<Ogrenci> Ogrenci { get; set; }
    }
}
