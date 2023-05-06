using System;

using FileWriterLibrary;

namespace FileWriterLibrary.FileWriters;

public class FileWriterTXT : FileWriter
{
    public const string txt = ".txt";

    public FileWriterTXT(string name, string content) : base(name, content)
    { }


    public override string FilePath() 
	    => Path.Combine(base.FilePath(), txt);
}