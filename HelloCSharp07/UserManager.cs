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
    public partial class UserManager : Form
    {
        public UserManager()
        {
            InitializeComponent();
            dataGridView1.DataSource = null;
            if (DataManager.Users.Count > 0)
                dataGridView1.DataSource = DataManager.Users;

            dataGridView1.CellClick += DataGridViewCellClick;

            // 람다 방식. 무명 델리게이트를 줄인 방식
            button1.Click += (s, e) =>
            {
                // 반환은 true/false 매개변수는 해당 List의 구성요소
                // 여기선 User를 모두 검색해서 같은 값이 있는지 찾아 반환한다.
                if (DataManager.Users.Exists(x=>x.id==textBox1.Text))
                {
                    MessageBox.Show("해당 ID 이미 있음");
                }
                else
                {
                    User u = new User() { id = textBox1.Text, 이름 = textBox2.Text };
                    DataManager.Users.Add(u);
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = DataManager.Users;
                    DataManager.Save();
                }
            };

            // 무명 델리게이트 방식. 메소드 이름 짓는 것도 귀찮으면 사용하면 됌.
            button2.Click += delegate (object s, EventArgs e)
            {
                try
                {
                    User u = DataManager.Users.Single(x => x.id == textBox1.Text);
                    u.이름 = textBox2.Text;
                    try
                    {
                        // Single에 해당하는 게 없으면 바로 catch로 빠지는 특징이 있다.
                        Book b = DataManager.Books.Single(x => x.userld == textBox1.Text);
                        b.userName = textBox2.Text;
                    }
                    catch (Exception)
                    {
                        
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("없는 ID 입니다.");
                }
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = DataManager.Users;
                DataManager.Save();
            };

            // EventHandler deleteBtn = delegate(object s, EventArgs e) {}
            EventHandler deleteBtn = (s, e) =>
            {
                try
                {
                    User u = DataManager.Users.Single(x=>x.id==textBox1.Text);
                    DataManager.Users.Remove(u);

                    dataGridView1.DataSource = null;
                    if (DataManager.Users.Count>0)
                        dataGridView1.DataSource = DataManager.Users;
                    DataManager.Save();
                }
                catch (Exception)
                {

                }
            };
            button3.Click += deleteBtn;
        }

        private void DataGridViewCellClick(object sender, DataGridViewCellEventArgs e)
        {
            User u = (sender as DataGridView).CurrentRow.DataBoundItem as User;
            textBox1.Text = u.id;
            textBox2.Text = u.이름;
        }
    }
}
