using System;
using Microsoft.Data.SqlClient;

namespace DBUserLibrary.DataBase;

public class DataBase
{
    private readonly string _connection;

    public DataBase(string connection)
    {
        _connection = connection;
    }

    public const string CreateDBUsers =  @"
    IF NOT EXISTS (SELECT * FROM [sys].[databases] WHERE [name]='Users') 
    BEGIN CREATE DATABASE [Users] END";

    public const string UseDBUserss = @"
    IF EXISTS (SELECT * FROM [sys].[databases] WHERE [name]='Users') 
    BEGIN USE [Users] END";

    public const string CreateTableUser = @"
    IF NOT EXISTS (SELECT * FROM [sys].[sysobjects] WHERE [name]='Users' AND [xtype]='U') 
    BEGIN 
    CREATE TABLE [Users].[dbo].[User] (
    [Id] INT NOT NULL UNIQUE IDENTITY(1,1), 
    [User] NVARCHAR(200) NOT NULL UNIQUE, 
    [Password] NVARCHAR(200) NOT NULL
    [Date] DATE NOT NULL
    CONSTRAINT PK_Person PRIMARY KEY (Id)
    ) END";

    public const string InsertTableUser = @"
    IF NOT EXISTS (SELECT * FROM [Users].[dbo].[User] WHERE [User] = 'pippopippo@mail.com') 
    BEGIN INSERT INTO [Users].[dbo].[User] VALUES ('pippopippo@mail.com','Password1$','2023-05-05') END
    IF NOT EXISTS (SELECT * FROM [Users].[dbo].[User] WHERE [User] = 'pippapippa@mail.com') 
    BEGIN INSERT INTO [Users].[dbo].[User] VALUES ('pippapippa@mail.com','Password2$','2023-05-05') END
    IF NOT EXISTS (SELECT * FROM [Users].[dbo].[User] WHERE [User] = 'pippipippi@mail.com') 
    BEGIN INSERT INTO [Users].[dbo].[User] VALUES ('pippipippi@mail.com','Password3$','2023-05-05') END";

    public bool Initialize()
    {
        try
        {
            using var cn = new SqlConnection(_connection);

            using var createDBUsers = new SqlCommand(CreateDBUsers, cn);
            using var createTableUser = new SqlCommand(CreateTableUser, cn);
            using var insertTableUser = new SqlCommand(InsertTableUser, cn);

            cn.Open();

            createDBUsers.ExecuteNonQuery();
            createTableUser.ExecuteNonQuery();
            insertTableUser.ExecuteNonQuery();

            return true;
        }
        catch (SqlException ex)
        {
            return false;
        }
    }
}