using SalesOrder.Client.Services;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace SalesOrder.Client
{
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private async void btnContinue_Click(object sender, RoutedEventArgs e)
        {
            if (txtPassword.Text.Trim() != txtConfirm.Text.Trim())
            {
                MessageBox.Show("Password wrong");
            }
            else
            {
                bool response = await ApiService.RegisterUser(txtName.Text.Trim(), txtEmail.Text.Trim(), txtPassword.Text);
                if (response)
                {
                    MessageBox.Show("Hi, Your account has been created");
                }
                else
                {
                    MessageBox.Show("Ooops, there are something went wrong.");
                }
            }
        }
    }
}
