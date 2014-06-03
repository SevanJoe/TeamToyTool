using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamToyTool.Data
{
    class DataManager
    {
        public static readonly List<int> MANAGER_IDS = new List<int> { 2, 6 };

        public List<User> mUsers { get; set; }

        public DataManager()
        {
            mUsers = new List<User>();
        }

        public void addUser(User user)
        {
            mUsers.Add(user);
        }
    }
}
