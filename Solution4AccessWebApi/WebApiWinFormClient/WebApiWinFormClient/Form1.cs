using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebApiWinFormClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void GetAllBooks()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://localhost:44869/api/Books");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            ShowResult(JsonConvert.DeserializeObject<List<Book>>(responseBody));
        }

        private void ShowResult(List<Book> list)
        {
            listBox1.Items.Clear();
            for (int i = 0; i < list.Count; i++)
            {
                listBox1.Items.Add(list[i].Title);
            }
        }

        private void BtnGetAllBooks_Click(object sender, EventArgs e)
        {
            GetAllBooks();
        }

        //
        //LOOK>>https://docs.microsoft.com/en-us/aspnet/web-api/overview/advanced/calling-a-web-api-from-a-net-client
        //
        //private async void PostBook()
        //{
        //    HttpClient client = new HttpClient();
        //    HttpResponseMessage response = await client.PostAsync("http://localhost:44869/api/Books", null);
        //    response.EnsureSuccessStatusCode();
        //    string responseBody = await response.Content.ReadAsStringAsync();
        //    ShowResult(JsonConvert.DeserializeObject<List<Product>>(responseBody));
        //}

    }

    internal class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
    }
}
