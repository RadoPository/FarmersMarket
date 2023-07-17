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

namespace FarmersMarket
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ProductRepository productRepo;
        public MainWindow()
        {
            InitializeComponent();

            // Initialize the ProductRepository
            productRepo = new ProductRepository("Data Source=database.db");

            // Load data into the DataGrid
            LoadData();
        }

        private void LoadData()
        {
            productDataGrid.ItemsSource = productRepo.GetAllProducts();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // Open a dialog to add a new product
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            // Open a dialog to update the selected product
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Delete the selected product
            if (productDataGrid.SelectedItem is Product selectedProduct)
            {
                productRepo.DeleteProduct(selectedProduct.ProductId);
                LoadData();
            }
        }
    }
}
