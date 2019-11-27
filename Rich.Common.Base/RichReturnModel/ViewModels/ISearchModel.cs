using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Rich.Common.Base.RichReturnModel
{ 
    public interface ISearchParams
    {
        [DefaultValue(0)]
        int id { set; get; }
        [DefaultValue(null)]
        DateTime[] createDate { set; get; }

    }
    public class SearchNoParams : ISearchParams
    {

        //根据业务需求新增属性
        public int id { get; set; }
        public DateTime[] createDate { get; set; }
    }
    /// <summary>
    /// 通用搜索参数对象
    /// </summary>
    public interface ISearchModel<TSearchParams> where TSearchParams : ISearchParams
    {
        /// <summary>
        /// 自定义搜索参数
        /// </summary>
        TSearchParams searchParams { set; get; }
        /// <summary>
        /// 额外字段
        /// </summary>
        List<string> extFields { set; get; }
        /// <summary>
        /// 分页信息
        /// </summary>
        IRequestPageInfo pageInfo { set; get; }

        bool Validate();
    }
}