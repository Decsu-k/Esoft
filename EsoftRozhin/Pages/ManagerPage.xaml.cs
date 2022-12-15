using EsoftRozhin.AppDataFile;
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

namespace EsoftRozhin.Pages
{
    /// <summary>
    /// Логика взаимодействия для ManagerPage.xaml
    /// </summary>
    public partial class ManagerPage : Page
    {
        public ManagerPage()
        {
            InitializeComponent();
            var allTypes = EsoftBaseRozhinEntities.GetContext().StatusTask.ToList();

            var currentTask = EsoftBaseRozhinEntities.GetContext().Task.ToList();
            LViewExecutor.ItemsSource = currentTask;
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
