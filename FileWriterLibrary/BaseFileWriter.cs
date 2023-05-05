using System;

namespace FileWriterLibrary;

public abstract class BaseFileWriter : IFileWriter
{
	private readonly string _basepath = null!;

	public string BasePath { get => _basepath; }

	public BaseFileWriter()
	{
		_basepath = Directory.GetCurrentDirectory();
	}

	public abstract bool FileWrite(string content);
}