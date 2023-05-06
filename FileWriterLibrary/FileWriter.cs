using System;
using System.IO;

namespace FileWriterLibrary;

public class FileWriter : BaseFileWriter
{
	private readonly string _name = null!;
    private readonly string _content = null!;

    public string Name { get => _name; }
    public string Content { get => _content; }

    public FileWriter(string basepath, string name, string content) 
	: base(basepath)
    {
        _name = name;
        _content = content;
    }

    public FileWriter(string name, string content) 
	: base()
	{
		_name = name;
        _content = content;
    }


	public virtual string FilePath()
	{
		return Path.Combine(BasePath, Name);
	}

	public override bool FileWrite() 
    {
		try
		{
			File.WriteAllText(FilePath(), Content);
			return true;
		}
		catch (Exception ex)
		{
			// Console.WriteLine(ex.Message);
			return false;
		}
	}
}