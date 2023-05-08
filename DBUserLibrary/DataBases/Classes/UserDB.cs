using System;

using Microsoft.Data.SqlClient;

using DBUserLibrary.DataBases.Abstract;


namespace DBUserLibrary.DataBases.Classes;

public class UserDB : BaseDB
{
    public UserDB(string connection)
	: base(connection)
    { }

    public override string CreateDataBase() =>  @"
    IF NOT EXISTS (SELECT * FROM [sys].[databases] WHERE [name]='Users') 
    BEGIN CREATE DATABASE [Users] END";

    public override string CreateTables() => @"
    IF NOT EXISTS (SELECT * FROM [Users].[sys].[sysobjects] WHERE [name]='User' AND [xtype]='U')
    BEGIN 
    CREATE TABLE [Users].[dbo].[User] (
    [Id] INT NOT NULL UNIQUE IDENTITY(1,1), 
    [Email] NVARCHAR(200) NOT NULL UNIQUE, 
    [Password] NVARCHAR(200) NOT NULL,
    [Date] DATE NOT NULL,
    CONSTRAINT PK_Person PRIMARY KEY (Id)
    ) END";

    public override string InsertTables() => @"
    IF NOT EXISTS (SELECT * FROM [Users].[dbo].[User] WHERE [Email] = 'pippopippo@mail.com') 
    BEGIN INSERT INTO [Users].[dbo].[User] VALUES ('pippopippo@mail.com','Password1$','2023-05-05') END
    IF NOT EXISTS (SELECT * FROM [Users].[dbo].[User] WHERE [Email] = 'pippapippa@mail.com') 
    BEGIN INSERT INTO [Users].[dbo].[User] VALUES ('pippapippa@mail.com','Password1$','2023-05-05') END
    IF NOT EXISTS (SELECT * FROM [Users].[dbo].[User] WHERE [Email] = 'pippipippi@mail.com') 
    BEGIN INSERT INTO [Users].[dbo].[User] VALUES ('pippipippi@mail.com','Password1$','2023-05-05') END";
}