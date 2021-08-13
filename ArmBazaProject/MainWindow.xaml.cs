using System.Windows;
using System.Data.Entity;
using System.Windows.Controls;
using ArmBazaProject.windows;
using System.Windows.Input;
using ArmBazaProject.ViewModels;
using System.Collections.ObjectModel;
using ArmBazaProject.Models;

namespace ArmBazaProject
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ApplicationContext dataBase;
        DataBaseModel dataBaseModel;
        CompetitionViewModel competitionVM;
        

        public MainWindow()
        {
            InitializeComponent();

            #region БД
            dataBase = new ApplicationContext();
            dataBaseModel = new DataBaseModel();

            dataBase.Members.Load();
            dataBase.Teams.Load();
            #endregion

            #region прикрепление данных к листам из бд
            

            teamsList.ItemsSource = dataBase.Teams.Local.ToBindingList();
            competitorList.ItemsSource = dataBase.Members.Local.ToBindingList();
            #endregion

            ageCategoryCB.ItemsSource = competitionVM.categories;
            categoriesGrid.ItemsSource = dataBaseModel.GetAllCategories();
            pointsCB.ItemsSource = competitionVM.points;


        }

        #region Members Edit

        private void AddMember_Click(object sender, RoutedEventArgs e)
        {
            MemberWindow memberWindow = new MemberWindow(new Member());
            if (memberWindow.ShowDialog() == true)
            {
                Member member = memberWindow.changedMember;
                dataBase.Members.Add(member);
                dataBase.SaveChanges();
            }
        }

        private void EditMember_Click(object sender, RoutedEventArgs e)
        {

            //  если ни одного объекта не выделено, выходим
            if (competitorList.SelectedItem == null) return;
            // получаем выделенный объект
            Member member = competitorList.SelectedItem as Member;

            MemberWindow windowOfChange = new MemberWindow(new Member
            {
                Id = member.Id,
                FullName = member.FullName,
                QualificationId = member.QualificationId,
                TeamId = member.TeamId,
                DateOfBirth = member.DateOfBirth,
                Weight = member.Weight,
                Gender = member.Gender

            }) ;

            windowOfChange.genderCB.SelectedItem = member.Gender;



            if (windowOfChange.ShowDialog() == true)
            {
                member = dataBase.Members.Find(windowOfChange.changedMember.Id);
                if (member != null)
                {
                    member.FullName = windowOfChange.changedMember.FullName;
                    member.QualificationId = windowOfChange.changedMember.QualificationId;
                    member.DateOfBirth = windowOfChange.changedMember.DateOfBirth;
                    member.TeamId = windowOfChange.changedMember.TeamId;
                    member.Weight = windowOfChange.changedMember.Weight;
                    member.Gender = windowOfChange.changedMember.Gender;
                    dataBase.Entry(member).State = EntityState.Modified;
                    dataBase.SaveChanges();
                }
            }

        }

        private void DeleteMember_Click(object sender, RoutedEventArgs e)
        {
            // если ни одного объекта не выделено, выходим
            if (competitorList.SelectedItem == null) return;
            // получаем выделенный объект
            Member member = competitorList.SelectedItem as Member;
            dataBase.Members.Remove(member);
            dataBase.SaveChanges();
        }

        #endregion

        #region Team Edit

        private void AddTeam_Click(object sender, RoutedEventArgs e)
        {
            TeamWindow teamWindow = new TeamWindow(new Team());
            if (teamWindow.ShowDialog() == true)
            {
                Team team = teamWindow.changedTeam;
                dataBase.Teams.Add(team);
                dataBase.SaveChanges();
            }
        }

        private void EditTeam_Click(object sender, RoutedEventArgs e)
        {
            //  если ни одного объекта не выделено, выходим
            if (teamsList.SelectedItem == null) return;
            // получаем выделенный объект
            Team team = teamsList.SelectedItem as Team;

            TeamWindow teamWindow = new TeamWindow(new Team
            {
                TrainerName = team.TrainerName,
                RegionId = team.RegionId,
                Name = team.Name,
                Id = team.Id
            }) ;



            if (teamWindow.ShowDialog() == true)
            {
                team = dataBase.Teams.Find(teamWindow.changedTeam.Id);
                if (team != null)
                {
                    team.Name = teamWindow.changedTeam.Name;
                    team.TrainerName = teamWindow.changedTeam.TrainerName;
                    team.RegionId = teamWindow.changedTeam.RegionId;
                    dataBase.Entry(team).State = EntityState.Modified;
                    dataBase.SaveChanges();
                }
            }
        }

        private void DeleteTeam_Click(object sender, RoutedEventArgs e)
        {
            // если ни одного объекта не выделено, выходим
            if (teamsList.SelectedItem == null) return;
            // получаем выделенный объект
            Team team = teamsList.SelectedItem as Team;
            dataBase.Teams.Remove(team);
            dataBase.SaveChanges();
        }

        #endregion

        #region Trainer Edit

        //private void AddTrainer_Click(object sender, RoutedEventArgs e)
        //{
        //    TrainerWindow trainerWindow = new TrainerWindow(new Trainer());
        //    if (trainerWindow.ShowDialog() == true)
        //    {
        //        Trainer trainer = trainerWindow.changedTrainer;
        //        dataBase.Trainers.Add(trainer);
        //        dataBase.SaveChanges();
        //    }
        //}

        //private void EditTrainer_Click(object sender, RoutedEventArgs e)
        //{
        //    //  если ни одного объекта не выделено, выходим
        //    if (trainersList.SelectedItem == null) return;
        //    // получаем выделенный объект
        //    Trainer trainer = trainersList.SelectedItem as Trainer;

        //    TrainerWindow trainerWindow = new TrainerWindow(new Trainer
        //    {
        //        FullName = trainer.FullName,
        //        Id = trainer.Id
        //    });



        //    if (trainerWindow.ShowDialog() == true)
        //    {
        //        trainer = dataBase.Trainers.Find(trainerWindow.changedTrainer.Id);
        //        if (trainer != null)
        //        {
        //            trainer.FullName = trainerWindow.changedTrainer.FullName;
        //            dataBase.Entry(trainer).State = EntityState.Modified;
        //            dataBase.SaveChanges();
        //        }
        //    }
        //}

        //private void DeleteTrainer_Click(object sender, RoutedEventArgs e)
        //{
        //    // если ни одного объекта не выделено, выходим
        //    if (teamsList.SelectedItem == null) return;
        //    // получаем выделенный объект
        //    Team team = teamsList.SelectedItem as Team;
        //    dataBase.Teams.Remove(team);
        //    dataBase.SaveChanges();
        //}

        #endregion


        private void dg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (membersGrid.SelectedItem != null)
                {
                    MemberViewModel memberItem = (MemberViewModel)membersGrid.SelectedItem;
                    competitionVM.AllMembers.Remove(memberItem);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //competitionVM.SortAllMembers();
            ////информация
            //tabControlBoys.ItemsSource = null;
            //tabControlGirls.ItemsSource = null;
            
            //tabControlBoys.ItemsSource = competitionVM.Competition.CategoriesB;
            //tabControlGirls.ItemsSource = competitionVM.Competition.CategoriesG;

        }

        private void toure1_Button_Click(object sender, RoutedEventArgs e)
        {
        }

        private void cellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            //MemberViewModel member;
            //member = ((MemberViewModel)membersGrid.SelectedItem);
            //member.Member.WeightCategory = competitionVM.GetWeightCategory(member.Member.Weight, member.Member.Gender).ToString();
        }


        private void membersGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void findInDBButton_Click(object sender, RoutedEventArgs e)
        {
            bdMembersList.ItemsSource = competitionVM.GetInfoAboutMembersByParam(searchePanel.Text);
        }

        private void addMemberButton_Click(object sender, RoutedEventArgs e)
        {
            MemberViewModel member = (MemberViewModel)bdMembersList.SelectedItem;
            competitionVM.AllMembers.Add(member);
        }

        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            searchePanel.Clear();
        }

        private void sv_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            sv.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
            ScrollViewer scrollviewer = sender as ScrollViewer;
            if (e.Delta > 0)
            {
                scrollviewer.LineUp();
            }
            else
            {
                scrollviewer.LineDown();
            }
            e.Handled = true;
        }

        private void getResultsButton_Click(object sender, RoutedEventArgs e)
        {
            TabResultBL.DataContext = null;
            TabResultBR.DataContext = null;
            TabResultGL.DataContext = null;
            TabResultGR.DataContext = null;

            //турнирные
            TabResultBL.DataContext = competitionVM.CompetitionLeftHand.CategoriesB;
            TabResultBR.DataContext = competitionVM.CompetitionRightHand.CategoriesB;
            TabResultGL.DataContext = competitionVM.CompetitionLeftHand.CategoriesG;
            TabResultGR.DataContext = competitionVM.CompetitionRightHand.CategoriesG;

            competitionVM.GetAllPoints();
        }

        private void ColumnDefinition_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            competitionVM.SortAllMembers();

            TabBoysLeftHand.DataContext = null;
            TabBoysRighHand.DataContext = null;
            TabGirlsLeftHand.DataContext = null;
            TabGirlsRighHand.DataContext = null;

            //турнирные
            TabBoysLeftHand.DataContext = competitionVM.CompetitionLeftHand.CategoriesB;
            TabBoysRighHand.DataContext = competitionVM.CompetitionRightHand.CategoriesB;
            TabGirlsLeftHand.DataContext = competitionVM.CompetitionLeftHand.CategoriesG;
            TabGirlsRighHand.DataContext = competitionVM.CompetitionRightHand.CategoriesG;

            //ComandScore.DataContext = competitionVM.

        }

        private void findInDBButton_Click2(object sender, RoutedEventArgs e)
        {
            DBList.ItemsSource = competitionVM.GetInfoAboutMembersByParam(searchDB.Text);
        }

        private void resetButton_Click2(object sender, RoutedEventArgs e)
        {
            searchDB.Text = "";
        }

        private void saveParametrsCompetition_Click(object sender, RoutedEventArgs e)
        {
            competitionVM = new CompetitionViewModel(ageCategoryCB.SelectedValue.ToString(), pointsCB.SelectedValue.ToString(),
                competitionName.Text, competitionLocation.Text, competitionDate.DisplayDate, judges.Text, secr.Text);
            membersGrid.ItemsSource = competitionVM.AllMembers;
            GenderComboBox.ItemsSource = competitionVM.CompetitionLeftHand.Genders;
            HandComboBox.ItemsSource = competitionVM.CompetitionLeftHand.Hands;
            QualificationComboBox.ItemsSource = competitionVM.DataBaseModel.GetAllQualifications();
            RegionComboBoxz.ItemsSource = competitionVM.DataBaseModel.GetAllRegions();
            TeamComboBox.ItemsSource = competitionVM.DataBaseModel.GetAllTeams();
            membersGrid.CellEditEnding += cellEditEnding;

            //result = new ResultViewModel(competitionVM);

        }

        private void pointsCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void saveButton(object sender, RoutedEventArgs e)
        {       
            PrintDialog dlg = new PrintDialog();

                Window currentMainWindow = Application.Current.MainWindow;

                Application.Current.MainWindow = this;

                if ((bool)dlg.ShowDialog().GetValueOrDefault())
                {
                    Application.Current.MainWindow = currentMainWindow; // do it early enough if the 'if' is entered
                    dlg.PrintTicket.PageOrientation = System.Printing.PageOrientation.Landscape;
            }
        }
    }
}
