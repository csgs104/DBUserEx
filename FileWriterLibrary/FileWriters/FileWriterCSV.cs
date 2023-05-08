using System;

using FileWriterLibrary;


namespace FileWriterLibrary.FileWriters;

public class FileWriterCSV : BaseFileWriter
{
    public const string csv = ".csv";


    public FileWriterCSV(string basepath, string name, string content)
    : base(basepath, name, content) 
    { }

    public FileWriterCSV(string name, string content) 
	: base(name, content) 
    { }


    public override string FilePath()
    {
       return $"{base.FilePath()}{csv}";
    }
}