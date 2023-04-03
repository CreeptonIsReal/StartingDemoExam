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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KitchenWPF_Project
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Base.User User = null;
        public MainWindow()
        {
            InitializeComponent();
            FillData();
        }

        private void SelectAuthWindow()
        {
            Autorization window = new Autorization();
            window.Show();
            Close();
        }

        private void FillData()
        {
            string fio_user = "Вы вошли как гость";
            fio_user = User.UserName + " " + User.UserSurname + " " + User.UserPatronymic + " ";
            NameLabel.Content = "Добро пожаловать, " + fio_user;
        }

        private void BackAuthButton_Click(object sender, RoutedEventArgs e)
        {
            SelectAuthWindow();
        }
    }
}
