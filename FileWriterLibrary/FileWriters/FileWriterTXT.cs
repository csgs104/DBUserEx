using System;

using FileWriterLibrary;

namespace FileWriterLibrary.FileWriters;

public class FileWriterTXT : FileWriter
{
    public const string txt = ".txt";

    public FileWriterTXT(string basepath, string name, string content)
    : base(basepath, name, content) { }

    public FileWriterTXT(string name, string content)
    : base(name, content) { }

    public override string FilePath()
    {
        return $"{base.FilePath()}{txt}";
    }
}