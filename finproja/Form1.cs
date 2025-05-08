using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace finproja
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();

           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Color backColour = ColorTranslator.FromHtml("#161616");
            this.BackColor = backColour;
            Color btnbackColour = ColorTranslator.FromHtml("#de0900");
            btnLogin.BackColor = btnbackColour;
            Color lblcolor = ColorTranslator.FromHtml("#434343");
            label3.ForeColor = lblcolor;
            label4.ForeColor = lblcolor;
            btnLogin.FlatAppearance.BorderColor = btnbackColour;
            btnLogin.FlatAppearance.BorderSize = 1;
            SetPlaceholder(txtUsername, "Email");
            SetPlaceholder(txtPassword, "Password");
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

        private void label5_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.Show();
            this.Hide();
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

        private void lblResetPass_MouseHover(object sender, EventArgs e)
        {
            Color colour = ColorTranslator.FromHtml("#de0900");
            lblResetPass.ForeColor = colour;
        }
  

        private void lblResetPass_MouseLeave_1(object sender, EventArgs e)
        {
            lblResetPass.ForeColor = Color.White;
        }

        private void lblResetPass_Click(object sender, EventArgs e)
        {
            Form3 form = new Form3();
            form.Show();
            this.Hide();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            User user = UserManager.Instance.Login(txtUsername.Text, txtPassword.Text);
            if (user == null)
            {
                MessageBox.Show("Wrong credentials");
            }
            else
            {
                home home1 = new home(user);
                Dictionary.Instance.Insert600();
                
                home1.Show();
                this.Hide();
                
            }
        }
    }
}
