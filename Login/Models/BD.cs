using System.Data.SqlClient;
using Dapper;
namespace Login.Models;

public class BD
{
    private static string ConnectionString = @"Server=PC-GAMER\SQLEXPRESS;DataBase=Green Gains;Trusted_Connection=True;";

    public static void AgregarUsuario(string username, string password)
    {
        using (SqlConnection db = new SqlConnection(ConnectionString))
        {
            string sql = "INSERT INTO Usuario (username, password) VALUES (@pUsername, CONVERT(varchar(32), HASHBYTES('md5', @pPassword), 2))";
            db.Execute(sql, new { pUsername = username, pPassword = password });
        }
    }

    public static Usuario? Mostrar(string username, string password)
    {
        Usuario? usuario;
        using (SqlConnection db = new SqlConnection(ConnectionString))
        {
            string sql = "SELECT * FROM Usuario WHERE username = @pUsername AND password = CONVERT(varchar(32), HASHBYTES('md5', @pPassword), 2)";
            usuario = db.QueryFirstOrDefault<Usuario>(sql, new { pUsername = username, pPassword = password });
        }
        return usuario;
    }

    public static void CambiarPassword(string username, string password)
    {
        using (SqlConnection db = new SqlConnection(ConnectionString))
        {
            string sql = "UPDATE Usuario SET password = @pPassword WHERE username = @pUsername";
            db.Execute(sql, new { pUsername = username, pPassword = password });
        }
    }
}