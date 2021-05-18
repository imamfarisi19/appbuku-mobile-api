using System;
using System.Collections.Generic;
using System.Text;

namespace AppBuku.Models
{
    public class Buku
    {
        public int Id { get; set; }  // Primary key
        
        public string Judul { get; set; }
        
        public string Penulis { get; set; }
        
        public string Penerbit { get; set; }
        
        public int Tahun { get; set; }
        
        public string UrlGambar { get; set; }

        public static List<Buku> Contoh()
        {
            List<Buku> hsl = new List<Buku>();
            var b1 = new Buku()
            {
                Judul = "Diary of a Wimpy Kid #15",
                Penerbit = "Amulet Books",
                Penulis = "Jeff Kinney",
                Tahun = 2020,
                UrlGambar = "http://books.google.com/books/content?id=GyNKzQEACAAJ&printsec=frontcover&img=1&zoom=1&source=gbs_api"
            };
            hsl.Add(b1);

            var b2 = new Buku()
            {
                Judul = "A First Weather Book for Kids",
                Penerbit = "Rockridge Press",
                Penulis = "Huda Harajli",
                Tahun = 2020,
                UrlGambar = "http://books.google.com/books/content?id=PvE1yQEACAAJ&printsec=frontcover&img=1&zoom=1&source=gbs_api"
            };
            hsl.Add(b2);

            var b3 = new Buku()
            {
                Judul = "Feathers, Not Just for Flying",
                Penerbit = "Charlesbridge Pub Incorporated",
                Penulis = "Melissa Stewart",
                Tahun = 2014,
                UrlGambar = "http://books.google.com/books/content?id=iO1KmAEACAAJ&printsec=frontcover&img=1&zoom=1&source=gbs_api"
            };
            hsl.Add(b3);

            return hsl;
        }
    }
}
