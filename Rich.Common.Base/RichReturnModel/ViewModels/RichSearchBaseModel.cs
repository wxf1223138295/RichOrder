using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Rich.Common.Base.RichReturnModel
{
    [JsonObject(MemberSerialization.OptOut)]
    public abstract class RichSearchBaseModel<TSearchParams> : ISearchModel<TSearchParams> where TSearchParams : ISearchParams
    {
        protected RichSearchBaseModel()
        {
            pageInfo = new RequestPageInfo();
        }

        public TSearchParams searchParams { get; set; }
        public List<string> extFields { get; set; }
        public IRequestPageInfo pageInfo { get; set; }
        [JsonIgnore]
        protected string validErrors { set; get; }

        public virtual bool Validate()
        {
            this.validErrors = string.Empty;
            if (this.searchParams == null)
            {
                this.validErrors += "请传入搜索参数";
                return false;
            }

            if (this.searchParams != null && this.searchParams.createDate != null &&
                this.searchParams.createDate.Length > 2)
            {
                this.validErrors += "搜索参数【创建日期】传入不正确";
                return false;
            }

            if (pageInfo == null)
            {
                pageInfo = new RequestPageInfo();
            }

            return string.IsNullOrEmpty(this.validErrors);
        }

    }
    [JsonObject(MemberSerialization.OptOut)]
    public abstract class RichSearchNoParamModel : ISearchModel<SearchNoParams>
    {
        protected RichSearchNoParamModel()
        {
            pageInfo = new RequestPageInfo();
        }

        public SearchNoParams searchParams { get; set; }
        public List<string> extFields { get; set; }
        public IRequestPageInfo pageInfo { get; set; }

        public bool Validate()
        {
            return true;
        }
        
    }
}
