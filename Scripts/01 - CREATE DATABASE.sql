USE master
GO
IF NOT EXISTS  ( SELECT name FROM master.dbo.sysdatabases  WHERE name = N'DB_FEIRA_LIVRE'  )BEGIN   
    CREATE DATABASE [DB_FEIRA_LIVRE]    
END

