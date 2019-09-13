using System;
using System.Data;
using CloudNativeKit.Infrastructure.DataPersistence.EfCore.Db;
using Microsoft.Data.SqlClient;

namespace CloudNativeKit.Infrastructure.DataPersistence.Dapper.Internal
{
    internal class SqlConnectionFactory : ISqlConnectionFactory, IDisposable
    {
        private readonly IDbConnStringFactory _connStringFactory;
        private IDbConnection _connection;

        public SqlConnectionFactory(IDbConnStringFactory connStringFactory)
        {
            _connStringFactory = connStringFactory;
        }

        public IDbConnection GetOpenConnection()
        {
            if (_connection == null || _connection.State != ConnectionState.Open)
            {
                _connection = new SqlConnection(_connStringFactory.Create());
                _connection.Open();
            }

            return _connection;
        }

        public void Dispose()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
            {
                _connection.Dispose();
            }
        }
    }
}