using System;
namespace DBUserApp.Menu.Modules;

public static class Options
{
    public const string EXIT = "X";
    public const string INSERT = "I";
    public const string UPDATE = "U";
    public const string DELETE = "D";
    public const string SELECT = "S";

    public static Dictionary<string, string> Operations()
    {
        var operations = new Dictionary<string, string>();
        operations.Add(EXIT, "Exit");
        operations.Add(INSERT, "Insert");
        operations.Add(UPDATE, "Update");
        operations.Add(DELETE, "Delete");
        operations.Add(SELECT, "Select");
        return operations;
    }
}