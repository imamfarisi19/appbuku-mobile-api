using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppBuku.Models.Data
{
    public class ReviewBukuProcessing
    {
        private string dbFilename;
        private string namaTabel = "reviewbuku";
        
        public ReviewBukuProcessing(string filename)
        {
            dbFilename = filename;
        }

        public List<ReviewBuku> GetAll()
        {
            // Buat koneksi database
            using (var db = new LiteDatabase(dbFilename))
            {
                // ambil koleksi
                var col = db.GetCollection<ReviewBuku>(namaTabel);
                return col.FindAll().ToList();
            }
        }

        public List<ReviewBuku> GetByBukuId(int bukuId)
        {
            // Buat koneksi database
            using (var db = new LiteDatabase(dbFilename))
            {
                // ambil koleksi
                var col = db.GetCollection<ReviewBuku>(namaTabel);
                return col.Query().Where(x => x.BukuId == bukuId).ToList();
            }
        }

        public ReviewBuku Get(int id)
        {
            // Buat koneksi database
            using (var db = new LiteDatabase(dbFilename))
            {
                // ambil koleksi
                var col = db.GetCollection<ReviewBuku>(namaTabel);
                return col.FindById(id);
            }
        }
        
        // boleh void, boleh pakai bool 
        public string Insert(ReviewBuku reviewBuku)
        {
            try
            {
                // Buat koneksi database
                using (var db = new LiteDatabase(dbFilename))
                {
                    // ambil koleksi
                    var col = db.GetCollection<ReviewBuku>(namaTabel);
                    // lakukan proses insert
                    var id = col.Insert(reviewBuku);
                    return "S:" + id.ToString();
                }
            }
            catch (Exception ex)
            {
                return "E:" + ex.Message;
            }
        }

        public string Update(ReviewBuku reviewBuku)
        {
            try
            {
                // Buat koneksi database
                using (var db = new LiteDatabase(dbFilename))
                {
                    // ambil koleksi
                    var col = db.GetCollection<ReviewBuku> (namaTabel);

                    bool ada = col.Update(reviewBuku);
                    if (!ada)
                        return "E:Not Found";
                    else
                        return "S:" + reviewBuku.Id.ToString();
                }
            }
            catch (Exception ex)
            {
                return "E:" + ex.Message;
            }
        }

        public string Delete(int id)
        {
            try
            {
                // Buat koneksi database
                using (var db = new LiteDatabase(dbFilename))
                {
                    // ambil koleksi
                    var col = db.GetCollection<ReviewBuku>(namaTabel);

                    bool ada = col.Delete(id);
                    if (!ada)
                    {
                        return "E:Not Found";
                    }
                    else
                    {
                        return "S:" + id.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                return "E:" + ex.Message;
            }
        }
    }


}
