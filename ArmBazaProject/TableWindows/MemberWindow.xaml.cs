using ArmBazaProject.Models;
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

namespace ArmBazaProject
{
    /// <summary>
    /// Логика взаимодействия для MemberWindow.xaml
    /// </summary>
    public partial class MemberWindow : Window
    {
        public List<string> genders = new List<string>() { "м", "ж" };
        public Member changedMember;
        DataBaseModel dataBaseModel;

        public MemberWindow(Member member)
        {
            InitializeComponent();
            changedMember = member;
            this.DataContext = changedMember;
            dataBaseModel = new DataBaseModel();
            teamCB.ItemsSource = dataBaseModel.GetAllObjectTeams();
            QualificationCB.ItemsSource = dataBaseModel.GetAllObjectQualifications();
            genderCB.ItemsSource = genders;

        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            changedMember.Gender = genderCB.Text;
            this.DialogResult = true;
        }

    }
}
