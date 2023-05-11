using System;

using FileWriterLibrary.FileWriters;

using DBUserLibrary.Entities.Classes;


namespace DBUserApp.Writers;

public class FileWriterUserCSV : FileWriterCSV
{
    public const string header = $"matricola;username;password;data";


    public FileWriterUserCSV(User user)
    : base(DestinationPath("UserToCSV"),
           $"{user.Id.ToString()}-{user.Date.ToString("yyyy-MM-dd")}",
           $"{header}{Environment.NewLine}{user.ToCommaSeparatedString()}") 
    { }


    private static string DestinationPath(string directory)
    {
        var path = Directory.GetCurrentDirectory();
        var root = Path.GetPathRoot(path) ?? string.Empty;
        var b = Path.Combine(path.Split(Path.DirectorySeparatorChar).TakeWhile(s => !s.Equals("bin")).ToArray());
        return Path.Combine(root, b, directory);
    }
}