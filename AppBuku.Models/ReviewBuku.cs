using System;
using System.Collections.Generic;
using System.Text;

namespace AppBuku.Models
{
    public class ReviewBuku
    {
        public int Id { get; set; }  // Primary key

        public DateTime WaktuInsert { get; set; }  // waktu update

        public int BukuId { get; set; }

        public string UserId { get; set; }  // akan otomatis diperbahrui sesuai login ke sistem

        public string Nama { get; set; }  // Silahkan input sendiri, 

        public int Rating { get; set; }

        public string IsiReview { get; set; }

    }
}
