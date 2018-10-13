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

        public void CreateBook()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:44869/api/");

                int ValueOfAuthorId = 0;

                var book = new Book()
                {
                    Title = textBox1.Text,
                    Year = int.Parse(textBox2.Text),
                    Price = int.Parse(textBox3.Text),
                    Genre = textBox4.Text,

                    AuthorId = ValueOfAuthorId,
                    Author = new Author()
                    {
                        Id = ValueOfAuthorId,
                        Name = textBox5.Text
                    }
                };

                var postTask = client.PostAsJsonAsync<Book>("Books", book);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Book>();
                    readTask.Wait();

                    var insertBook = readTask.Result;

                    label1.Text = String.Format("Book {0} inserted", book.Title);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CreateBook();
        }

        public void ReadBook()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:44869/api/");

                var responseTask = client.GetAsync("Books");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Book[]>();
                    readTask.Wait();

                    var books = readTask.Result;

                    listBox1.Items.Clear();
                    foreach (var item in books)
                    {
                        listBox1.Items.Add(item.Title);
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ReadBook();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ReadBook();
        }
    }

    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public string Genre { get; set; }

        // Foreign Key
        public int AuthorId { get; set; }
        // Navigation property
        public Author Author { get; set; }
    }

    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

}
