using System;

using FileWriterLibrary;

namespace FileWriterLibrary.FileWriters;

public class FileWriterTXT : FileWriter
{
    public const string txt = ".txt";

    public FileWriterTXT(string name) : base(name)
    { }

    public override string FilePath() => Path.Combine(base.FilePath(), txt);
}