using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ArmBazaProject.Entities
{
    class Serializator
    {
        BinaryFormatter formatter;
        ObservableCollection<MemberViewModel> fileMembers;
        ObservableCollection<MemberViewModel> savedMembers;


        public Serializator()
        {
            formatter = new BinaryFormatter();
            fileMembers = new ObservableCollection<MemberViewModel>();
            savedMembers = new ObservableCollection<MemberViewModel>();
        }

        public void SaveData(ObservableCollection<MemberViewModel> members)
        {
            int k = 0;
            using (FileStream fs = new FileStream("members.dat", FileMode.OpenOrCreate))
            {
                if (fs.Length >= 1)
                {
                    fileMembers = savedMembers;
                    if (fileMembers.Count == members.Count)
                    {
                        formatter.Serialize(fs, members);
                    }
                    else if (fileMembers.Count < members.Count)
                    {
                        for(int i = 0; i < fileMembers.Count; i++)
                        {
                            fileMembers[i] = (MemberViewModel)members[i].Clone();
                        }

                        k = fileMembers.Count;

                        for (int j = k; j < members.Count; j++)
                        {
                            fileMembers.Add((MemberViewModel)members[j].Clone());
                        }

                        formatter.Serialize(fs, fileMembers);
                    }
                }
                else
                {
                    formatter.Serialize(fs, members);
                }
            }
            
        }

        public ObservableCollection<MemberViewModel> OpenData()
        {
            using (FileStream fs = new FileStream("members.dat", FileMode.OpenOrCreate))
            {
                if(fs.Length >= 1)
                {
                    savedMembers = (ObservableCollection<MemberViewModel>)formatter.Deserialize(fs);
                }
                
            }
            return savedMembers;
        }
    }
}
