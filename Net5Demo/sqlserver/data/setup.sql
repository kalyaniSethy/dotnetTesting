CREATE DATABASE Net5AppSecurity
GO
USE Net5AppSecurity
GO
CREATE TABLE Net5AppSecurity.dbo.tblUser (
    Id INT PRIMARY KEY IDENTITY (1,1) NOT NULL,
    Name varchar(100) NOT NULL
)
GO

CREATE DATABASE Net5AppData
GO
USE Net5AppData
GO
CREATE TABLE Net5AppData.dbo.tblTrip (
    Id INT PRIMARY KEY IDENTITY (1,1) NOT NULL,
    FromLocation varchar(100) NOT NULL,
    ToLocation varchar(100) NOT NULL
)
GO

