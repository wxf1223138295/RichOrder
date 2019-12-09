using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Rich.Common.Base.RichReturnModel;
using Rich.Order.Domain.Permissions;
using Rich.Order.Domain.User;

namespace Rich.Order.Application.UserAppService
{
    public class RichUserAppService: IRichUserAppService
    {
        private readonly IRichUserRepository _richUserRepository;
        private readonly RoleManager<RichOrderRole> _roleManager;


        public RichUserAppService(IRichUserRepository richUserRepository, RoleManager<RichOrderRole> roleManager)
        {
            _richUserRepository = richUserRepository;
            _roleManager = roleManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<RichBizReturn<List<PagePermissionViewModel>>> GetRouterByRoleId(string roleId="")
        {
            if (string.IsNullOrEmpty(roleId))
            {
                //查询所有
                var managerPages = await _richUserRepository.GetAllManagerPageAsync();
                var permissions = await _richUserRepository.GetAllPermissionAsync();
                //找到最小的levelid为顶级菜单
                var levelids=permissions.Select(p => p.LevelId).Distinct().ToList();
                var topLevel=levelids.Min();
                List<PagePermissionViewModel> pagesList=new List<PagePermissionViewModel>();

                permissions.ToList().ForEach(p =>
                {
                    if (p.LevelId == topLevel)
                    {
                        PagePermissionViewModel viewModel = new PagePermissionViewModel();
                        var item = managerPages.Where(q => q.Id == p.PageId)?.FirstOrDefault();
                        viewModel.Name = item.PageDescribe;
                        if (item.PageUrl == "Layout")
                        {
                            viewModel.Component = "layout/" + item.PageUrl;
                        }
                        else
                        {
                            viewModel.Component = item.PageUrl;
                        }
                      
                        viewModel.Path = item.PageUrlName;
                        viewModel.AlwaysShow = item.AlwaysShow;
                        viewModel.Id = item.Id;
                        viewModel.Redirect = string.IsNullOrEmpty(item.Redirect)?string.Empty: item.Redirect;
                        viewModel.Meta = new MetaNode();
                        viewModel.Meta.Roles = new List<string>();
                        if (p.RoleIds.Contains(","))
                        {
                            var temp = p.RoleIds.Split(',');
                            foreach (var s in temp)
                            {
                                var role=_roleManager.Roles.Where(g => g.Id == s).ToList().FirstOrDefault();
                                viewModel.Meta.Roles.Add(role.Name);
                            }
                        }
                        else
                        {
                            var role = _roleManager.Roles.Where(g => g.Id == p.RoleIds).ToList().FirstOrDefault();
                            viewModel.Meta.Roles.Add(role.Name);
                        }

                        viewModel.Meta.Icon = string.IsNullOrEmpty(item.Icon)
                            ? string.Empty
                            : item.Icon;
                        viewModel.Meta.NoCache = item.NoCache;
                        viewModel.Meta.Title = item.Title;
                        var childs = permissions.Where(o => o.ParentPageId == p.PageId).ToList();
                        //存在子菜单
                        if (childs != null && childs.Count > 0)
                        {
                            viewModel.Children=new List<ChildNode>();
                            childs.ForEach(t =>
                            {
                                var managerPage = managerPages.Where(q => q.Id == t.PageId)?.FirstOrDefault();
                                ChildNode node=new ChildNode();
                                node.Meta=new MetaNode();
                                node.Meta.Roles=new List<string>();

                                node.Path = managerPage.PageUrlName;
                                node.Component = managerPage.PageUrl;
                                node.Name = managerPage.PageDescribe;
                                if (t.RoleIds.Contains(","))
                                {
                                    var temp = t.RoleIds.Split(',');
                                    foreach (var s in temp)
                                    {
                                        var role = _roleManager.Roles.Where(g => g.Id == s).ToList().FirstOrDefault();
                                       
                                        node.Meta.Roles.Add(role.Name);
                                    }
                                }
                                else
                                {
                                    var role = _roleManager.Roles.Where(g => g.Id == t.RoleIds).ToList().FirstOrDefault();
                                    node.Meta.Roles.Add(role.Name);
                                }

                                node.Meta.Icon = string.IsNullOrEmpty(managerPage.Icon)
                                    ? string.Empty
                                    : managerPage.Icon;
                                node.Meta.NoCache = managerPage.NoCache;
                                node.Meta.Title = managerPage.Title;
                                viewModel.Children.Add(node);
                            });
                        }
                        pagesList.Add(viewModel);
                    }
                });
                var return1 = new RichBizReturn<List<PagePermissionViewModel>>
                {
                    message = "加载成功",
                    data = pagesList,
                    resultCode=20000,
                    success = true
                };
                return return1;
            }
            else
            {
                var permissions = await _richUserRepository.GetAllPermissionByRoleIdAsync(roleId);

                if (permissions == null || permissions.Count() <= 0)
                {
                    var return2 = new RichBizReturn<List<PagePermissionViewModel>>
                    {
                        message = "加载失败",
                        data = null,
                        resultCode = 50000,
                        success = false,
                        exceptionMsg = "角色没有对应的配置"
                    };
                    return return2;
                }

                var managerPages = await _richUserRepository.GetAllManagerPageAsync();
               
                //找到最小的levelid为顶级菜单
                var levelids = permissions.Select(p => p.LevelId).Distinct().ToList();
                var topLevel = levelids.Min();
                List<PagePermissionViewModel> pagesList = new List<PagePermissionViewModel>();

                permissions.ToList().ForEach(p =>
                {
                    if (p.LevelId == topLevel)
                    {
                        PagePermissionViewModel viewModel = new PagePermissionViewModel();
                        var item = managerPages.Where(q => q.Id == p.PageId)?.FirstOrDefault();
                        viewModel.Name = item.PageDescribe;
                        viewModel.Component = item.PageUrl;
                        viewModel.Path = item.PageUrlName;
                        viewModel.AlwaysShow = item.AlwaysShow;
                        viewModel.Id = item.Id;
                        viewModel.Redirect = string.IsNullOrEmpty(item.Redirect) ? string.Empty : item.Redirect;
                        viewModel.Meta = new MetaNode();
                        viewModel.Meta.Roles = new List<string>();
                        if (p.RoleIds.Contains(","))
                        {
                            var temp = p.RoleIds.Split(',');
                            foreach (var s in temp)
                            {
                                var role = _roleManager.Roles.Where(g => g.Id == s).ToList().FirstOrDefault();
                                viewModel.Meta.Roles.Add(role.Name);
                            }
                        }
                        else
                        {
                            var role = _roleManager.Roles.Where(g => g.Id == p.RoleIds).ToList().FirstOrDefault();
                            viewModel.Meta.Roles.Add(role.Name);
                        }

                        viewModel.Meta.Icon = string.IsNullOrEmpty(item.Icon)
                            ? string.Empty
                            : item.Icon;
                        viewModel.Meta.NoCache = item.NoCache;
                        viewModel.Meta.Title = item.Title;
                        var childs = permissions.Where(o => o.ParentPageId == p.PageId).ToList();
                        //存在子菜单
                        if (childs != null && childs.Count > 0)
                        {
                            viewModel.Children = new List<ChildNode>();
                            childs.ForEach(t =>
                            {
                                var managerPage = managerPages.Where(q => q.Id == t.PageId)?.FirstOrDefault();
                                ChildNode node = new ChildNode();
                                node.Meta = new MetaNode();
                                node.Meta.Roles = new List<string>();

                                node.Path = managerPage.PageUrlName;
                                node.Component = managerPage.PageUrl;
                                node.Name = managerPage.PageDescribe;
                                if (t.RoleIds.Contains(","))
                                {
                                    var temp = t.RoleIds.Split(',');
                                    foreach (var s in temp)
                                    {
                                        var role = _roleManager.Roles.Where(g => g.Id == s).ToList().FirstOrDefault();
                                        node.Meta.Roles.Add(role.Name);
                                    }
                                }
                                else
                                {
                                    var role = _roleManager.Roles.Where(g => g.Id == t.RoleIds).ToList().FirstOrDefault();
                                    node.Meta.Roles.Add(role.Name);
                                }

                                node.Meta.Icon = string.IsNullOrEmpty(managerPage.Icon)
                                    ? string.Empty
                                    : managerPage.Icon;
                                node.Meta.NoCache = managerPage.NoCache;
                                node.Meta.Title = managerPage.Title;
                                viewModel.Children.Add(node);
                            });
                        }
                        pagesList.Add(viewModel);
                    }
                });
                var return1 = new RichBizReturn<List<PagePermissionViewModel>>
                {
                    message = "加载成功",
                    data = pagesList,
                    resultCode = 20000,
                    success = true
                };
                return return1;
            }
        }

    }
}
