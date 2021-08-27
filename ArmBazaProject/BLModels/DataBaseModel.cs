using ArmBazaProject.BDModels;
using ArmBazaProject.Models;
using System.Collections.Generic;
using System.Linq;

namespace ArmBazaProject
{
    class DataBaseModel
    {
        public DataBaseModel()
        {
        }

        public IEnumerable<Member> GetAllMembers()
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                return context.Members.ToArray();
            }
            
        }

        public IEnumerable<Qualification> GetAllObjectQualifications()
        {
            using (ApplicationContext context = new ApplicationContext())
            {

                return context.Qualifications.ToArray();
            }
        }

        public IEnumerable<Team> GetAllObjectTeams()
        {
            using (ApplicationContext context = new ApplicationContext())
            {

                return context.Teams.ToArray();
            }
        }



        public string[] GetAllQualifications()
        {
            string[] names;
            Qualification[] qualifications;
            using (ApplicationContext context = new ApplicationContext())
            {
                qualifications = context.Qualifications.ToArray();
                names = new string[qualifications.Length];
                for(int i = 0; i < qualifications.Length; i++)
                {
                    names[i] = qualifications[i].Name;
                }
                return names;
            }
            

        }

        public string[] GetAllRegions()
        {
            string[] names;
            Region[] regions;
            using (ApplicationContext context = new ApplicationContext())
            {
                regions = context.Regions.ToArray();
                names = new string[regions.Length];
                for (int i = 0; i < regions.Length; i++)
                {
                    names[i] = regions[i].Name;
                }
                
                return names;
            }

        }

        public List<Category> GetAllCategories(string gender, string categoryName)
        {
            List<Category> categories;
            using (ApplicationContext context = new ApplicationContext())
            {
                categories = context.Categories.Where(category => category.Name == categoryName && category.Gender == gender).ToList();     
            }
            
            return categories;
        }

        public List<Points> GetAllPoints(string pointName)
        {
            List<Points> points;
            using (ApplicationContext context = new ApplicationContext())
            {
                points = context.Points.Where(point => point.Name == pointName).ToList();
            }

            return points;

        }

        public IEnumerable<MemberViewModel> GetAllMembersByParam(string param)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                List<Member> newList = new List<Member>();
                List<MemberViewModel> memberVM = new List<MemberViewModel>();
                MemberViewModel viewModel;
                foreach (var m in context.Members)
                {
                    if (m.FullName.ToLower().Contains(param.ToLower()))
                        newList.Add(m);
                }

                for(int i = 0; i< newList.Count; i++)
                {
                    viewModel = new MemberViewModel();
                    viewModel.Member = newList[i];
                    viewModel.QualificationName = newList[i].Qualification.Name;
                    viewModel.TrainerName = newList[i].Team.TrainerName;
                    viewModel.TeamName = newList[i].Team.Name;
                    viewModel.RegionName = newList[i].Team.Region.Name;
                    memberVM.Add(viewModel);
                }
                return memberVM;
            }
            
        }


        public string[] GetAllTeams()
        {
            string[] names;
            Team[] teams;
            using (ApplicationContext context = new ApplicationContext())
            {
                teams = context.Teams.ToArray();
                names = new string[teams.Length];
                for (int i = 0; i < teams.Length; i++)
                {
                    names[i] = teams[i].Name;
                }
                return names;
            }
        }

        public IEnumerable<Team> GetAllTeams(string region)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                return context.Teams.Where(t => t.Region.Name == region).ToArray();
            }
        }

        public IEnumerable<Category> GetAllCategories()
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                return context.Categories.ToArray();
            }
        }




    }
}
