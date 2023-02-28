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
}