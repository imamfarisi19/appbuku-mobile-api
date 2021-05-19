using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppBuku.TMobileFromWeb.Services
{
    public class MyHttpClient
    {
        HttpClientHandler handler = new HttpClientHandler();
        HttpClient client;
        bool _isEnable = false;
        string _baseUri;
        string _webUsername;
        string _webPassword;

        public MyHttpClient(string baseUri)
        {
            _baseUri = baseUri;
            _webUsername = Application.Current.Properties["WebUsername"] as string;
            _webPassword = Application.Current.Properties["WebPassword"] as string;
           
            client = new HttpClient(handler);
            _isEnable = Task.Run(DoLogin).Result;
        }


        // Khusus untuk aplikasi standar Mukhaimy
        private async Task<bool> DoLogin()
        {
            Models.Userlogin userlogin =
                new Models.Userlogin() { Username = _webUsername, Password = _webPassword };
            string hslResponse = "";

            // apakah masih login / terkoneksi?
            var response = await client.GetAsync(_baseUri + "api/Auth/IsLogin");
            if (!response.IsSuccessStatusCode) return false;
            hslResponse = await response.Content.ReadAsStringAsync();
            if (hslResponse == "1")
                return true;

            // else .. lakukan proses login
            var dataAsString = JsonConvert.SerializeObject(userlogin);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            response = await client.PostAsync(_baseUri + "api/Auth/Login", content);
            if (!response.IsSuccessStatusCode) return false;
            hslResponse = await response.Content.ReadAsStringAsync();

            // uji login sekali lagi..
            response = await client.GetAsync(_baseUri + "api/Auth/IsLogin");
            if (!response.IsSuccessStatusCode) return false;
            hslResponse = await response.Content.ReadAsStringAsync();
            if (hslResponse == "1")
                return true;

            return false;
        }

        public bool IsEnable => _isEnable;

        public async Task<string> HttpGet(string path)
        {
            var response = await client.GetAsync(_baseUri + path);
            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> HttpGet(string path, string id)
        {
            if (!path.EndsWith("/"))
                path += "/";
            var response = await client.GetAsync(_baseUri + path + id);
            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadAsStringAsync();
        }


        public async Task<string> HttpPost(string path, object obj)
        {
            var dataAsString = JsonConvert.SerializeObject(obj);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(_baseUri + path, content);
            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> HttpPut(string path, string id, object obj)
        {
            if (!path.EndsWith("/"))
                path += "/";

            var dataAsString = JsonConvert.SerializeObject(obj);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(_baseUri + path + id, content);
            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> HttpDelete(string path, string id)
        {
            if (!path.EndsWith("/"))
                path += "/";

            var response = await client.DeleteAsync(_baseUri + path + id);
            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadAsStringAsync();
        }
    }
}
