using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArmBazaProject.Entities;
using ArmBazaProject.Models;

namespace ArmBazaProject.ViewModels
{
    class ResultViewModel : NotifyableObject
    {
        Result result;

        #region свойства доступв
      
        public Result Result
        {
            get { return result; }
            set
            {
                if (result != value)
                {
                    result = value;
                    OnPropertyChanged("Result");
                }
            }
        }

        

        #endregion

        public ResultViewModel()
        {
            result = new Result();

        }

        private void GetData(CategoryViewModel[] categories, ObservableCollection<MemberViewModel>[] memberViewModels)
        {
            for (int i = 0; i < categories.Length; i++)
            {
                memberViewModels[i] = categories[i].PlaceMembers;
            }
        }

        //private void GetAllData()
        //{
        //    GetData(competitionVM.CompetitionLeftHand.CategoriesB, scoreMembersLeftB);
        //    GetData(competitionVM.CompetitionRightHand.CategoriesB, scoreMembersRightB);
        //    GetData(competitionVM.CompetitionLeftHand.CategoriesG, scoreMembersLeftG);
        //    GetData(competitionVM.CompetitionRightHand.CategoriesG, scoreMembersRightG);
        //}

        public void GetAllPoints()
        {
            //GetPoints(competitionVM.CompetitionLeftHand);
            //GetPoints(competitionVM.CompetitionRightHand);
        }

        private void GetPoints(Сompetition competition)
        {
            for (int i = 0; i < competition.CategoriesB.Length; i++)
            {
                for (int j = 0; j < competition.CategoriesB[i].PlaceMembers.Count; j++)
                {
                    for (int k = 0; k < competition.pointsDatas.Count; k++)
                    {
                        if (competition.CategoriesB[i].PlaceMembers[j].Place == competition.pointsDatas[k].Place)
                        {
                            competition.CategoriesB[i].PlaceMembers[j].Score = competition.pointsDatas[k].Points;
                            break;
                        }
                        else
                        {
                            competition.CategoriesB[i].PlaceMembers[j].Score = 1;
                        }
                    }

                }
            }

            for (int i = 0; i < competition.CategoriesG.Length; i++)
            {
                for (int j = 0; j < competition.CategoriesG[i].PlaceMembers.Count; j++)
                {
                    for (int k = 0; k < competition.pointsDatas.Count; k++)
                    {
                        if (competition.CategoriesG[i].PlaceMembers[j].Place == competition.pointsDatas[k].Place)
                        {
                            competition.CategoriesG[i].PlaceMembers[j].Score = competition.pointsDatas[k].Points;
                        }
                        else
                        {
                            competition.CategoriesG[i].PlaceMembers[j].Score = 1;
                        }
                    }

                }
            }
        }

        public void getAllResults()
        {
            GetAllPoints();
           // GetAllData();

        }
    }
}
