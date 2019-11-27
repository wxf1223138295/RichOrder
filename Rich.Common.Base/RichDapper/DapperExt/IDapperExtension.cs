using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Rich.Common.Base.RichDapper
{
    public interface IDapperExtension
    {
        Task<IEnumerable<T>> GetPageList<T>(string sql, string extSql, DynamicParameters parameters, int page = 1,
            int pageSize = 10);

        Task<IEnumerable<T>> GetPageList<T>(string sql, DynamicParameters parameters, int page = 1, int pageSize = 10);
        Task<IEnumerable<T>> QueryList<T>(string sqlText, DynamicParameters parameters);
        Task<int> GetTotalCount(string sql, DynamicParameters parameters);
        Task<IEnumerable<T>> GetAllTask<T>() where T : class;

        Task<DataTable> RunProcedure(DynamicParameters parameters, string Pro);
        Task<T> Single<T>(string sql, DynamicParameters para);
    }
}
