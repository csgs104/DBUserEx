using System;
using System.IO;

// 2
namespace FileWriterLibrary;

public class BaseFileWriter : FileWriter
{
	private readonly string _name = null!;
    private readonly string _content = null!;

    public string Name { get => _name; }
    public string Content { get => _content; }


    public BaseFileWriter(string basepath, string name, string content) 
	: base(basepath)
    {
        _name = name;
        _content = content;
    }

    public BaseFileWriter(string name, string content) 
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