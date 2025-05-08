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
     partial class home : Form
    {
        private User user;
        public home(User user)
        {
            InitializeComponent();
            this.user = user;
        }

        private void home_Load_1(object sender, EventArgs e)
        {
            Color backColour = ColorTranslator.FromHtml("#161616");
            this.BackColor = backColour;
            Color btnbackColour = ColorTranslator.FromHtml("#de0900");
            Color panelColour = ColorTranslator.FromHtml("#313131");
            panel1.BackColor = panelColour;
            

            CustomPanelButton feelingLucky = CreateCustomButton(@"C:\Users\Ahmad Mustabassir\Desktop\projjaa\surprise-box.png", "FeelingLucky", new Point(225, 255));
            feelingLucky.TargetForm = new Searched(Dictionary.Instance.feelingLucky());  // Specify the form to open for FeelingLucky

            CustomPanelButton bookmark = CreateCustomButton(@"C:\Users\Ahmad Mustabassir\Desktop\projjaa\bookmark (2).png", "Bookmark", new Point(225, 123));
            bookmark.TargetForm = new Form4(user);  // Specify the form to open for Bookmark

            CustomPanelButton search = CreateCustomButton(@"C:\Users\Ahmad Mustabassir\Desktop\projjaa\seatch.png", "Search", new Point(77, 123));
           search.TargetForm = new Search(user);  // Specify the form to open for Search

            CustomPanelButton anagram = CreateCustomButton(@"C:\Users\Ahmad Mustabassir\Desktop\projjaa\anagram.png", "Anagram", new Point(77, 255));
           // anagram.TargetForm = new AnagramForm();  // Specify the form to open for Anagram

            this.Controls.AddRange(new Control[] { feelingLucky, bookmark, search, anagram });



            this.Select();
            this.ActiveControl = null;
        }

        private void splitter1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private CustomPanelButton CreateCustomButton(string imagePath, string labelText, Point location)
        {
            CustomPanelButton button = new CustomPanelButton(imagePath, labelText);
            button.Size = new Size(100, 100);
            button.Location = location;
            return button;
        }
    }
}
