using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
//web api url>>http://localhost:51416
namespace ConsoleApp4CallSimpleWebApi
{
    class Program
    {
        static void Main(string[] args)
        {
            Gettest();
            Console.ReadLine();
            Console.WriteLine("Please Input A Name...");
            string NewName = Console.ReadLine();
            Posttest(NewName);
            Console.ReadLine();
            Gettest();
            Console.ReadLine();
            Console.WriteLine("Please Input A Index Number...");
            string UpdateIndexNumber = Console.ReadLine();
            Console.WriteLine("Please Input A Name...");
            string UpdateName = Console.ReadLine();
            Puttest(UpdateIndexNumber, UpdateName);
            Console.ReadLine();
            Gettest();
            Console.ReadLine();
            Console.WriteLine("Please Input A Index Number...");
            string DeleteIndexNumber = Console.ReadLine();
            Deletetest(DeleteIndexNumber);
            Console.ReadLine();
            Gettest();
            Console.ReadLine();
        }

        static string WebApiSourceURL = @"http://localhost:51416/";

        static void Gettest()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(WebApiSourceURL);
            HttpResponseMessage resp = client.GetAsync("api/employees").Result;
            IEnumerable<string> data = null;
            if (resp.IsSuccessStatusCode)
            {
                data = resp.Content.ReadAsAsync<IEnumerable<string>>().Result;
                foreach (var item in data)
                {
                    Console.WriteLine(item);
                }
            }
        }

        static async void Posttest(string newName)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(WebApiSourceURL);
            var response = await client.PostAsJsonAsync("api/employees", newName);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Inserted");
            }
        }

        static async void Puttest(string updateId, string updateName)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(WebApiSourceURL);
            var response = await client.PutAsJsonAsync("api/employees/" + updateId, updateName);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("updated");
            }
        }

        static async void Deletetest(string deleteId)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(WebApiSourceURL);
            var response = await client.DeleteAsync("api/employees/" + deleteId);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("deleted");
            }
        }
    }
}
