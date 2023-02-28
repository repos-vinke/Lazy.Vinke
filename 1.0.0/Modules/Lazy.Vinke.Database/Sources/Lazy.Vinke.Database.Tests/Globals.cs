global using Microsoft.VisualStudio.TestTools.UnitTesting;

public static class Globals
{
    public static class MySql
    {
        public static string ConnectionString = "Server=localhost;Database=lazy;Uid=lazy;Pwd=lazy;";
    }

    public static class Postgre
    {
        public static string ConnectionString = "Server=localhost;Port=5432;Database=lazy;User Id=lazy;Password=lazy;";
    }

    public static class SqlServer
    {
        public static string ConnectionString = "Server=.\\SQLExpress;Database=lazy;User Id=lazy;Password=lazy;Encrypt=no";
    }
}