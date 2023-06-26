namespace FileWriterLibrary;

// 1
public abstract class FileWriter : IFileWriter
{
	private readonly string _basepath = null!;

	public string BasePath { get => _basepath; }

	public FileWriter(string basepath)
	{
		_basepath = basepath;
    }

	public FileWriter()
	: this(Directory.GetCurrentDirectory()) 
    { }


    public abstract bool FileWrite();
}