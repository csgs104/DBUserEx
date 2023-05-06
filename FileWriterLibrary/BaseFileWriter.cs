using System;
using System.IO;

namespace FileWriterLibrary;

public abstract class BaseFileWriter : IFileWriter
{
	private readonly string _basepath = null!;

	public string BasePath { get => _basepath; }

	public BaseFileWriter(string basepath)
	{
		_basepath = basepath;
    }

	public BaseFileWriter()
	: this(Directory.GetCurrentDirectory()) { }

    public abstract bool FileWrite();
}