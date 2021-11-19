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

namespace ArmBazaProject.windows
{
    /// <summary>
    /// Логика взаимодействия для TeamWindow.xaml
    /// </summary>
    public partial class TeamWindow : Window
    {
        public Team changedTeam;
        DataBaseModel model;
        public TeamWindow(Team team)
        {
            InitializeComponent();
            model = new DataBaseModel();
            changedTeam = team;
            this.DataContext = changedTeam;
           // regionCB.ItemsSource = model.GetAllRegions();
        }

        private void Accept_ClickTeam(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
