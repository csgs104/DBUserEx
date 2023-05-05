using System;
using Microsoft.Data.SqlClient;

namespace DBUserLibrary.DB;

public class Initialization
{
    private readonly string _connection;

    public Initialization(string connection)
    {
        _connection = connection;
    }

    public static string CreateDBPersons() => @"
    IF NOT EXISTS (SELECT * FROM [sys].[databases] WHERE [name]='Users') 
    BEGIN CREATE DATABASE [Users] END";

    public static string UseDBPersons() => @"
    IF EXISTS (SELECT * FROM [sys].[databases] WHERE [name]='Users') 
    BEGIN USE [Users] END";

    public static string CreateTablePerson() => @"
    IF NOT EXISTS (SELECT * FROM [sys].[sysobjects] WHERE [name]='Users' AND [xtype]='U') 
    BEGIN 
    CREATE TABLE [Users].[dbo].[User] (
    [Id] INT NOT NULL UNIQUE, 
    [User] NVARCHAR(200) NOT NULL UNIQUE, 
    [Password] NVARCHAR(200) NOT NULL
    [Date] DATE NOT NULL
    CONSTRAINT PK_Person PRIMARY KEY (Id)
    ) END";

    public static string InsertTablePerson() => @"
    IF NOT EXISTS (SELECT * FROM [Users].[dbo].[User] WHERE [Id] = 1) 
    BEGIN INSERT INTO [Users].[dbo].[User] VALUES (1,'pippopippo@mail.com','Password1$','2023-05-05') END
    IF NOT EXISTS (SELECT * FROM [Users].[dbo].[User] WHERE [Id] = 2) 
    BEGIN INSERT INTO [Users].[dbo].[User] VALUES (2,'pippapippa@mail.com','Password2$','2023-05-05') END
    IF NOT EXISTS (SELECT * FROM [Users].[dbo].[User] WHERE [Id] = 3) 
    BEGIN INSERT INTO [Users].[dbo].[User] VALUES (3,'pippipippi@mail.com','Password3$','2023-05-05') END";

    public bool Run()
    {
        try
        {
            using var cn = new SqlConnection(_connection);

            using var createDBPersons = new SqlCommand(CreateDBPersons(), cn);
            using var createTablePerson = new SqlCommand(CreateTablePerson(), cn);
            using var insertTablePerson = new SqlCommand(InsertTablePerson(), cn);

            cn.Open();

            createDBPersons.ExecuteNonQuery();
            createTablePerson.ExecuteNonQuery();
            insertTablePerson.ExecuteNonQuery();

            return true;
        }
        catch (SqlException ex)
        {
            return false;
        }
    }
}