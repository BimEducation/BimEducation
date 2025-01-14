using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using Path = System.IO.Path;

namespace BimEducation.Login_Page
{
    /// <summary>
    /// Interaction logic for LoginUI.xaml
    /// </summary>
    public partial class LoginUI : Window
    {
        public bool IsAuthorized { get; private set; }
        public LoginUI()
        {
            InitializeComponent();
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text.Trim();
            string password = PasswordBox.Password.Trim();

            if (ValidateCredentials(email, password))
            {
                IsAuthorized = true;
                MessageBox.Show("Login Successful!", "Success");
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid email or password. Please try again.", "Error");
            }
        }
        private bool ValidateCredentials(string email, string password)
        {
            string xmlPath = Path.Combine(Directory.GetCurrentDirectory(), "UserCredentials.xml");
            if (!File.Exists(xmlPath))
                return false;

            XDocument credentialsXml = XDocument.Load(xmlPath);

            return credentialsXml.Root.Elements("User").Any(user =>
                user.Element("Email")?.Value == email &&
                user.Element("Password")?.Value == password);
        }
    }
}
