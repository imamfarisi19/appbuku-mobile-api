using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppBuku.Models.Data
{

    public class BukuProcessing
    {
        private string dbFilename;
        private string namaTabel = "buku";
        
        public BukuProcessing(string filename)
        {
            dbFilename = filename;
        }

        public List<Buku> GetAll()
        {
            // Buat koneksi database
            using (var db = new LiteDatabase(dbFilename))
            {
                // ambil koleksi
                var col = db.GetCollection<Buku>(namaTabel);
                return col.FindAll().ToList();
            }
        }

        public Buku Get(int id)
        {
            // Buat koneksi database
            using (var db = new LiteDatabase(dbFilename))
            {
                // ambil koleksi
                var col = db.GetCollection<Buku>(namaTabel);
                return col.FindById(id);
            }
        }
        
        // boleh void, boleh pakai bool 
        public string Insert(Buku buku)
        {
            try
            {
                // Buat koneksi database
                using (var db = new LiteDatabase(dbFilename))
                {
                    // ambil koleksi
                    var col = db.GetCollection<Buku>(namaTabel);
                    // lakukan proses insert
                    var id = col.Insert(buku);
                    return "S:" + id.ToString();
                }
            }
            catch (Exception ex)
            {
                return "E:" + ex.Message;
            }
        }

        public string Update(Buku buku)
        {
            try
            {
                // Buat koneksi database
                using (var db = new LiteDatabase(dbFilename))
                {
                    // ambil koleksi
                    var col = db.GetCollection<Buku>(namaTabel);

                    bool ada = col.Update(buku);
                    if (!ada)
                        return "E:Not Found";
                    else
                        return "S:" + buku.Id.ToString();
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
                    var col = db.GetCollection<Buku>(namaTabel);

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


    //// Open database (or create if doesn't exist)
    //using(var db = new LiteDatabase(@"C:\Temp\MyData.db"))
    //{
    //    // Get a collection (or create, if doesn't exist)
    //    var col = db.GetCollection<Customer>("customers");
    //
    //    // Create your new customer instance
    //    var customer = new Customer
    //    { 
    //        Name = "John Doe", 
    //        Phones = new string[] { "8000-0000", "9000-0000" }, 
    //        IsActive = true
    //    };
    //	
    //    // Insert new customer document (Id will be auto-incremented)
    //    col.Insert(customer);
    //	
    //    // Update a document inside a collection
    //    customer.Name = "Jane Doe";
    //	
    //    col.Update(customer);
    //	
    //    // Index document using document Name property
    //    col.EnsureIndex(x => x.Name);
    //	
    //    // Use LINQ to query documents (filter, sort, transform)
    //    var results = col.Query()
    //        .Where(x => x.Name.StartsWith("J"))
    //        .OrderBy(x => x.Name)
    //        .Select(x => new { x.Name, NameUpper = x.Name.ToUpper() })
    //        .Limit(10)
    //        .ToList();
    //
    //    // Let's create an index in phone numbers (using expression). It's a multikey index
    //    col.EnsureIndex(x => x.Phones); 
    //
    //    // and now we can query phones
    //    var r = col.FindOne(x => x.Phones.Contains("8888-5555"));
    //}
}
