using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;

namespace Rich.Common.Base.RichDapper
{
    public abstract class DapperExtension : IDapperExtension
    {
        private string ConnectStr { get; set; }
        protected DapperExtension()
        {

        }
        protected DapperExtension(string connectstr)
        {
            ConnectStr = connectstr;
        }

        public async Task<IEnumerable<T>> GetAllTask<T>() where T : class
        {
            using (IDbConnection conn = new SqlConnection(ConnectStr))
            {
                var result = await conn.GetAllAsync<T>();
                return result;
            }
        }
        public async Task<IEnumerable<T>> QueryList<T>(string sqlText, DynamicParameters parameters)
        {

            using (SqlConnection conn = new SqlConnection(ConnectStr))
            {
                conn.Open();
                try
                {
                    //with no lock
                    return await conn.QueryAsync<T>(sqlText, parameters);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetPageList<T>(string sql, string extSql, DynamicParameters parameters, int page = 1, int pageSize = 10)
        {
            using (SqlConnection conn = new SqlConnection(ConnectStr))
            {
                conn.Open();
                try
                {
                    StringBuilder builder = new StringBuilder();
                    builder.Append(extSql);
                    builder.Append($"select Top {pageSize} * from  ( ");
                    builder.Append(sql);
                    builder.Append($" ) temp where temp.numberid>{pageSize}*({page}-1)");
                    var sqlText = builder.ToString();

                    var result = await conn.QueryAsync<T>(sqlText, parameters);
                    return result;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetPageList<T>(string sql, DynamicParameters parameters, int page = 1, int pageSize = 10)
        {
            using (SqlConnection conn = new SqlConnection(ConnectStr))
            {
                conn.Open();
                try
                {
                    StringBuilder builder = new StringBuilder();
                    builder.Append($"select Top {pageSize} * from  ( ");
                    builder.Append(sql);
                    builder.Append($" ) temp where temp.numberid>{pageSize}*({page}-1)");
                    var sqlText = builder.ToString();

                    var result = await conn.QueryAsync<T>(sqlText, parameters);
                    return result;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<int> GetTotalCount(string sql, DynamicParameters parameters)
        {
            using (SqlConnection conn = new SqlConnection(ConnectStr))
            {
                conn.Open();
                try
                {
                    var result = await conn.QuerySingleOrDefaultAsync<int>(sql, parameters);
                    return result;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

        }
        public async Task<T> Single<T>(string sql, DynamicParameters para)
        {
            using (IDbConnection conn = new SqlConnection(ConnectStr))
            {
                var result = await conn.QueryAsync<T>(sql, para);
                if (result == null || result.Count() == 0)
                {
                    return default(T);
                }
                return result.FirstOrDefault();
            }
        }
        public async Task<DataTable> RunProcedure(DynamicParameters parameters, string Pro)
        {

            using (SqlConnection conn = new SqlConnection(ConnectStr))
            {
                conn.Open();
                try
                {
                    DataTable table = new DataTable();
                    var result =
                        await conn.ExecuteReaderAsync(Pro, parameters, commandType: CommandType.StoredProcedure);
                    table.Load(result);
                    return table;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }
    }
}
