using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace finproja
{
     partial class Search : Form
    {
        
        private AutoCompleteStringCollection autoCompleteCollection;
        public Search(User user)
        {
            InitializeComponent();

            autoCompleteCollection = new AutoCompleteStringCollection();
            autoCompleteCollection.AddRange(Dictionary.Instance.autoComplete().ToArray());

            // Assign the AutoCompleteCustomSource to the TextBox
            txtSearch.AutoCompleteCustomSource = autoCompleteCollection;
            txtSearch.AutoCompleteMode = AutoCompleteMode.Append;  // Set to Append to enable autocomplete without showing suggestions
            txtSearch.AutoCompleteSource = AutoCompleteSource.CustomSource;

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            
        }


        public void recent() {
            IEnumerable<string> recentSearches = UserManager.Instance.currentUser.recentSearchManager.GetRecentSearches();
            lblRecent1.Text = recentSearches.FirstOrDefault() ?? "No recent searches";
            lblRecent2.Text = recentSearches.Skip(1).FirstOrDefault() ?? "";
            lblRecent3.Text = recentSearches.Skip(2).FirstOrDefault() ?? "";
            lblRecent4.Text = recentSearches.Skip(3).FirstOrDefault() ?? "";
            lblRecent5.Text = recentSearches.Skip(4).FirstOrDefault() ?? "";
        }

        private void Search_Load(object sender, EventArgs e)
        {
            recent();
            Color backColour = ColorTranslator.FromHtml("#161616");
            this.BackColor = backColour;
            SetPlaceholder(txtSearch, "Search Here");
            this.Select();
            this.ActiveControl = null;
        }
        private void SetPlaceholder(TextBox textBox, string placeholder)
        {
            textBox.Text = placeholder;
            textBox.ForeColor = Color.Gray;
            textBox.GotFocus += (s, e) => { OnTextBoxGotFocus(textBox, placeholder); };
            textBox.LostFocus += (s, e) => { OnTextBoxLostFocus(textBox, placeholder); };
        }
        private void OnTextBoxGotFocus(TextBox textBox, string placeholder)
        {
            if (textBox.Text == placeholder)
            {
                textBox.Text = "";
                textBox.ForeColor = SystemColors.WindowText; // Set the color to the default text color
            }
        }

        private void OnTextBoxLostFocus(TextBox textBox, string placeholder)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = placeholder;
                textBox.ForeColor = Color.Gray;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            AVLNode node = Dictionary.Instance.searchWord(txtSearch.Text);

            if (node != null)
            {
                Searched earch = new Searched(node);
            
                this.Close();
                earch.Show();
            }
            else MessageBox.Show("Word not found");
           
        }

        

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            home home = new home(UserManager.Instance.currentUser);
            home.Show();
            this.Hide();
        }
    }
}
