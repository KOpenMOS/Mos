using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using AasxPackageExplorer.Models;

namespace AasxPackageExplorer
{
    /// <summary>
    /// Interaction logic for AasListView.xaml
    /// </summary>
    public partial class AasListView : Window
    {
        public string CurrentIdShort { get; set; }
        public ObservableCollection<AasView> AasItems { get; set; }
        public AasListView()
        {
            InitializeComponent();
        }

        public void SetItems(IEnumerable<AasView> aasViews)
        {
            AasItems = new ObservableCollection<AasView>();
            foreach (var item in aasViews)
            {
                AasItems.Add(item);
            }

            aasList.ItemsSource = AasItems;
        }

        private void LoadAas(object sender, RoutedEventArgs e)
        {
            CurrentIdShort = ((Button)sender).Tag.ToString();
            this.DialogResult = true;
            this.Close();
        }

    }

}
