using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamToyWPF.Data
{
    class User
    {
        public int id { get; set; }
        public string name { get; set; }

        public List<ToDo> mToDoList { get; set; }

        public User(int id, string name)
        {
            this.id = id;
            this.name = name;

            mToDoList = new List<ToDo>();
        }

        public void addTodo(ToDo todo)
        {
            mToDoList.Add(todo);
        }

        public List<ToDo> getValidatedToDoList(int month)
        {
            List<ToDo> result = new List<ToDo>();
            foreach (ToDo todo in mToDoList)
            {
                if (todo.isTimeSet)
                {
                    if (todo.month == month)
                    {
                        result.Add(todo);
                    }
                }
            }
            return result;
        }
    }
}
