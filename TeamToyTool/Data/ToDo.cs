using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TeamToyTool.Data
{
    class ToDo
    {
        public int id { get; set; }
        public string content { get; set; }
        public int owner_uid { get; set; }
        public int comment_count { get; set; }
        public int score { get; set; }
        public bool isScoreSet { get; set; }
        public bool isTimeSet { get; set; }
        public int month { get; set; }
        public int day { get; set; }

        public List<Comment> mComments { get; set; }

        private static readonly Regex mRegex = new Regex("[0-9]{1,2}.[0-9]{1,2}");

        public ToDo(int id, string content, int owner_id, int comment_count)
        {
            this.id = id;
            this.content = content;
            this.owner_uid = owner_uid;
            this.comment_count = comment_count;
            score = 0;
            isScoreSet = false;
            isTimeSet = false;

            mComments = new List<Comment>();
        }

        public void addComment(Comment comment)
        {
            mComments.Add(comment);
        }

        public void validateScore()
        {
            foreach (Comment comment in mComments)
            {
                if (!comment.content.Equals(""))
                {
                    if (DataManager.MANAGER_IDS.Contains(comment.uid))
                    {
                        string value = comment.content.ToCharArray()[0].ToString();
                        int tempScore = 0;
                        if (int.TryParse(value, out tempScore))
                        {
                            score = tempScore;
                            isScoreSet = true;
                        }
                    }
                }
            }
        }

        public void validateDate()
        {
            Match match = mRegex.Match(content);
            if(match.Success && content.Substring(0, 1).Equals("["))
            {
                isTimeSet = true;
                string dateString = content.Substring(1, content.IndexOf(']') - 1);
                int indexOfDot = dateString.IndexOf('.');
                month = int.Parse(dateString.Substring(0, indexOfDot));
                day = int.Parse(dateString.Substring(indexOfDot + 1, dateString.Length - indexOfDot - 1));
            }
        }

    }
}
