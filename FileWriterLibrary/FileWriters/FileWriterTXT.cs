namespace FileWriterLibrary.FileWriters;

public class FileWriterTXT : BaseFileWriter
{
    public const string txt = ".txt";

    public FileWriterTXT(string basepath, string name, string content)
    : base(basepath, name, content) 
    { }

    public FileWriterTXT(string name, string content)
    : base(name, content) 
    { }

    public override string FilePath()
    {
        return $"{base.FilePath()}{txt}";
    }
}