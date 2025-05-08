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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            SetPlaceholder(txtEmail, "Enter your email");
            SetPlaceholder(txtUsername, "Enter your username");
            SetPlaceholder(txtPassword, "Enter your password");
            this.Select();
            this.ActiveControl = null;
            Color backColour = ColorTranslator.FromHtml("#161616");
            this.BackColor = backColour;
            Color btnbackColour = ColorTranslator.FromHtml("#de0900");
            btnSignup.BackColor = btnbackColour;
            btnSignup.FlatAppearance.BorderColor = btnbackColour;
            btnSignup.FlatAppearance.BorderSize = 1;
            Color lblcolor = ColorTranslator.FromHtml("#434343");
            lblStick2.ForeColor = lblcolor;
            lblStick.ForeColor = lblcolor;
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
            Form1 form = new Form1();
            form.Show();
            this.Close();
        }

        private void label5_MouseHover(object sender, EventArgs e)
        {
            Color colour = ColorTranslator.FromHtml("#de0900");
            label5.ForeColor = colour;
        }

        private void label5_MouseLeave(object sender, EventArgs e)
        {
            label5.ForeColor = Color.White;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void label_Click(object sender, EventArgs e)
        {

        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSignup_Click(object sender, EventArgs e)
        {
            UserManager.Instance.RegisterUser(txtUsername.Text , txtEmail.Text , txtPassword.Text);
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        
    }
}
