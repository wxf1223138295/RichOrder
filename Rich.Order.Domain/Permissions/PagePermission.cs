﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Rich.Order.Domain.Permissions
{
    public class PagePermission
    {
        public int Id { get; set; } 
        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public int PageId { get; set; }
        public int ParentPageId { get; set; }
        public int LevelId { get; set; }
        /// <summary>
        /// 2,3
        /// </summary>
        public string RoleIds { get; set; }
        public string UserId { get; set; }
        public string Creator { get; set; }
    }

    public class ManagerPage
    {
        public int Id { get; set; }
        public bool AlwaysShow { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public string Creator { get; set; }
        public string PageUrlName { get; set; }
        public string PageUrl { get; set; }
        public string PageShowName { get; set; }
        public string PageDescribe { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public string Redirect { get; set; }
        public bool NoCache { get; set; }
    }
}
