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

namespace KitchenWPF_Project
{
    /// <summary>
    /// Логика взаимодействия для ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        private string[] manufacturers = { "Все производители", "Davinci", "Attribute", "Doria", "Alaska", "Apollo", "Smart Home", "Mayer & Boch" };
        public ProductWindow()
        {
            InitializeComponent();
            ProductListBox.ItemsSource = SourceCore.MyBase.Product.ToList();
        }

        private void SelectAuthWindow()
        {
            Autorization window = new Autorization();
            window.Show();
            Close();
        }

        private void BackAuthButton_Click(object sender, RoutedEventArgs e)
        {
            SelectAuthWindow();
        }

        /*private void FilterProducts()
        {
            if (ManufacturerComboBox.SelectedItem.ToString() == manufacturers[0])
            {
                ProductListBox.ItemsSource = Products.Where(p => p.ProductName.Contains(SearchTextBox.Text) ||
                p.ProductName.Contains(SearchTextBox.Text) ||
                p.ProductCost.ToString().Contains(SearchTextBox.Text) ||
                p.ProductManufacturer.Contains(SearchTextBox.Text) ||
                p.ProductQuantityInStock.ToString().Contains(SearchTextBox.Text));
            }
            else
            {
                ProductListBox.ItemsSource = Products.Where(p => (p.ProductName.Contains(SearchTextBox.Text) ||
                p.ProductName.Contains(SearchTextBox.Text) ||
                p.ProductCost.ToString().Contains(SearchTextBox.Text) ||
                p.ProductQuantityInStock.ToString().Contains(SearchTextBox.Text)) &&
                p.ProductManufacturer.Contains(ManufacturerComboBox.SelectedItem.ToString()));
            }

        }*/

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textbox = sender as TextBox;
            ProductListBox.ItemsSource = SourceCore.MyBase.Product.Where(
                q => q.ProductName.ToString().Contains(textbox.Text) ||
                q.ProductDescription.ToString().Contains(textbox.Text) ||
                q.ProductManufacturer.ToString().Contains(textbox.Text) ||
                q.ProductCost.ToString().Contains(textbox.Text) ||
                q.ProductQuantityInStock.ToString().Contains(textbox.Text)
                ).ToList();
        }
    }
}
