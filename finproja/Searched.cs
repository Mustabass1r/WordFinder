using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace finproja
{
    public partial class Searched : Form
    {
        private AVLNode node;
        private AutoCompleteStringCollection autoCompleteCollection;
        
        public Searched(AVLNode node)
        {
            InitializeComponent();
            this.node = node;

            autoCompleteCollection = new AutoCompleteStringCollection();
            autoCompleteCollection.AddRange(Dictionary.Instance.autoComplete().ToArray());

            // Assign the AutoCompleteCustomSource to the TextBox
            txtSearch.AutoCompleteCustomSource = autoCompleteCollection;
            txtSearch.AutoCompleteMode = AutoCompleteMode.Append;
            txtSearch.AutoCompleteSource = AutoCompleteSource.CustomSource;

        }





        private void Searched_Load(object sender, EventArgs e)
        {
            UserManager.Instance.currentUser.addRecentSearch(node.word);
            List<string> synonyms = node?.synonyms ?? new List<string>();
            List<string> antonyms = node?.antonyms ?? new List<string>();

            // Update labels with synonyms and antonyms
            label1.Text = node?.word;

            // Simulate line breaks after 20 characters for meanings
            label2.Text = InsertLineBreaks(node?.meanings, 50);

            // Simulate line breaks after 20 characters for synonyms
            label3.Text = "Synonyms: " + InsertLineBreaksForList(synonyms, 40);

            // Simulate line breaks after 20 characters for antonyms
            label4.Text = "Antonyms: " + InsertLineBreaksForList(antonyms, 40);
            if (UserManager.Instance.currentUser.bookmarks.findBookmark(node.word))
            {
                string bookmarkImagePath = @"C:\Users\Ahmad Mustabassir\Desktop\projjaa\bookmark filled.png";

                if (File.Exists(bookmarkImagePath))
                {
                    pbBookmark.Image = Image.FromFile(bookmarkImagePath);
                }
            }
            else
            {
                string bookmarkImagePath = @"C:\Users\Ahmad Mustabassir\Desktop\projjaa\bookmark empty.png";

                if (File.Exists(bookmarkImagePath))
                {
                    pbBookmark.Image = Image.FromFile(bookmarkImagePath);
                }
            }

        }

        private string InsertLineBreaksForList(List<string> wordList, int charactersPerLine)
        {
            StringBuilder brokenText = new StringBuilder();

            int currentLineLength = 0;

            foreach (string word in wordList)
            {
                if (currentLineLength + word.Length <= charactersPerLine)
                {
                    brokenText.Append(word + ", ");
                    currentLineLength += word.Length + 2; // 2 for the word and the comma
                }
                else
                {
                    brokenText.AppendLine();
                    brokenText.Append(word + ", ");
                    currentLineLength = word.Length + 2;
                }
            }

            return brokenText.ToString().TrimEnd(' ', ','); // Trim trailing spaces and commas
        }


        private string InsertLineBreaks(string text, int charactersPerLine)
        {
            StringBuilder brokenText = new StringBuilder();
            string[] words = text.Split(' ');

            int currentLineLength = 0;

            foreach (string word in words)
            {
                if (currentLineLength + word.Length <= charactersPerLine)
                {
                    brokenText.Append(word + " ");
                    currentLineLength += word.Length + 1; // 1 for the space between words
                }
                else
                {
                    brokenText.AppendLine();
                    brokenText.Append(word + " ");
                    currentLineLength = word.Length + 1;
                }
            }

            return brokenText.ToString().TrimEnd();
        }
        public void searchWord() {

            UserManager.Instance.currentUser.addRecentSearch(txtSearch.Text);
            List<string> synonyms = node?.synonyms ?? new List<string>();
            List<string> antonyms = node?.antonyms ?? new List<string>();

            // Update labels with synonyms and antonyms
            label1.Text = node?.word;

            // Simulate line breaks after 20 characters for meanings
            label2.Text = InsertLineBreaks(node?.meanings, 50);

            // Simulate line breaks after 20 characters for synonyms
            label3.Text = "Synonyms: " + InsertLineBreaksForList(synonyms, 30);

            // Simulate line breaks after 20 characters for antonyms
            label4.Text = "Antonyms: " + InsertLineBreaksForList(antonyms, 30);


            

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.node = Dictionary.Instance.searchWord(txtSearch.Text);
            if (node != null)
            {
                searchWord();
                if (UserManager.Instance.currentUser.bookmarks.findBookmark(node.word))
                {
                    string bookmarkImagePath = @"C:\Users\Ahmad Mustabassir\Desktop\projjaa\bookmark filled.png";

                    if (File.Exists(bookmarkImagePath))
                    {
                        pbBookmark.Image = Image.FromFile(bookmarkImagePath);
                    }
                }
                else
                {
                    string bookmarkImagePath = @"C:\Users\Ahmad Mustabassir\Desktop\projjaa\bookmark empty.png";

                    if (File.Exists(bookmarkImagePath))
                    {
                        pbBookmark.Image = Image.FromFile(bookmarkImagePath);
                    }
                }
            }
            else MessageBox.Show("Word not found");

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Search search = new Search(UserManager.Instance.currentUser);
            search.Show();
            this.Close();
        }

        private void pbBookmark_Click(object sender, EventArgs e)
        {
            if (UserManager.Instance.currentUser.bookmarks.findBookmark(node.word))
            {
                UserManager.Instance.currentUser.RemoveBookmark(node.word);
                string bookmarkImagePath = @"C:\Users\Ahmad Mustabassir\Desktop\projjaa\bookmark empty.png";

                if (File.Exists(bookmarkImagePath))
                {
                    pbBookmark.Image = Image.FromFile(bookmarkImagePath);
                }
            }
            else
            {
                UserManager.Instance.currentUser.AddBookmark(node.word);
                string bookmarkImagePath = @"C:\Users\Ahmad Mustabassir\Desktop\projjaa\bookmark filled.png";

                if (File.Exists(bookmarkImagePath))
                {
                    pbBookmark.Image = Image.FromFile(bookmarkImagePath);
                }
            }
        }
    }
}
