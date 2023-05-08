using System;

namespace DBUserLibrary.Repositories.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string message) 
	: base(message) 
    { }
}