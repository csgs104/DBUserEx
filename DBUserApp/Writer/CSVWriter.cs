using System;

using DBUserLibrary.Entities.Classes;
using FileWriterLibrary.FileWriters;

namespace DBUserApp.Writer;

public static class Writer
{
    public static bool UserToCSV(User user) 
    {
        var userCSV = new FileWriterCSV(user.Id.ToString());
	    return userCSV.FileWrite(user.ToCommaSeparatedString());
    }
}