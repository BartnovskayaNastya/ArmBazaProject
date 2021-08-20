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
        CompetitionViewModel competition;
        CategoryViewModel[] resultCategoryGirls;
        CategoryViewModel[] resultCategoryBoys;

        private int[] protocolPoints = new int[] { 25, 17, 9, 5, 3, 2 };


        #region свойства доступа

        public CategoryViewModel[] ResultCategoryBoys
        {
            get { return resultCategoryBoys; }
            set
            {
                if (resultCategoryBoys != value)
                {
                    resultCategoryBoys = value;
                    OnPropertyChanged("ResultCategoryBoys");
                }
            }
        }

        public CategoryViewModel[] ResultCategoryGirls
        {
            get { return resultCategoryGirls; }
            set
            {
                if (resultCategoryGirls != value)
                {
                    resultCategoryGirls = value;
                    OnPropertyChanged("ResultCategoryGirls");
                }
            }
        }

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

        public ResultViewModel(CompetitionViewModel competition)
        {
            result = new Result();
            this.competition = competition;
            resultCategoryGirls = new CategoryViewModel[competition.CompetitionLeftHand.GirlsWeights.Length];
            resultCategoryBoys = new CategoryViewModel[competition.CompetitionLeftHand.BoysWeights.Length];
        }

        public void GetResults()
        {
            //установили для всех рук очки
            SetPoints(competition.CompetitionLeftHand.CategoriesB);
            SetPoints(competition.CompetitionLeftHand.CategoriesG);
            SetPoints(competition.CompetitionRightHand.CategoriesB);
            SetPoints(competition.CompetitionRightHand.CategoriesG);

            //сбор информации вместе

            SetAllPointsForMembers(competition.CompetitionLeftHand.CategoriesB, competition.CompetitionRightHand.CategoriesB, resultCategoryBoys);
            SetAllPointsForMembers(competition.CompetitionLeftHand.CategoriesG, competition.CompetitionRightHand.CategoriesG, resultCategoryGirls);

            SetResultHandsPoints(resultCategoryBoys);
            SetResultHandsPoints(resultCategoryGirls);

            GetTotalResultByHand(resultCategoryBoys);
            GetTotalResultByHand(resultCategoryGirls);

            GetCategotyTeams(resultCategoryGirls, competition.CompetitionLeftHand.CategoriesG, competition.CompetitionRightHand.CategoriesG);
            GetCategotyTeams(resultCategoryBoys, competition.CompetitionLeftHand.CategoriesB, competition.CompetitionRightHand.CategoriesB);

            GetResultTeam(resultCategoryBoys);
            GetResultTeam(resultCategoryGirls);
        }

        public void GetProtocolResults()
        {
            GetProtocolPoints(ResultCategoryGirls);
            GetProtocolPoints(ResultCategoryBoys);

        }

        #region hand results

        //назначение очков для левой и правой руки для каждого участника
        private void SetAllPointsForMembers(CategoryViewModel[] categoriesLeft, CategoryViewModel[] categoriesRight,
            CategoryViewModel[] resultCategory)
        {
            MemberViewModel member;

            //проверка по левой руке
            for (int i = 0; i < categoriesLeft.Length; i++)
            {
                resultCategory[i] = new CategoryViewModel(categoriesLeft[i].WeightCategory.CategoryWeight,
                    categoriesLeft[i].WeightCategory.WeightName);
                for (int j = 0; j < categoriesLeft[i].PlaceMembers.Count; j++)
                {
                    member = (MemberViewModel)categoriesLeft[i].PlaceMembers[j].Clone();
                    if(member.isLeftHand && member.isRightHand)
                    {
                        member.LeftHandScore = categoriesLeft[i].PlaceMembers[j].Score;
                        member.LeftHandPlace = categoriesLeft[i].PlaceMembers[j].Place;
                        for (int k = 0; k < categoriesRight[i].PlaceMembers.Count; k++)
                        { 
                            if(CheckMember(categoriesLeft[i].PlaceMembers[j], categoriesRight[i].PlaceMembers[k]))
                            {
                                member.RightHandScore = categoriesRight[i].PlaceMembers[k].Score;
                                member.RightHandPlace = categoriesRight[i].PlaceMembers[k].Place;
                            }
                        }
                        
                    }
                    else if(member.isLeftHand && !member.isRightHand)
                    {
                        member.LeftHandScore = categoriesLeft[i].PlaceMembers[j].Score;
                        member.LeftHandPlace = categoriesLeft[i].PlaceMembers[j].Place;
                        member.RightHandScore = 0;
                        member.RightHandPlace = 0;
                    }
                    resultCategory[i].ResultMembers.Add(member);

                }
            }

            for (int i = 0; i < categoriesRight.Length; i++)
            {
                for (int j = 0; j < categoriesRight[i].PlaceMembers.Count; j++)
                {
                    if (categoriesRight[i].PlaceMembers[j].isRightHand && !categoriesRight[i].PlaceMembers[j].isLeftHand)
                    {
                        member = (MemberViewModel)categoriesRight[i].PlaceMembers[j].Clone();
                        member.RightHandScore = categoriesRight[i].PlaceMembers[j].Score;
                        member.RightHandPlace = categoriesRight[i].PlaceMembers[j].Place;
                        member.LeftHandScore = 0;
                        member.LeftHandPlace = 0;
                        resultCategory[i].ResultMembers.Add(member);
                    }
                }
            }

        }


        // назначение финальных очков по 2 рукам для каждого участника
        private void SetResultHandsPoints(CategoryViewModel[] categories)
        {
            for (int i = 0; i < categories.Length; i++)
            {
                for (int j = 0; j < categories[i].ResultMembers.Count; j++)
                {
                    categories[i].ResultMembers[j].ResultHandScore = 
                        categories[i].ResultMembers[j].LeftHandScore + categories[i].ResultMembers[j].RightHandScore;
                }
            }
        }

        //проверка участника на существование в листе
        private bool CheckMember(MemberViewModel member1, MemberViewModel member2)
        {
            if(member1.Member.FullName == member2.Member.FullName)
                //&&
                //member1.Member.DateOfBirth == member2.Member.DateOfBirth &&
               // member1.Member.Team.Name == member2.Member.Team.Name)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        //назначение очков для участников в зависимости от занятого места
        private void SetPoints(CategoryViewModel[] category)
        {
            for (int i = 0; i < category.Length; i++)
            {
                for (int j = 0; j < category[i].PlaceMembers.Count; j++)
                {
                    for (int k = 0; k < competition.CompetitionLeftHand.Points.Length; k++)
                    {
                        if (category[i].PlaceMembers[j].Place == k+1)
                        {
                            category[i].PlaceMembers[j].Score = competition.CompetitionLeftHand.Points[k];
                            break;
                        }
                        if (competition.CompetitionLeftHand.Points[0] == 36)
                        {
                            category[i].PlaceMembers[j].Score = 1;
                        }
                        else
                        {
                            category[i].PlaceMembers[j].Score = 0;
                        }
                    }

                }
            }
        }
        
        //сортировка участников по возрастанию
        private void SortMembers(CategoryViewModel[] categories)
        {
            MemberViewModel temp;
            for (int t = 0; t < categories.Length; t++)
            {
                for (int i = 0; i < categories[t].ResultMembers.Count - 1; i++)
                {
                    for (int j = i + 1; j < categories[t].ResultMembers.Count; j++)
                    {
                        if (categories[t].ResultMembers[i].ResultHandScore > categories[t].ResultMembers[j].ResultHandScore)
                        {
                            temp = categories[t].ResultMembers[i];
                            categories[t].ResultMembers[i] = categories[t].ResultMembers[j];
                            categories[t].ResultMembers[j] = temp;
                        }
                    }
                }
            }
        }


        //назначение места для каждого участника по сумме 2-х рук
        private void GetTotalResultByHand(CategoryViewModel[] categories)
        {
            int place = 0;
            SortMembers(categories);
            for (int i = 0; i < categories.Length; i++)
            {
                for (int j = 0; j < categories[i].ResultMembers.Count; j++)
                {
                    place = categories[i].ResultMembers.Count - j;
                    categories[i].ResultMembers[j].ResultHandPlace = place;
                }
            }

        }

        /// <summary>
        /// Team block
        /// </summary>
        /// <param name="categories"></param>
        //получение мест для команд
        private void GetResultTeam(CategoryViewModel[] categories)
        {
            MemberViewModel member;
            for (int i = 0; i < categories.Length; i++)
            {
                for (int j = 0; j < categories[i].ResultMembers.Count; j++)
                {
                    for (int k = 0; k < categories[i].Teams.Count; k++)
                    {
                        if(categories[i].ResultMembers[j].TeamName == categories[i].Teams[k].Name)
                        {
                            member = (MemberViewModel)categories[i].ResultMembers[j].Clone();
                            categories[i].Teams[k].Score += member.ResultHandScore;
                            categories[i].Teams[k].Members.Add(member);
                        }
                    }
                }
            }

            SetPlaceTeam(categories);
        }

        //сортировка команд по очкам участников
        private void SortTeams(CategoryViewModel[] categories)
        {
            TeamModel temp;
            for (int t = 0; t < categories.Length; t++)
            {
                for (int i = 0; i < categories[t].Teams.Count - 1; i++)
                {
                    for (int j = i + 1; j < categories[t].Teams.Count; j++)
                    {
                        if (categories[t].Teams[i].Score > categories[t].Teams[j].Score)
                        {
                            temp = categories[t].Teams[i];
                            categories[t].Teams[i] = categories[t].Teams[j];
                            categories[t].Teams[j] = temp;
                        }
                    }
                }
            }
        }

        //назначение места для команды
        private void SetPlaceTeam(CategoryViewModel[] categories)
        {
            int place = 0;
            SortTeams(categories);
            for (int i = 0; i < categories.Length; i++)
            {
                for (int j = 0; j < categories[i].Teams.Count; j++)
                {
                    place = categories[i].Teams.Count - j;
                    categories[i].Teams[j].Place = place;
                }
            }
        }

        private void GetCategotyTeams(CategoryViewModel[] categories, CategoryViewModel[] categoriesL, CategoryViewModel[] categoriesR)
        {
            TeamModel team;
            bool isExist = false;
            int index = 0;
            for (int i = 0; i < categoriesL.Length; i++)
            {
                for (int j = 0; j < categoriesL[i].Teams.Count; j++)
                {
                    team = new TeamModel(categoriesL[i].Teams[j].Name);
                    categories[i].Teams.Add(team);
                }
            }

            for (int i = 0; i < categoriesR.Length; i++)
            {
                for (int j = 0; j < categoriesR[i].Teams.Count; j++)
                {
                    for (int k = 0; k < categories.Length; k++)
                    {
                        if(k == i)
                        {
                            for (int m = 0; m < categories[k].Teams.Count; m++)
                            {
                                if(!categories[k].CheckTeam(categoriesR[i].Teams[j]))
                                {
                                    team = new TeamModel(categoriesR[i].Teams[j].Name);
                                    categories[k].Teams.Add(team);
                                }
                            }
                        }
                        
                    }
                }
            }
        }

        #endregion

        private void GetProtocolPoints(CategoryViewModel[] categories)
        {
            int place;
            int pointsPlace = 0;
            int placeLeft = 0;
            int placeRight = 0;
            for(int i = 0; i < categories.Length; i++)
            {
                for (int j = 0; j < categories[i].ResultMembers.Count; j++)
                {
                    for (int k = 0; k < protocolPoints.Length; k++)
                    {
                        if (categories[i].ResultMembers[j].IsSportTeamLeftHand)
                        {
                            place = categories[i].ResultMembers[j].LeftHandPlace;
                            pointsPlace = k;
                            pointsPlace++;
                            if(place == pointsPlace)
                            {
                                placeLeft = protocolPoints[k];
                            }
                            if (place > 6)
                            {
                                placeLeft = 0;
                            }

                            categories[i].ResultMembers[j].LeftHandSTScore = placeLeft;
                            categories[i].ResultMembers[j].LeftHandSTScoreVM = placeLeft.ToString();



                        }
                        if (categories[i].ResultMembers[j].IsSportTeamRightHand)
                        {
                            place = categories[i].ResultMembers[j].RightHandPlace;
                            pointsPlace = k;
                            pointsPlace++;
                            if (place == pointsPlace)
                            {
                                placeRight = protocolPoints[k];
                            }
                            if (place > 6)
                            {
                                placeRight = 0;
                            }

                            categories[i].ResultMembers[j].RightHandSTScore = placeRight;
                            categories[i].ResultMembers[j].RightHandSTScoreVM = placeRight.ToString();
                        }
                        if (!categories[i].ResultMembers[j].IsSportTeamRightHand && !categories[i].ResultMembers[j].IsSportTeamLeftHand)
                        {
                            categories[i].ResultMembers[j].RightHandSTScoreVM = "Л";
                            categories[i].ResultMembers[j].LeftHandSTScoreVM = "Л";
                        }
                    }
                }
            }
        }

    }
}
