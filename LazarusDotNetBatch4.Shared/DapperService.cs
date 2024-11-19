using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace LazarusDotNetBatch4.Shared
{
    public class DapperService
    {
        private readonly string _connectionString;

        public DapperService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<T> Query<T>(string query, object? param = null)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            var lst = db.Query<T>(query, param).ToList();
            return lst;
        }

        public Q QueryFirstOrDefault<Q>(string query, object? param = null)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            var item = db.Query<Q>(query, param).FirstOrDefault();
            return item;
        }

        public int Execute(string query, object? param = null)
        {
            using IDbConnection db = new SqlConnection( _connectionString);
            var result = db.Execute(query, param);
            return result;
        }

    }
}
