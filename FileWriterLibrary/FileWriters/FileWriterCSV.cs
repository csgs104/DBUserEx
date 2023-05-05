using System;

using FileWriterLibrary;

namespace FileWriterLibrary.FileWriters;

public class FileWriterCSV : FileWriter
{
    public const string csv = ".csv";

    public FileWriterCSV(string name) : base(name)
    { }

    public override string FilePath() => Path.Combine(base.FilePath(), csv);
}