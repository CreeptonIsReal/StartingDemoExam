using Azure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Validation;
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
        public static bool _guest;
        public static Base.Product Product = null;
        private int DlgMode = 0;
        public Base.Product SelectedItem;
        public ObservableCollection<Base.Product> Products;
        private int CountRecord;
        private string[] manufacturers = { "Все производители", "Davinci", "Attribute", "Doria", "Alaska", "Apollo", "Smart Home", "Mayer & Boch" };
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            UpdateGrid(null);
            DlgLoad(false, "");
            ProductListBox.ItemsSource = SourceCore.MyBase.Product.ToList();
            CountRecord = ProductListBox.Items.Count;

            FillData();

        }

        private void GenerateCaptcha()
        {
            string allowchar = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            allowchar += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,y,z";
            allowchar += "1,2,3,4,5,6,7,8,9,0";
            //разделитель
            char[] a = { ',' };
            //расщепление массива по разделителю
            String[] ar = allowchar.Split(a);
            String pwd = "";
            Random r = new Random();
            for (int i = 0; i < 6; i++)
            {
                string temp = ar[(r.Next(0, ar.Length))];
                pwd += temp;
            }
            RecordTextArticle.Text = pwd.ToUpper();
        }

        private void SelectAuthWindow()
        {
            Autorization window = new Autorization();
            window.Show();
            Close();
        }

        private void FillData()
        {
            if (_guest)
            {
                AdminDP.Visibility = Visibility.Hidden;
            }
            else 
            {
                string fio_user = "Вы вошли как гость";
                fio_user = User.UserName + " " + User.UserSurname + " " + User.UserPatronymic + " ";
                NameLabel.Content = "Добро пожаловать, " + fio_user;
                if (User.UserRole == 1)
                {
                    AdminDP.Visibility = Visibility.Visible;
                }
                else
                {
                    AdminDP.Visibility = Visibility.Hidden;
                }
            }


            TypeComboBox.Items.Insert(0, "Вилки");
            TypeComboBox.Items.Insert(1, "Ложки");
            TypeComboBox.Items.Insert(2, "Наборы");
            TypeComboBox.Items.Insert(3, "Ножи");

            ManufacturerComboBox.SelectedItem = manufacturers[0];
            ManufacturerComboBox.ItemsSource = manufacturers;
        }

        public void UpdateGrid(Base.Product Product)
        {
            if ((Product == null) && (ProductListBox.ItemsSource != null))
            {
                Product = (Base.Product)ProductListBox.SelectedItem;
            }
            Products = new ObservableCollection<Base.Product>(SourceCore.MyBase.Product);
            ProductListBox.ItemsSource = Products;
            ProductListBox.SelectedItem = Product;
            CountUpdate();
        }

        public void DlgLoad(bool b, string DlgModeContent)
        {
            if (b == true)
            {
                ColumnChange.Width = new GridLength(200);
                ProductListBox.IsHitTestVisible = false;
                RecordLabel.Content = DlgModeContent + " запись";
                AddCommit.Content = DlgModeContent;
                RecordAdd.IsEnabled = false;
                RecordEdit.IsEnabled = false;
                RecordDellete.IsEnabled = false;
            }
            else
            {
                ColumnChange.Width = new GridLength(0);
                ProductListBox.IsHitTestVisible = true;
                RecordAdd.IsEnabled = true;
                RecordEdit.IsEnabled = true;
                RecordDellete.IsEnabled = true;
                DlgMode = -1;
            }
        }

        private void BackAuthButton_Click(object sender, RoutedEventArgs e)
        {
            CountUpdate();
            SelectAuthWindow();
        }


        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CountUpdate();
            FilterProducts();
        }

        private void RecordAdd_Click(object sender, RoutedEventArgs e)
        {
            GenerateCaptcha();
            DlgLoad(true, "Добавить");
            DataContext = null;
            DlgMode = 0;
            CountRecord = ProductListBox.Items.Count;
            CountUpdate();
        }
        private void CountUpdate()
        {
            CountLabel.Content = ProductListBox.Items.Count + "/" + CountRecord;
        }

        private void AddCommit_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrEmpty(RecordTextArticle.Text))
                errors.AppendLine("Укажите артикль");

            if (string.IsNullOrEmpty(RecordTextName.Text))
                errors.AppendLine("Укажите наименование");

            if (string.IsNullOrEmpty(TypeComboBox.Text))
                errors.AppendLine("Укажите тип");

            if (string.IsNullOrEmpty(RecordTextQuantity.Text))
                errors.AppendLine("Укажите количество");

            if (string.IsNullOrEmpty(RecordTextManufacturer.Text))
                errors.AppendLine("Укажите поставщика");

            if (string.IsNullOrEmpty(RecordTextPrice.Text))
                errors.AppendLine("Укажите цену за 1 ед.");

            if (string.IsNullOrEmpty(RecordTextDescription.Text))
                errors.AppendLine("Укажите описание");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (DlgMode == 0)
            {
                //(int)Convert.ToDecimal
                var NewBase = new Base.Product();
                NewBase.ProductArticleNumber = RecordTextArticle.Text;
                NewBase.ProductName = RecordTextName.Text;
                NewBase.ProductCategory = TypeComboBox.SelectedItem.ToString();
                NewBase.ProductQuantityInStock = int.Parse(RecordTextQuantity.Text);
                NewBase.ProductManufacturer = RecordTextManufacturer.Text;
                NewBase.ProductCost = Convert.ToDecimal(RecordTextPrice.Text);
                NewBase.ProductDescription = RecordTextDescription.Text;
                SourceCore.MyBase.Product.Add(NewBase);
                SelectedItem = NewBase;
                CountUpdate();
            }
            else
            {
                var EditBase = new Base.Product();
                EditBase = SourceCore.MyBase.Product.First(p => p.ProductArticleNumber == SelectedItem.ProductArticleNumber);
                EditBase.ProductArticleNumber = RecordTextArticle.Text;
                EditBase.ProductName = RecordTextName.Text;
                EditBase.ProductCategory = TypeComboBox.SelectedItem.ToString();
                EditBase.ProductQuantityInStock = int.Parse(RecordTextQuantity.Text);
                EditBase.ProductManufacturer = RecordTextManufacturer.Text;
                EditBase.ProductCost = Convert.ToDecimal(RecordTextPrice.Text);
                EditBase.ProductDescription = RecordTextDescription.Text;
                CountUpdate();
            }

            try
            {
                CountUpdate();
                SourceCore.MyBase.SaveChanges();
                UpdateGrid(SelectedItem);
                DlgLoad(false, "");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void AddRollback_Click(object sender, RoutedEventArgs e)
        {
            UpdateGrid(SelectedItem);
            DlgLoad(false, "");
            CountUpdate();
            CountRecord = ProductListBox.Items.Count;
        }

        private void RecordEdit_Click(object sender, RoutedEventArgs e)
        {
            if (ProductListBox.SelectedItem != null)
            {
                DlgLoad(true, "Изменить");
                SelectedItem = (Base.Product)ProductListBox.SelectedItem;
                RecordTextArticle.Text = SelectedItem.ProductArticleNumber;
                RecordTextName.Text = SelectedItem.ProductName;
                RecordTextQuantity.Text = SelectedItem.ProductQuantityInStock.ToString();
                RecordTextManufacturer.Text = SelectedItem.ProductManufacturer;
                RecordTextPrice.Text = SelectedItem.ProductCost.ToString();
                RecordTextDescription.Text = SelectedItem.ProductDescription;
                CountUpdate();
            }
            else
            {
                MessageBox.Show("Не выбрано ни одной строки!", "Сообщение", MessageBoxButton.OK);
            }
        }

        private void RecordDellete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить запись?", "Внимание", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
            {

                try
                {
                    // Ссылка на удаляемую книгу
                    Base.Product DeletingAccessory = (Base.Product)ProductListBox.SelectedItem;
                    // Определение ссылки, на которую должен перейти указатель после удаления
                    if (ProductListBox.SelectedIndex < ProductListBox.Items.Count - 1)
                    {
                        ProductListBox.SelectedIndex++;
                    }
                    else
                    {
                        if (ProductListBox.SelectedIndex > 0)
                        {
                            ProductListBox.SelectedIndex--;
                        }
                    }
                    Base.Product SelectingAccessory = (Base.Product)ProductListBox.SelectedItem;
                    SourceCore.MyBase.Product.Remove(DeletingAccessory);
                    SourceCore.MyBase.SaveChanges();
                    UpdateGrid(SelectingAccessory);
                }
                catch
                {

                    MessageBox.Show("Невозможно удалить запись, так как она используется в других справочниках базы данных.",
                    "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None);
                }
            }
        }

        private void FilterProducts()
        {
            if (ManufacturerComboBox.SelectedItem.ToString() == manufacturers[0])
            {
                ProductListBox.ItemsSource = Products.Where(p => p.ProductName.Contains(SearchTextBox.Text) ||
                p.ProductName.Contains(SearchTextBox.Text) ||
                p.ProductCost.ToString().Contains(SearchTextBox.Text) ||
                p.ProductManufacturer.Contains(SearchTextBox.Text) ||
                p.ProductQuantityInStock.ToString().Contains(SearchTextBox.Text));
                CountUpdate();
            }
            else
            {
                ProductListBox.ItemsSource = Products.Where(p => (p.ProductName.Contains(SearchTextBox.Text) ||
                p.ProductName.Contains(SearchTextBox.Text) ||
                p.ProductCost.ToString().Contains(SearchTextBox.Text) ||
                p.ProductQuantityInStock.ToString().Contains(SearchTextBox.Text)) &&
                p.ProductManufacturer.Contains(ManufacturerComboBox.SelectedItem.ToString()));
                CountUpdate();
            }

        }

        private void ManufacturerComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterProducts();
        }
    }
}
