using System;

using DBUserLibrary.Entities.Classes;
using FileWriterLibrary.FileWriters;

namespace DBUserApp.Writers;

public class FileWriterUserCSV : FileWriterCSV
{
    public FileWriterUserCSV(User user) 
	: base($"{user.Id.ToString()}-{user.Date.ToString("yyyy-MM-dd")}", user.ToCommaSeparatedString()) 
    { }
}