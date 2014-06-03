using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace TeamToyTool.Data
{

    class SqlManager
    {
        private const string CONNECTION_URL = "server=115.28.16.204;User Id=root;password=OysmXGo@0Z@@;Database=teamtoy";
        private const string SELECT_USER = "select * from user where id > 1";
        private const string SELECT_TODO_FOR_EACH_USER = "select * from todo where owner_uid = ";
        private const string SELECT_COMMENT_FOR_EACH_TODO = "select * from todo_history where type = 2 and tid = ";

        private MySqlConnection mConnection;
        private MySqlCommand mCommand;
        private MySqlDataReader mDataReader;

        private DataManager mDataManager;

        public SqlManager()
        {
            mDataManager = new DataManager();

            createConnection();
            getUsers();
            getToDoForEachUser();
            getCommentForEachToDo();

            ExcelManager excelManager = new ExcelManager(mDataManager);
        }

        private void createConnection()
        {
            try
            {
                mConnection = new MySqlConnection(CONNECTION_URL);
                mCommand = new MySqlCommand();
                mCommand.Connection = mConnection;
            }
            catch (MySqlException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        public void getUsers()
        {
            try
            {
                mConnection.Open();
                mCommand.CommandText = SELECT_USER;
                mDataReader = mCommand.ExecuteReader();
                while(mDataReader.Read())
                {
                    int id = int.Parse(mDataReader["id"].ToString());
                    string name = mDataReader["name"].ToString();
                    User user = new User(id, name);
                    mDataManager.addUser(user);
                }
                mDataReader.Close();
                mConnection.Close();
            }
            catch (MySqlException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        public void getToDoForEachUser()
        {
            try
            {
                mConnection.Open();
                foreach (User user in mDataManager.mUsers)
                {
                    mCommand.CommandText = SELECT_TODO_FOR_EACH_USER + user.id;
                    mDataReader = mCommand.ExecuteReader();
                    while(mDataReader.Read())
                    {
                        int id = int.Parse(mDataReader["id"].ToString());
                        string content = mDataReader["content"].ToString();
                        int comment_count = int.Parse(mDataReader["comment_count"].ToString());
                        ToDo todo = new ToDo(id, content, user.id, comment_count);
                        user.addTodo(todo);
                    }
                    mDataReader.Close();
                }
                mConnection.Close();
            }
            catch (MySqlException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        public void getCommentForEachToDo()
        {
            try
            {
                mConnection.Open();
                foreach (User user in mDataManager.mUsers)
                {
                    foreach (ToDo todo in user.mToDoList)
                    {
                        mCommand.CommandText = SELECT_COMMENT_FOR_EACH_TODO + todo.id;
                        mDataReader = mCommand.ExecuteReader();
                        while (mDataReader.Read())
                        {
                            int id = int.Parse(mDataReader["id"].ToString());
                            int uid = int.Parse(mDataReader["uid"].ToString());
                            string content = mDataReader["content"].ToString();
                            Comment comment = new Comment(id, todo.id, uid, content);
                            todo.addComment(comment);
                        }
                        todo.validateScore();
                        todo.validateDate();
                        Console.WriteLine(user.name + ": " + todo.month + "." + todo.day + " " + todo.score + "分， " + todo.content);
                        mDataReader.Close();
                    }
                }
                mConnection.Close();
            }
            catch (MySqlException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

    }
}
