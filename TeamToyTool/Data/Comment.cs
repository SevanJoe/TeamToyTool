using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamToyTool.Data
{
    class Comment
    {
        public int id { get; set; }
        public int tid { get; set; }
        public int uid { get; set; }
        public string content { get; set; }

        public Comment(int id, int tid, int uid, string content)
        {
            this.id = id;
            this.tid = tid;
            this.uid = uid;
            this.content = content;
        }
    }
}
