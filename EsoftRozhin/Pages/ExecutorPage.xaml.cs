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
    /// Логика взаимодействия для ExecutorPage.xaml
    /// </summary>
    public partial class ExecutorPage : Page
    {
        public ExecutorPage()
        {
            InitializeComponent();
            var allTypes = EsoftBaseRozhinEntities.GetContext().StatusTask.ToList();
            allTypes.Insert(0, new StatusTask
            {
                Status = "Все типы"
            });
            ComboType.ItemsSource = allTypes;

            ComboType.SelectedIndex = 0;

            var currentTask = EsoftBaseRozhinEntities.GetContext().Task.ToList();
            LViewExecutor.ItemsSource = currentTask;

        }

        private void UpdateExecutor()
        {
            var currentExecutor = EsoftBaseRozhinEntities.GetContext().StatusTask.ToList();

            //if (ComboType.SelectedIndex > 0)
            //    currentExecutor = currentExecutor.Where(p => p.Status.Contains(ComboType.SelectedItem as StatusTask)).ToList();

            //currentExecutor = currentExecutor.Where(p => p.Status.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();

            //LViewExecutor.ItemsSource = currentExecutor.OrderBy(p => p.Status).ToList();

        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateExecutor();
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateExecutor();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPageE());
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPageE());
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                EsoftBaseRozhinEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                //Task.ItemsSource = EsoftBaseRozhinEntities.GetContext().Task.ToList();
            }
        }
    }
}
