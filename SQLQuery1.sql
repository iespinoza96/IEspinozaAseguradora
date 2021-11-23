CREATE DATABASE IEspinozaAseguradora 
GO 

USE IEspinozaAseguradora
GO 

---
--Usuario
CREATE TABLE Usuario 

	(IdUsuario INT IDENTITY(1,1) PRIMARY KEY NOT NULL
	,UserName VARCHAR (50) NOT NULL UNIQUE
	,Nombre VARCHAR(50) NOT NULL
	,ApellidoPaterno VARCHAR(50) NOT NULL
	,ApellidoMaterno VARCHAR(50) NULL
	,Email VARCHAR(254) NOT NULL UNIQUE
	,Sexo CHAR(2)
	,Telefono VARCHAR(20) NOT NULL
	,Celular VARCHAR(20) NULL
	,FechaNacimiento DATE NULL
	,CURP VARCHAR(50) NULL
	,Imagen VARBINARY(MAX) Null)

GO

INSERT INTO Usuario VALUES ('isaacjeo96','Isaac','Espinoza','Ocampo','isaacjeo96@gmail.com','M','5556133926','5527427050','10/05/1996','EIOI960510HDFSCS06')
go

CREATE PROCEDURE UsuarioGetAll
AS
	SELECT [IdUsuario]
      ,[UserName]
      ,[Nombre]
      ,[ApellidoPaterno]
      ,[ApellidoMaterno]
      ,[Email]
      ,[Sexo]
      ,[Telefono]
      ,[Celular]
      ,[FechaNacimiento]
      ,[CURP]
	  ,[Imagen]
  FROM [Usuario]
GO

CREATE PROCEDURE UsuarioAdd
@UserName VARCHAR (50) 
,@Nombre VARCHAR(50)
,@ApellidoPaterno VARCHAR(50)
,@ApellidoMaterno VARCHAR(50) 
,@Email VARCHAR(254) 
,@Sexo CHAR(2)
,@Telefono VARCHAR(20) 
,@Celular VARCHAR(20) 
,@FechaNacimiento VARCHAR(20) 
,@CURP VARCHAR(50) 
,@Imagen VARBINARY(MAX)
AS
	INSERT INTO [Usuario]
           ([UserName]
           ,[Nombre]
           ,[ApellidoPaterno]
           ,[ApellidoMaterno]
           ,[Email]
           ,[Sexo]
           ,[Telefono]
           ,[Celular]
           ,[FechaNacimiento]
           ,[CURP]
           ,[Imagen])
     VALUES
           (@UserName
           ,@Nombre
           ,@ApellidoPaterno
           ,@ApellidoMaterno
           ,@Email
           ,@Sexo
           ,@Telefono
           ,@Celular
           ,CONVERT (DATE,@FechaNacimiento,105)
           ,@CURP
           ,@Imagen)
GO

CREATE PROCEDURE UsuarioUpdate
@IdUsuario INT 
,@UserName VARCHAR (50)
,@Nombre VARCHAR(50)
,@ApellidoPaterno VARCHAR(50)
,@ApellidoMaterno VARCHAR(50)
,@Email VARCHAR(254)
,@Sexo CHAR(2)
,@Telefono VARCHAR(20)
,@Celular VARCHAR(20)
,@FechaNacimiento VARCHAR(50)
,@CURP VARCHAR(50) 
,@Imagen VARBINARY(MAX)
AS
	UPDATE [Usuario]
   SET [UserName] = @UserName
      ,[Nombre] = @Nombre
      ,[ApellidoPaterno] = @ApellidoPaterno
      ,[ApellidoMaterno] = @ApellidoMaterno
      ,[Email] = @Email
      ,[Sexo] = @Sexo
      ,[Telefono] = @Telefono
      ,[Celular] = @Celular
      ,[FechaNacimiento] = CONVERT (DATE,@FechaNacimiento,105)
      ,[CURP] = @CURP
      ,[Imagen] = @Imagen
 WHERE IdUsuario = @IdUsuario
GO

CREATE PROCEDURE UsuarioDelete
@IdUsuario INT
AS
	DELETE FROM Usuario 
		WHERE IdUsuario = @IdUsuario
GO 

CREATE PROCEDURE UsuarioGetById
@IdUsuario INT
AS
	SELECT [IdUsuario]
      ,[UserName]
      ,[Nombre]
      ,[ApellidoPaterno]
      ,[ApellidoMaterno]
      ,[Email]
      ,[Sexo]
      ,[Telefono]
      ,[Celular]
      ,[FechaNacimiento]
      ,[CURP]
      ,[Imagen]
  FROM [Usuario]
  WHERE IdUsuario = @IdUsuario
GO

----------------------------------------------------------------
--Empresa

CREATE TABLE Empresa (IdEmpresa INT PRIMARY KEY IDENTITY(1,1)
					  ,Nombre VARCHAR(100)
					  ,Telefono VARCHAR(50)
					  ,Email VARCHAR(24)
					  ,DireccionWeb VARCHAR(100)
					  ,Logo VARBINARY(MAX))
GO

CREATE PROCEDURE EmpresaAdd
@Nombre VARCHAR(100)
,@Telefono VARCHAR(50)
,@Email VARCHAR(24)
,@DireccionWeb VARCHAR(100)
,@Logo VARBINARY(MAX)
AS 
	INSERT INTO Empresa 
		VALUES 
		 (@Nombre,@Telefono,@Email,@DireccionWeb,@Logo)
GO 

CREATE PROCEDURE EmpresaUpdate
@IdEmpresa INT
,@Nombre VARCHAR(100)
,@Telefono VARCHAR(50)
,@Email VARCHAR(24)
,@DireccionWeb VARCHAR(100)
,@Logo VARBINARY(MAX)
AS
	UPDATE [dbo].[Empresa]
	   SET [Nombre] = @Nombre
		  ,[Telefono] = @Telefono
		  ,[Email] = @Email
		  ,[DireccionWeb] = @DireccionWeb
		  ,[Logo] = @Logo
	 WHERE  IdEmpresa = @IdEmpresa
GO

CREATE PROCEDURE EmpresaDelete
@IdEmpresa INT
AS 
	DELETE FROM Empresa 
		WHERE IdEmpresa = @IdEmpresa
GO

CREATE PROCEDURE EmpresaGetAll
AS
	SELECT [IdEmpresa]
      ,[Nombre]
      ,[Telefono]
      ,[Email]
      ,[DireccionWeb]
      ,[Logo]
  FROM [dbo].[Empresa]
GO

CREATE PROCEDURE EmpresaGetById
@IdEmpresa INT 
AS
SELECT [IdEmpresa]
      ,[Nombre]
      ,[Telefono]
      ,[Email]
      ,[DireccionWeb]
      ,[Logo]
  FROM [dbo].[Empresa]

  WHERE IdEmpresa = @IdEmpresa
GO
---
--Empleado

CREATE TABLE Empleado (NumEmpleado VARCHAR(50)
					   ,RFC VARCHAR(20)
					   ,Nombre VARCHAR(50) NOT NULL
					   ,ApellidoPaterno VARCHAR(50) NOT NULL
					   ,ApellidoMaterno VARCHAR(50) NULL
					   ,Email VARCHAR(254) NOT NULL UNIQUE
					   ,Telefono VARCHAR(20) NOT NULL
					   ,FechaNacimiento DATE NULL
					   ,NSS VARCHAR(20)
					   ,FechaIngreso DATE
					   ,Foto VARBINARY(MAX)
					   ,IdEmpresa INT FOREIGN KEY REFERENCES Empresa(IdEmpresa))
GO

CREATE PROCEDURE EmpleadoAdd 
@NumEmpleado VARCHAR(50)
,@RFC VARCHAR(20)
,@Nombre VARCHAR(50) 
,@ApellidoPaterno VARCHAR(50) 
,@ApellidoMaterno VARCHAR(50)
,@Email VARCHAR(254)
,@Telefono VARCHAR(20)
,@FechaNacimiento VARCHAR(20)
,@NSS VARCHAR(20)
,@FechaIngreso VARCHAR(20)
,@Foto VARBINARY(MAX)
,@IdEmpresa INT 
AS
	INSERT INTO [Empleado]
           ([NumEmpleado]
           ,[RFC]
           ,[Nombre]
           ,[ApellidoPaterno]
           ,[ApellidoMaterno]
           ,[Email]
           ,[Telefono]
           ,[FechaNacimiento]
           ,[NSS]
           ,[FechaIngreso]
           ,[Foto]
           ,[IdEmpresa])
     VALUES
           (@NumEmpleado
           ,@RFC
           ,@Nombre
           ,@ApellidoPaterno
           ,@ApellidoMaterno
           ,@Email
           ,@Telefono
           ,CONVERT (DATE,@FechaNacimiento,105)
           ,@NSS
           ,CONVERT (DATE,@FechaIngreso, 105)
           ,@Foto
           ,@IdEmpresa)
GO

CREATE PROCEDURE EmpleadoUpdate
@NumEmpleado VARCHAR(50)
,@RFC VARCHAR(20)
,@Nombre VARCHAR(50) 
,@ApellidoPaterno VARCHAR(50) 
,@ApellidoMaterno VARCHAR(50)
,@Email VARCHAR(254)
,@Telefono VARCHAR(20)
,@FechaNacimiento VARCHAR(20)
,@NSS VARCHAR(20)
,@FechaIngreso VARCHAR(20)
,@Foto VARBINARY(MAX)
,@IdEmpresa INT 
AS
	UPDATE [dbo].[Empleado]
   SET [NumEmpleado] = @NumEmpleado
      ,[RFC] = @RFC
      ,[Nombre] = @Nombre
      ,[ApellidoPaterno] = @ApellidoPaterno
      ,[ApellidoMaterno] = @ApellidoMaterno
      ,[Email] = @Email
      ,[Telefono] = @Telefono
      ,[FechaNacimiento] = CONVERT (DATE,@FechaNacimiento,105)
      ,[NSS] = @NSS
      ,[FechaIngreso] = CONVERT (DATE,@FechaIngreso, 105)
      ,[Foto] = @Foto
      ,[IdEmpresa] = @IdEmpresa

	WHERE NumEmpleado = @NumEmpleado
GO

CREATE PROCEDURE EmpleadoGetAll
AS
SELECT [NumEmpleado]
      ,[RFC]
      ,[Nombre]
      ,[ApellidoPaterno]
      ,[ApellidoMaterno]
      ,[Email]
      ,[Telefono]
      ,[FechaNacimiento]
      ,[NSS]
      ,[FechaIngreso]
      ,[Foto]
      ,[IdEmpresa]
  FROM [Empleado]
GO

CREATE PROCEDURE EmpleadoGetById
@NumEmpleado VARCHAR(50)
AS
	SELECT [NumEmpleado]
      ,[RFC]
      ,[Nombre]
      ,[ApellidoPaterno]
      ,[ApellidoMaterno]
      ,[Email]
      ,[Telefono]
      ,[FechaNacimiento]
      ,[NSS]
      ,[FechaIngreso]
      ,[Foto]
      ,[IdEmpresa]
  FROM [Empleado]

  WHERE NumEmpleado = @NumEmpleado

  GO

  CREATE PROCEDURE EmpleadoDelete 
  @NumEmpleado VARCHAR(50)
  AS
	DELETE FROM Empleado 

	 WHERE NumEmpleado = @NumEmpleado

GO

----------------------------------------
--SubPoliza 

CREATE TABLE SubPoliza (IdSubPoliza TINYINT PRIMARY KEY IDENTITY(1,1), Nombre VARCHAR(100))
GO

CREATE PROCEDURE SubPolizaGetAll
AS
	SELECT [IdSubPoliza]
      ,[Nombre]
  FROM [dbo].[SubPoliza]
GO

CREATE PROCEDURE SubPolizaAdd
@Nombre VARCHAR(100)
AS
	INSERT INTO [dbo].[SubPoliza]
           ([Nombre])
     VALUES
           (@Nombre)
GO

CREATE PROCEDURE SubPolizaUpdate
@IdSubPoliza TINYINT,
@Nombre VARCHAR(100)
AS
	UPDATE [dbo].[SubPoliza]
   SET [Nombre] = @Nombre
 WHERE IdSubPoliza = @IdSubPoliza
GO

CREATE PROCEDURE SubPolizaGetById
@IdSubPoliza TINYINT
AS
	SELECT [IdSubPoliza]
      ,[Nombre]
  FROM [dbo].[SubPoliza]
  WHERE IdSubPoliza = @IdSubPoliza
GO

CREATE PROCEDURE SubPolizaDelete 
@IdSubPoliza TINYINT
AS
	DELETE FROM SubPoliza 
	  WHERE IdSubPoliza = @IdSubPoliza
GO
-------------------------------------
--Vigencia 

CREATE TABLE Vigencia (IdVigencia INT PRIMARY KEY IDENTITY(1,1)
						,FechaInicio DATE
						,FechaFin DATE
						,FechaCreacion DATETIME
						,FechaModificacion DATETIME
						,IdUsuario INT FOREIGN KEY REFERENCES Usuario(IdUsuario)
						,IdPoliza INT FOREIGN KEY REFERENCES Poliza(IdPoliza))
						GO
---------------------------------------
--Poliza
		
CREATE TABLE Poliza(IdPoliza INT IDENTITY(1,1) PRIMARY KEY
					,Nombre VARCHAR(100)
					,IdSubPoliza TINYINT FOREIGN KEY REFERENCES SubPoliza(IdSubPoliza)
					,NumeroPoliza VARCHAR(20)
					,FechaCreacion DATETIME
					,FechaModificacion DATETIME
					,IdUsuario INT FOREIGN KEY REFERENCES Usuario(IdUsuario))
GO
-----------------------------------------------------------
--SP Poliza y Vigencia 
CREATE PROCEDURE PolizaAdd
	@Nombre VARCHAR(100),
	@IdSubPoliza TINYINT,
	@NumeroPoliza VARCHAR(20),
	@FechaInicio VARCHAR(10),
	@FechaFin VARCHAR(10),
	@IdUsuario INT
AS
	INSERT INTO Poliza 
	(Nombre
	,IdSubPoliza
	,NumeroPoliza
	,FechaCreacion
	,FechaModificacion
	,IdUsuario)

     VALUES
	 (@Nombre
	 ,@IdSubPoliza
	 ,@NumeroPoliza
	 ,GETDATE ()
	 ,GETDATE ()
	 ,@IdUsuario)

	 INSERT INTO Vigencia 
	 (FechaInicio
	 ,FechaFin
	 ,FechaCreacion
	 ,FechaModificacion
	 ,IdUsuario
	 ,IdPoliza)

	 VALUES 
	 (CONVERT (DATE,@FechaInicio,105)
	 ,CONVERT(DATE, @FechaFin,105)
	 ,GETDATE ()
	 ,GETDATE ()
	 ,@IdUsuario
	 ,@@IDENTITY)
GO
--------------------

CREATE PROCEDURE PolizaDelete 
@IdPoliza INT
AS
DELETE FROM Vigencia
      WHERE IdPoliza = @IdPoliza
DELETE FROM Poliza
	  WHERE IdPoliza = @IdPoliza
GO
-------------------
CREATE PROCEDURE PolizaGetAll
AS
SELECT Poliza.IdPoliza
	  ,Poliza.Nombre AS PolizaNombre
	  ,Poliza.IdSubPoliza, NumeroPoliza
	  ,Poliza.FechaCreacion AS CreacionPoliza
	  ,Poliza.FechaModificacion AS ModificacionPoliza
	  ,Poliza.IdUsuario
	  , SubPoliza.Nombre AS SubPolizaNombre
	  ,Vigencia.IdVigencia
	  ,FechaInicio,FechaFin
	  ,Vigencia.FechaCreacion AS CreacionVigencia
	  ,Vigencia.FechaModificacion AS ModificacionVigencia
	  ,UserName,Usuario.Nombre AS UsuarioNombre
	  ,ApellidoPaterno
	  ,ApellidoMaterno
  FROM Poliza
INNER JOIN SubPoliza
	ON Poliza.IdSubPoliza = SubPoliza.IdSubPoliza  
INNER JOIN Vigencia
	ON Poliza.IdPoliza = Vigencia.IdPoliza
INNER JOIN Usuario
	ON Poliza.IdUsuario = Usuario.IdUsuario
GO
---------------------------------
CREATE PROCEDURE PolizaGetById
@IdPoliza INT 
AS
SELECT Poliza.IdPoliza,Poliza.Nombre AS PolizaNombre
      ,Poliza.IdSubPoliza,NumeroPoliza
	  ,Poliza.FechaCreacion AS CreacionPoliza
	  ,Poliza.FechaModificacion AS ModificacionPoliza
	  ,Poliza.IdUsuario
	  ,SubPoliza.IdSubPoliza
	  ,SubPoliza.Nombre AS SubPolizaNombre
	  ,Vigencia.IdVigencia,FechaInicio
	  ,FechaFin
	  ,Vigencia.FechaCreacion AS CreacionVigencia
	  ,Vigencia.FechaModificacion AS ModificacionVigencia
	  ,Vigencia.IdUsuario, Vigencia.IdPoliza
	  ,Poliza.IdUsuario
	  ,UserName
	  ,Usuario.Nombre AS UsuarioNombre
	  ,ApellidoPaterno
	  ,ApellidoMaterno
  FROM Poliza
INNER JOIN SubPoliza
	ON Poliza.IdSubPoliza = SubPoliza.IdSubPoliza  
INNER JOIN Vigencia
	ON Poliza.IdPoliza = Vigencia.IdPoliza
INNER JOIN Usuario
	ON Poliza.IdUsuario = Usuario.IdUsuario
WHERE Poliza.IdPoliza = @IdPoliza
GO
-----------------------------
CREATE PROCEDURE PolizaUpdate
@IdPoliza INT,
@Nombre VARCHAR(100),
@IdSubPoliza TINYINT,
@NumeroPoliza VARCHAR(20),
@IdUsuario INT,
@FechaInicio VARCHAR(10),
@FechaFin VARCHAR(10)
AS
UPDATE Poliza
   SET Nombre = @Nombre,IdSubPoliza = @IdSubPoliza
   ,NumeroPoliza = @NumeroPoliz
   ,FechaModificacion = GETDATE()
   ,IdUsuario = @IdUsuario

 WHERE IdPoliza = @IdPoliza

UPDATE Vigencia
   SET FechaInicio =  CONVERT(DATE, @FechaInicio,105)
   ,FechaFin = CONVERT(DATE, @FechaFin,105)
   ,FechaModificacion = GETDATE()
   ,IdUsuario = @IdUsuario
   ,IdPoliza = @IdPoliza

 WHERE IdPoliza = @IdPoliza

 GO
	