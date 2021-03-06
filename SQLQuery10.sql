USE [IEspinozaAseguradora]
GO
/****** Object:  StoredProcedure [dbo].[EmpleadoGetAll]    Script Date: 11/16/2021 6:18:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[EmpleadoGetAll]
@IdEmpresa INT,
@Nombre VARCHAR(50),
@ApellidoPaterno VARCHAR(50),
@ApellidoMaterno VARCHAR(50)
AS
	IF(@IdEmpresa <=0)
	BEGIN
		
	SELECT NumEmpleado
      ,RFC
      ,Empleado.Nombre
      ,ApellidoPaterno
      ,ApellidoMaterno
      ,Empleado.Email 
      ,Empleado.Telefono 
      ,FechaNacimiento
      ,NSS
      ,FechaIngreso
      ,Foto
      ,Empresa.IdEmpresa
	  ,Empresa.Nombre AS EmpresaNombre
	FROM Empleado

	INNER JOIN Empresa ON Empresa.IdEmpresa = Empleado.IdEmpresa
	WHERE Empleado.Nombre LIKE '%'+@Nombre+'%'AND ApellidoPaterno LIKE '%'+@ApellidoPaterno+'%' AND ApellidoMaterno LIKE '%'+@ApellidoMaterno+'%'
	ORDER BY Empleado.Nombre ASC 
	END 
  ELSE 
	BEGIN 
		SELECT NumEmpleado
      ,RFC
      ,Empleado.Nombre 
      ,ApellidoPaterno
      ,ApellidoMaterno
      ,Empleado.Email 
      ,Empleado.Telefono 
      ,FechaNacimiento
      ,NSS
      ,FechaIngreso
      ,Foto
      ,Empresa.IdEmpresa
	  ,Empresa.Nombre AS EmpresaNombre
	FROM Empleado

	INNER JOIN Empresa ON Empresa.IdEmpresa = Empleado.IdEmpresa
	WHERE Empleado.Nombre LIKE '%'+@Nombre+'%'AND ApellidoPaterno LIKE '%'+@ApellidoPaterno+'%' AND ApellidoMaterno LIKE '%'+@ApellidoMaterno+'%'
	ORDER BY Empleado.Nombre ASC 
	END