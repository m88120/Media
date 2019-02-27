using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AllYouMedia.Models
{
    public class UserTree
    {
        public UserTree()
        {
            this.TreeNode = new UserNode();
        }
        public UserNode TreeNode { get; set; }
    }
    public class UserNode
    {
        public UserNode()
        {
            this.ChildNodes = new List<UserNode>();
        }
        public string Name { get; set; }
        public string UserName { get; set; }

        public long AspNetUserID { get; set; }

        public List<UserNode> ChildNodes { get; set; }
    }
}