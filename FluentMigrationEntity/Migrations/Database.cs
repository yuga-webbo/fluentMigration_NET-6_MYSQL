using Dapper;
using MySql.Data.MySqlClient;
using System.Linq;

namespace FluentMigrationEntity.Migrations
{
    public static class Database
    {
       
        public static void Migrate(string connectionString, string name,string serverName,string username,string password )
        {
            /*var  username=IConfiguration.*/
            var oldconnectionstring = "server=" + serverName+"; " + "user=" + username+"; " + "password=" + password + ";";
            var connection = new MySqlConnection(oldconnectionstring);
            var parameters = new DynamicParameters();
            parameters.Add("name", name);
           /* var result = connection.Query("SHOW DATABASES LIKE @name", parameters).Any();*/
            if (!connection.Query("SHOW DATABASES LIKE @name", parameters).Any())
            {
                connection.Execute($"CREATE DATABASE {name}");
            }
        }
    }
}
