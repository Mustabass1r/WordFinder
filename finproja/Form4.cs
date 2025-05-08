using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace finproja
{
     partial class Form4 : Form
    {
       

        public Form4(User currentUser)
        {
            InitializeComponent();
          

        }

        private void Form4_Load(object sender, EventArgs e)
        {
            Color backColour = ColorTranslator.FromHtml("#161616");
            this.BackColor = backColour;
            pnlBookmark.BackColor = backColour;
            ShowBookmarks();
        }
        private void ShowBookmarks()
        {
            pnlBookmark.Controls.Clear();
            var bookmarks = UserManager.Instance.currentUser.GetBookmarks();
            foreach (var bookmark in bookmarks)
            {
                Label lblBookmark = new Label();
                lblBookmark.Text = bookmark;
                lblBookmark.Font = new Font("Arial", 13); 
                lblBookmark.ForeColor = Color.White;
                if (pnlBookmark.Controls.Count > 0)
                {
                    pnlBookmark.Controls.Add(new Label { Text = Environment.NewLine });
                }
                pnlBookmark.Controls.Add(lblBookmark);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            home home = new home(UserManager.Instance.currentUser);
            home.Show();
            this.Hide();
        }
    }
}

