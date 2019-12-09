using System;
using System.Collections.Generic;
using System.Text;

namespace Rich.Order.Application.UserAppService
{
    public class PagePermissionViewModel
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string Component { get; set; }
        public string Name { get; set; }
        public string Redirect { get; set; }
        public bool AlwaysShow { get; set; }
        public MetaNode Meta { get; set; }
        public List<ChildNode> Children { get; set; }
    }

    public class MetaNode
    {

        public string Title { get; set; }
        public string Icon { get; set; }
        public List<string> Roles { get; set; }
        public bool NoCache { get; set; }
    }

    public class ChildNode
    {
        public string Path { get; set; }
        public string Component { get; set; }
        public string Name { get; set; }
        public MetaNode Meta { get; set; }
    }
}
