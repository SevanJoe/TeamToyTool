using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using TeamToyTool.Data;

namespace TeamToyTool
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlManager sqlManager = new SqlManager();
            sqlManager.getUsers();
            sqlManager.getToDoForEachUser();
            sqlManager.getCommentForEachToDo();
        }
    }
}
