CREATE DATABASE MSandovalAlfaSoluciones

CREATE TABLE Alumno(
IdAlumno INT PRIMARY KEY IDENTITY(1,1),
Nombre VARCHAR(50) NOT NULL,
Genero BIT,
Edad INT,
CONSTRAINT CHK_Edad CHECK(Edad>=15 AND Edad<=18))

CREATE TABLE Beca(
IdBeca INT PRIMARY KEY IDENTITY(1,1),
Tipo VARCHAR(50) NOT NULL,
)

CREATE TABLE AlumnoBeca(
IdAlumnoBeca INT PRIMARY KEY IDENTITY(1,1),
IdAlumno INT REFERENCES Alumno(IdAlumno),
IdBeca INT REFERENCES Beca(IdBeca)
)

CREATE PROCEDURE AlumnoGetAll
AS
SELECT IdAlumno,Nombre,Genero,Edad FROM Alumno 

INSERT INTO Alumno(
Nombre,Genero,Edad)VALUES('Diana',0,17)

ALTER TABLE Alumno
DROP CONSTRAINT CHECK(Edad>=15 OR Edad<=18)

INSERT INTO Beca(
Tipo)VALUES('Deportivas'),('Culturales')

SELECT * FROM Beca