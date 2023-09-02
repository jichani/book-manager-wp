using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HelloCSharp07
{
    public partial class LibraryManager : Form
    {
        public LibraryManager()
        {
            InitializeComponent();
            dataGridView1.DataSource = null;
            if(DataManager.Books.Count>0)
                dataGridView1.DataSource = DataManager.Books;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        // 책 추가 button_add
        private void button1_Click(object sender, EventArgs e)
        {
            bool existBook = false;
            foreach(var item in DataManager.Books)
            {
                // C#은 equals 대신 ==으로 string 비교 가능
                // java는 버전에 따라, 상황에 따라 다르다.
                if(item.isbn == textBox1.Text)
                {
                    existBook = true;
                    break;
                }
            }
            if (existBook)
            {
                MessageBox.Show("이미 있음");
            }
            else
            {
                Book book = new Book();
                book.isbn = textBox1.Text;
                book.name = textBox2.Text;
                book.publisher = textBox3.Text;
                int.TryParse(textBox4.Text, out int page);
                book.page = page;
                if(page <= 0)
                {
                    MessageBox.Show("Page가 이상해요");
                    return;
                }
                DataManager.Books.Add(book);
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = DataManager.Books;
                DataManager.Save();
                //book.page = textBox3.Text
            }
        }

        // 책 수정 button_edit
        private void button2_Click(object sender, EventArgs e)
        {
            Book b = null;
            for (int i = 0; i< DataManager.Books.Count; i++)
            {
                if (DataManager.Books[i].isbn == textBox1.Text)
                {
                    b = DataManager.Books[i];
                    b.name = textBox2.Text;
                    b.publisher = textBox3.Text;
                    int.TryParse(textBox4.Text, out int page);
                    b.page = page;
                    if(page<=0)
                    {
                        MessageBox.Show("페이지 값이 잘못되었습니다.");
                        return;
                    }
                    dataGridView1.DataSource= null;
                    dataGridView1.DataSource = DataManager.Books;
                    DataManager.Save();
                }
            }
            if(b== null)
            {
                MessageBox.Show("없는 책입니다.");
            }
        }

        // 책 삭제 button_delete
        private void button3_Click(object sender, EventArgs e)
        {
            bool existBook = false;
            for(int i = 0; i < DataManager.Books.Count;i++)
            {
                if (DataManager.Books[i].isbn == textBox1.Text)
                {
                    // DataManager.Books.Remove(DataManager.Books[i]);
                    DataManager.Books.RemoveAt(i);
                    existBook = true;
                }
            }
            if(existBook)
            {
                dataGridView1.DataSource = null;
                if(DataManager.Books.Count > 0)
                {
                    dataGridView1.DataSource = DataManager.Books;
                }
                DataManager.Save();
            }
            else
            {
                MessageBox.Show("없는 책 입니다.");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Book b = dataGridView1.CurrentRow.DataBoundItem as Book;
            textBox1.Text = b.isbn;
            textBox2.Text = b.name;
            textBox3.Text = b.publisher;
            textBox4.Text = b.page.ToString();
        }
    }
}
