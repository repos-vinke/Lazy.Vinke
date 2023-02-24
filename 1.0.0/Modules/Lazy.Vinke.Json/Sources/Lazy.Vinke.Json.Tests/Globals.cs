global using Microsoft.VisualStudio.TestTools.UnitTesting;

public static class TestStringFormat
{
    public static string DateTimeISO_GMTXX = "yyyy-MM-ddTHH:mm:ss:fffzz";
}

public static class TestStringRegex
{
    public static string DateTimeISO_GMTXX = "^([0-9]{4})-([0-9]{2})-([0-9]{2})T([0-9]{2}):([0-9]{2}):([0-9]{2}):([0-9]{3})[+-]([0][0-9]|[1][0-2])$";
}