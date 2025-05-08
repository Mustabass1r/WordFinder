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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            SetPlaceholder(txtEmail, "Enter your email");
            SetPlaceholder(txtUsername, "Enter your username");
            SetPlaceholder(txtPassword, "Enter your password");
            this.Select();
            this.ActiveControl = null;
            Color backColour = ColorTranslator.FromHtml("#161616");
            this.BackColor = backColour;
            Color btnbackColour = ColorTranslator.FromHtml("#de0900");
            btnReset.BackColor = btnbackColour;
            btnReset.FlatAppearance.BorderColor = btnbackColour;
            btnReset.FlatAppearance.BorderSize = 1;
            Color lblcolor = ColorTranslator.FromHtml("#434343");
            lblStick.ForeColor = lblcolor;
            lblStick2.ForeColor = lblcolor;
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

        private void label5_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.Show();
            this.Close();
        }
    }
}
