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
        DataBaseModel dataBaseModel;
        Result result;
        CompetitionViewModel competition;
        CategoryViewModel[] resultCategoryGirls;
        CategoryViewModel[] resultCategoryBoys;

        private ObservableCollection<ProtocolTeam> summaryTeamsG;
        private ObservableCollection<ProtocolTeam> summaryTeamsB;

        private ObservableCollection<ProtocolTeam> resultSummaryTeams;

        private string[] teamNames;

        private int[] protocolPoints = new int[] { 25, 17, 9, 5, 3, 2 };
        int equalizer = -25;


        #region свойства доступа

        public ObservableCollection<ProtocolTeam> ResultSummaryTeams
        {
            get { return resultSummaryTeams; }
            set
            {
                if (resultSummaryTeams != value)
                {
                    resultSummaryTeams = value;
                    OnPropertyChanged("ResultSummaryTeams");
                }
            }
        }

        public ObservableCollection<ProtocolTeam> SummaryTeamsB
        {
            get { return summaryTeamsB; }
            set
            {
                if (summaryTeamsB != value)
                {
                    summaryTeamsB = value;
                    OnPropertyChanged("SummaryTeamsB");
                }
            }
        }
        public ObservableCollection<ProtocolTeam> SummaryTeamsG
        {
            get { return summaryTeamsG; }
            set
            {
                if (summaryTeamsG != value)
                {
                    summaryTeamsG = value;
                    OnPropertyChanged("SummaryTeamsG");
                }
            }
        }

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
            dataBaseModel = new DataBaseModel();
            this.competition = competition;
            resultCategoryGirls = new CategoryViewModel[competition.CompetitionLeftHand.GirlsWeights.Length];
            resultCategoryBoys = new CategoryViewModel[competition.CompetitionLeftHand.BoysWeights.Length];
            summaryTeamsG = new ObservableCollection<ProtocolTeam>();
            summaryTeamsB = new ObservableCollection<ProtocolTeam>();
            resultSummaryTeams = new ObservableCollection<ProtocolTeam>();
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

            /////+
            SetResultHandsScore(resultCategoryBoys);
            SetResultHandsScore(resultCategoryGirls);

            ///+
            GetTotalResultByHand(resultCategoryBoys);
            GetTotalResultByHand(resultCategoryGirls);


            //командные очки
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

        public void GetTotalResults()
        {
            GetSummaryProtocolPoints(resultCategoryGirls, summaryTeamsG);
            GetSummaryProtocolPoints(resultCategoryBoys, summaryTeamsB);

            //SetProtocolVMData(summaryTeamsG);
            //SetProtocolVMData(summaryTeamsB);

            resultSummaryTeams =  CollectDataTeams(summaryTeamsB, summaryTeamsG);


            SetPlaceSummaryTeams(resultSummaryTeams);
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
                    if (member.isLeftHand && member.isRightHand)
                    {
                        member.LeftHandScore = categoriesLeft[i].PlaceMembers[j].Score;
                        member.LeftHandPlace = categoriesLeft[i].PlaceMembers[j].Place;
                        member.LeftHandScoreVM = member.LeftHandScore.ToString();
                        member.LeftHandPlaceVM = member.LeftHandPlace.ToString();
                        for (int k = 0; k < categoriesRight[i].PlaceMembers.Count; k++)
                        {
                            if (CheckMember(categoriesLeft[i].PlaceMembers[j], categoriesRight[i].PlaceMembers[k]))
                            {
                                member.RightHandScore = categoriesRight[i].PlaceMembers[k].Score;
                                member.RightHandPlace = categoriesRight[i].PlaceMembers[k].Place;
                                member.RightHandScoreVM = member.RightHandScore.ToString();
                                member.RightHandPlaceVM = member.RightHandPlace.ToString();
                            }
                        }

                        if (member.RightHandScore == 0 && member.LeftHandScore == 0)
                        {
                            member.scoreZero = true;
                        }


                    }
                    else if (member.isLeftHand && !member.isRightHand)
                    {
                        member.LeftHandScore = categoriesLeft[i].PlaceMembers[j].Score;
                        member.LeftHandPlace = categoriesLeft[i].PlaceMembers[j].Place;
                        member.LeftHandScoreVM = member.LeftHandScore.ToString();
                        member.LeftHandPlaceVM = member.LeftHandPlace.ToString();
                        member.RightHandScore = 0 + equalizer;
                        member.RightHandPlace = 0;
                        member.RightHandScoreVM = "";
                        member.RightHandPlaceVM = "";
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
                        member.RightHandScoreVM = member.RightHandScore.ToString();
                        member.RightHandPlaceVM = member.RightHandPlace.ToString();
                        member.LeftHandScore = 0 + equalizer;
                        member.LeftHandPlace = 0;
                        member.LeftHandScoreVM = "";
                        member.LeftHandPlaceVM = "";
                        resultCategory[i].ResultMembers.Add(member);
                    }
                }
            }

        }


        // назначение финальных очков по 2 рукам для каждого участника
        private void SetResultHandsScore(CategoryViewModel[] categories)
        {
            for (int i = 0; i < categories.Length; i++)
            {
                for (int j = 0; j < categories[i].ResultMembers.Count; j++)
                {
                    categories[i].ResultMembers[j].ResultHandScore =
                    categories[i].ResultMembers[j].LeftHandPlace + categories[i].ResultMembers[j].RightHandPlace;

                }
            }
        }

        //проверка участника на существование в листе
        private bool CheckMember(MemberViewModel member1, MemberViewModel member2)
        {
            if (member1.Member.FullName == member2.Member.FullName
                && member1.TeamName == member2.TeamName &&
                member1.Member.Weight == member2.Member.Weight
                )

            //member1.Member.DateOfBirth == member2.Member.DateOfBirth &&
            // )
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
                        if (category[i].PlaceMembers[j].Place == k + 1)
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
            for (int i = 0; i < categories.Length; i++)
            {
                for (int j = 0; j < categories[i].ResultMembers.Count; j++)
                {
                    categories[i].ResultMembers[j].TempResultScoreHands =
                        categories[i].ResultMembers[j].LeftHandScore + categories[i].ResultMembers[j].RightHandScore;
                    if (categories[i].ResultMembers[j].isLeftHand && categories[i].ResultMembers[j].isRightHand && categories[i].ResultMembers[j].scoreZero)
                    {
                        categories[i].ResultMembers[j].TempResultScoreHands += 1;
                    }

                }
            }

            MemberViewModel temp;
            for (int t = 0; t < categories.Length; t++)
            {
                for (int i = 0; i < categories[t].ResultMembers.Count - 1; i++)
                {
                    for (int j = i + 1; j < categories[t].ResultMembers.Count; j++)
                    {
                        if (categories[t].ResultMembers[i].TempResultScoreHands > categories[t].ResultMembers[j].TempResultScoreHands)
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

        #endregion

        #region TeamsScore


        //получение мест для команд
        private void GetResultTeam(CategoryViewModel[] categories)
        {
            MemberViewModel member;
            int score = 0;
            for (int i = 0; i < categories.Length; i++)
            {
                for (int j = 0; j < categories[i].ResultMembers.Count; j++)
                {
                    for (int k = 0; k < categories[i].Teams.Count; k++)
                    {
                        if (categories[i].ResultMembers[j].TeamName == categories[i].Teams[k].Name)
                        {
                            if (categories[i].ResultMembers[j].IsSportTeamLeftHand && categories[i].ResultMembers[j].IsSportTeamRightHand)
                            {
                                score = categories[i].ResultMembers[j].LeftHandScore + categories[i].ResultMembers[j].ResultHandScore;

                            }
                            else if (categories[i].ResultMembers[j].IsSportTeamLeftHand && !categories[i].ResultMembers[j].IsSportTeamRightHand)
                            {
                                score = categories[i].ResultMembers[j].LeftHandScore;
                            }
                            else if (!categories[i].ResultMembers[j].IsSportTeamLeftHand && categories[i].ResultMembers[j].IsSportTeamRightHand)
                            {
                                score = categories[i].ResultMembers[j].RightHandScore;
                            }
                            member = (MemberViewModel)categories[i].ResultMembers[j].Clone();
                            categories[i].Teams[k].Score += score;
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
                        if (k == i)
                        {
                            for (int m = 0; m < categories[k].Teams.Count; m++)
                            {
                                if (!categories[k].CheckTeam(categoriesR[i].Teams[j]))
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

        #region Protocol Points

        private void GetProtocolPoints(CategoryViewModel[] categories)
        {
            int place;
            int pointsPlace = 0;
            int placeLeft = 0;
            int placeRight = 0;
            for (int i = 0; i < categories.Length; i++)
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
                            if (place == pointsPlace)
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
                        else if (!categories[i].ResultMembers[j].IsSportTeamLeftHand && categories[i].ResultMembers[j].isLeftHand)
                        {
                            categories[i].ResultMembers[j].LeftHandSTScoreVM = "Л";
                        }
                        else
                        {
                            categories[i].ResultMembers[j].LeftHandSTScoreVM = "-";
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
                        else if (!categories[i].ResultMembers[j].IsSportTeamRightHand && categories[i].ResultMembers[j].isRightHand)
                        {
                            categories[i].ResultMembers[j].RightHandSTScoreVM = "Л";
                        }
                        else
                        {
                            categories[i].ResultMembers[j].RightHandSTScoreVM = "-";
                        }
                    }
                }
            }
        }

        #endregion


        #region Summary protocol

        private void GetMembersToTeam(CategoryViewModel[] categories)
        {
            for (int i = 0; i < categories.Length; i++)
            {
                for (int j = 0; j < categories[i].Teams.Count; j++)
                {
                    for (int k = 0; k < categories[i].Teams[j].Members.Count; k++)
                    {
                        for (int l = 0; l < categories[i].ResultMembers.Count; l++)
                        {
                            if(categories[i].ResultMembers[l].Member.FullName == categories[i].Teams[j].Members[k].Member.FullName &&
                                categories[i].ResultMembers[l].TeamName == categories[i].Teams[j].Members[k].TeamName)
                            {
                                categories[i].Teams[j].Members[k].LeftHandSTScore = categories[i].ResultMembers[l].LeftHandSTScore;
                                categories[i].Teams[j].Members[k].RightHandSTScore = categories[i].ResultMembers[l].RightHandSTScore;
                            }
                        }
                    }
                }
                
            }

        }

        private void GetSummaryProtocolPoints(CategoryViewModel[] categories, ObservableCollection<ProtocolTeam> summaryTeams)
        {
            int score = 0;
            bool isSportTeam = false;
            ProtocolTeam protocolTeam;
            teamNames = dataBaseModel.GetAllTeams();
            for (int k = 0; k < teamNames.Length; k++)
            {
                protocolTeam = new ProtocolTeam(teamNames[k]);
                summaryTeams.Add(protocolTeam);
            }

            GetMembersToTeam(categories);

            for (int i = 0; i < categories.Length; i++)
            {
                for (int j = 0; j < categories[i].Teams.Count; j++)
                {
                    for (int k = 0; k < summaryTeams.Count; k++)
                    {
                        if (categories[i].Teams[j].Name == summaryTeams[k].Name)
                        {
                            for (int t = 0; t < categories[i].Teams[j].Members.Count; t++)
                            {
                                
                                if (categories[i].Teams[j].Members[t].IsSportTeamLeftHand && categories[i].Teams[j].Members[t].IsSportTeamRightHand)
                                {
                                    isSportTeam = true;
                                    score = categories[i].Teams[j].Members[t].LeftHandSTScore + categories[i].Teams[j].Members[t].RightHandSTScore;
                                }
                                else if (categories[i].Teams[j].Members[t].IsSportTeamLeftHand && !categories[i].Teams[j].Members[t].IsSportTeamRightHand)
                                {
                                    isSportTeam = true;
                                    score = categories[i].Teams[j].Members[t].LeftHandSTScore;
                                }
                                else if (!categories[i].Teams[j].Members[t].IsSportTeamLeftHand && categories[i].Teams[j].Members[t].IsSportTeamRightHand)
                                {
                                    isSportTeam = true;
                                    score = categories[i].Teams[j].Members[t].RightHandSTScore;
                                }
                                if(categories[i].Teams[j].Members[t].Member.Gender == "ж" && isSportTeam)
                                {
                                    summaryTeams[k].ScoreG += score;
                                }
                                else if (categories[i].Teams[j].Members[t].Member.Gender == "м" && isSportTeam)
                                {
                                    summaryTeams[k].ScoreB += score;
                                }
                                isSportTeam = false;
                            }
                        }
                    }
                }
            }
                #region old
                //int leftPoints = 0;
                //int rightPoints = 0;
                //Points pointsL;
                //Points pointsR;
                //ProtocolTeam protocolTeam;
                //teamNames = dataBaseModel.GetAllTeams();
                //for (int k = 0; k < teamNames.Length; k++)
                //{
                //    protocolTeam = new ProtocolTeam(teamNames[k]);
                //    summaryTeams.Add(protocolTeam);
                //}

                //for (int i = 0; i < categories.Length; i++)
                //{
                //    for (int j = 0; j < categories[i].Teams.Count; j++)
                //    {
                //        for (int k = 0; k < summaryTeams.Count; k++)
                //        {
                //            if(categories[i].Teams[j].Name == summaryTeams[k].Name)
                //            {
                //                summaryTeams[k].Сategories.Add(categories[i].WeightCategory.WeightName);
                //                pointsL = new Points();
                //                pointsR = new Points();


                //                for (int t = 0; t < categories[i].Teams[j].Members.Count; t++)
                //                {
                //                    if(categories[i].Teams[j].Members[t].IsSportTeamLeftHand && categories[i].Teams[j].Members[t].IsSportTeamRightHand)
                //                    {
                //                        leftPoints = categories[i].Teams[j].Members[t].LeftHandScore;
                //                        rightPoints = categories[i].Teams[j].Members[t].RightHandScore;
                //                        pointsL.points.Add(leftPoints);
                //                        pointsR.points.Add(rightPoints);

                //                        summaryTeams[k].PointsLeftHand.Add(pointsL);
                //                        summaryTeams[k].PointsRightHand.Add(pointsR);
                //                    }
                //                    else if (categories[i].Teams[j].Members[t].IsSportTeamLeftHand &&
                //                        !categories[i].Teams[j].Members[t].IsSportTeamRightHand)
                //                    {
                //                        leftPoints = categories[i].Teams[j].Members[t].LeftHandScore;
                //                        rightPoints = 0;
                //                        pointsL.points.Add(leftPoints);
                //                        pointsR.points.Add(rightPoints);

                //                        summaryTeams[k].PointsLeftHand.Add(pointsL);
                //                        summaryTeams[k].PointsRightHand.Add(pointsR);
                //                    }
                //                    else if (!categories[i].Teams[j].Members[t].IsSportTeamLeftHand &&
                //                        categories[i].Teams[j].Members[t].IsSportTeamRightHand)
                //                    {
                //                        leftPoints = 0;
                //                        rightPoints = categories[i].Teams[j].Members[t].ResultHandScore;
                //                        pointsL.points.Add(leftPoints);
                //                        pointsR.points.Add(rightPoints);

                //                        summaryTeams[k].PointsLeftHand.Add(pointsL);
                //                        summaryTeams[k].PointsRightHand.Add(pointsR);
                //                    }
                //                }

                //            }
                //        }
                //    }
                //}
                #endregion
        }

        private void SetProtocolVMData(ObservableCollection<ProtocolTeam> summaryTeams)
        {
            string result;
            for (int k = 0; k < summaryTeams.Count; k++)
            {
                for (int i = 0; i < summaryTeams[k].PointsLeftHand.Count; i++)
                {
                    if(summaryTeams[k].PointsLeftHand[i].points.Count >= 2)
                    {
                        result = summaryTeams[k].PointsLeftHand[i].points[0].ToString() + "," +
                       summaryTeams[k].PointsLeftHand[i].points[1].ToString();
                    }
                    else if (summaryTeams[k].PointsLeftHand[i].points.Count == 1)
                    {
                        result = summaryTeams[k].PointsLeftHand[i].points[0].ToString();
                    }
                    else
                    {
                        result = "";
                    }
                    //result = summaryTeams[k].PointsLeftHand[i].points[0].ToString() + "," +
                    //    summaryTeams[k].PointsLeftHand[i].points[1].ToString();

                    summaryTeams[k].PointsLeftHandVM.Add(result);
                }

                for (int i = 0; i < summaryTeams[k].PointsRightHand.Count; i++)
                {
                    if (summaryTeams[k].PointsRightHand[i].points.Count >= 2)
                    {
                        result = summaryTeams[k].PointsRightHand[i].points[0].ToString() + "," +
                        summaryTeams[k].PointsRightHand[i].points[1].ToString();
                    }
                    else if (summaryTeams[k].PointsRightHand[i].points.Count == 1)
                    {
                        result = summaryTeams[k].PointsRightHand[i].points[0].ToString();
                    }
                    else
                    {
                        result = "";
                    }
                   

                    summaryTeams[k].PointsRightHandVM.Add(result);
                }
            }
        }

        private void SortSummaryTeams(ObservableCollection<ProtocolTeam> summaryTeams)
        {
            ProtocolTeam temp;
            for (int i = 0; i < summaryTeams.Count - 1; i++)
            {
                for (int j = i + 1; j < summaryTeams.Count; j++)
                {
                    if (summaryTeams[i].TotalScore > summaryTeams[j].TotalScore)
                    {
                        temp = summaryTeams[i];
                        summaryTeams[i] = summaryTeams[j];
                        summaryTeams[j] = temp;
                    }
                }
            }

        }
        private void SetPlaceSummaryTeams(ObservableCollection<ProtocolTeam> summaryTeams)
        {
            int place = 0;
            SortSummaryTeams(summaryTeams);
            for (int i = 0; i < summaryTeams.Count; i++)
            {
                place = summaryTeams.Count - i;
                summaryTeams[i].TotalPlace = place;
            }
            #region old
            //int result;
            //for (int k = 0; k < summaryTeams.Count; k++)
            //{
            //    for (int i = 0; i < summaryTeams[k].PointsLeftHand.Count; i++)
            //    {
            //        if(summaryTeams[k].PointsLeftHand[i].points.Count >= 2)
            //        {
            //            summaryTeams[k].ResultLeftHand += summaryTeams[k].PointsLeftHand[i].points[0] +
            //                                          summaryTeams[k].PointsLeftHand[i].points[1];
            //        }
            //        else if (summaryTeams[k].PointsLeftHand[i].points.Count == 1)
            //        {
            //            summaryTeams[k].ResultLeftHand = summaryTeams[k].PointsLeftHand[i].points[0];
            //        }
            //        else
            //        {
            //            summaryTeams[k].ResultLeftHand = 0;
            //        }

            //    }

            //    for (int i = 0; i < summaryTeams[k].PointsRightHand.Count; i++)
            //    {
            //        if (summaryTeams[k].PointsRightHand[i].points.Count >= 2)
            //        {
            //            summaryTeams[k].ResultRightHand += summaryTeams[k].PointsRightHand[i].points[0] +
            //                                          summaryTeams[k].PointsRightHand[i].points[1];
            //        }
            //        else if (summaryTeams[k].PointsRightHand[i].points.Count == 1)
            //        {
            //            summaryTeams[k].ResultRightHand = summaryTeams[k].PointsRightHand[i].points[0];
            //        }
            //        else
            //        {
            //            summaryTeams[k].ResultRightHand = 0;
            //        }
            //    }

            //    summaryTeams[k].TotalResult = summaryTeams[k].ResultLeftHand + summaryTeams[k].ResultRightHand;
            //}
            #endregion
        }

        private ObservableCollection<ProtocolTeam> CollectDataTeams(ObservableCollection<ProtocolTeam> summaryTeams1, ObservableCollection<ProtocolTeam> summaryTeams2)
        {
            ObservableCollection<ProtocolTeam> summaryTeams;
            for (int i = 0; i < summaryTeams1.Count; i++)
            {
                summaryTeams2[i].ScoreB = summaryTeams1[i].ScoreB;
            }

            for (int k = 0; k < summaryTeams2.Count; k++)
            {
                summaryTeams2[k].TotalScore = summaryTeams2[k].ScoreB + summaryTeams2[k].ScoreG;
            }
            summaryTeams = summaryTeams2;

            return summaryTeams;
        }


        #endregion
    }
}
