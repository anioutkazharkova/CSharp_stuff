USE master;

-- Drop database
IF DB_ID('ChatBase') IS NOT NULL DROP DATABASE ChatBase;

-- If database could not be created due to open connections, abort
IF @@ERROR = 3702 
   RAISERROR('Database cannot be dropped because there are still open connections.', 127, 127) WITH NOWAIT, LOG;
   
   -- Create database
CREATE DATABASE ChatBase;
GO

USE ChatBase;
GO

--Create schema
CREATE SCHEMA chb AUTHORIZATION dbo;
GO

--Create tables

--User table
CREATE TABLE chb.Users
(
userid INT NOT NULL IDENTITY(1,1),
login NVARCHAR(50) NOT NULL,
password NVARCHAR(20) NOT NULL,
CONSTRAINT PK_User PRIMARY KEY(userid) 
);

-- Log table
CREATE TABLE chb.Logs
(
logid INT NOT NULL IDENTITY(1,1),
mesdate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
message NVARCHAR(4000) NOT NULL,
userid INT NOT NULL,
CONSTRAINT PK_Log PRIMARY KEY(logid),
CONSTRAINT FK_Log_User FOREIGN KEY(userid) REFERENCES chb.Users(userid) ON UPDATE CASCADE
);
GO

--Prepare default user
INSERT INTO chb.Users VALUES('test','1234567');