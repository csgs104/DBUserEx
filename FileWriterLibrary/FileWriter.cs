using System;
using System.IO;

namespace FileWriterLibrary;

public class FileWriter : BaseFileWriter
{
	private readonly string _name = null!;

    public string Name { get => _name; }

    public FileWriter(string name) : base()
	{
		_name = name;
    }

    public virtual string FilePath() => Path.Combine(BasePath, Name);

	public override bool FileWrite(string content) 
    {
		try
		{
			File.WriteAllText(FilePath(), content);
			return true;
		}
		catch (Exception ex)
		{
			return false;
		}
	}
}