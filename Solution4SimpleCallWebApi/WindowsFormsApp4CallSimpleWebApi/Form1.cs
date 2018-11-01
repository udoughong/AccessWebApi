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

namespace WindowsFormsApp4CallSimpleWebApi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnGetAll_Click(object sender, EventArgs e) => GetAllData();

        string WebApiSourceURL = @"http://localhost:51416/";

        private void GetAllData()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(WebApiSourceURL);
            HttpResponseMessage resp = client.GetAsync("api/employees").Result;
            IEnumerable<string> data = null;

            ListResult.Items.Clear();

            if (resp.IsSuccessStatusCode)
            {
                data = resp.Content.ReadAsAsync<IEnumerable<string>>().Result;
                foreach (var item in data)
                {
                    ListResult.Items.Add(item);
                }
            }
        }

        private void BtnCreate_Click(object sender, EventArgs e)
        {
            CreateDataAsync(TxtCreateName.Text);
        }

        private async void CreateDataAsync(string newName)
        {
            if (newName != "")
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(WebApiSourceURL);
                var response = await client.PostAsJsonAsync("api/employees", newName);
                if (response.IsSuccessStatusCode)
                {
                    GetAllData();
                }
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            UpdateDataAsync(TxtUpdateId.Text,TxtUpdateName.Text);
        }

        private async void UpdateDataAsync(string updateId, string updateName)
        {
            if ((updateId != "") && (updateName != ""))
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(WebApiSourceURL);
                HttpResponseMessage response = await client.PutAsJsonAsync("api/employees/" + updateId, updateName);
                if (response.IsSuccessStatusCode)
                {
                    GetAllData();
                }
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            DeleteData(TxtDeleteId.Text);
        }

        private async void DeleteData(string deleteId)
        {
            if (deleteId != "")
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(WebApiSourceURL);
                var response = await client.DeleteAsync("api/employees/" + deleteId);
                if (response.IsSuccessStatusCode)
                {
                    GetAllData();
                }
            }
        }
    }
}
