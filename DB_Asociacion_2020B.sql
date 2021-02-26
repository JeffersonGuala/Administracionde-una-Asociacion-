CREATE database Db_Asociacion2020B
go
use  Db_Asociacion2020B
go

---------------------------------------------------------------------------
----------------------------------------Tablas-----------------------------
---------------------------------------------------------------------------

CREATE TABLE TipoUsuario (
	id int IDENTITY(0,1) NOT NULL,
	tipo varchar(100) NOT NULL,
	CONSTRAINT TipoUsuario_PK PRIMARY KEY (id)
)

CREATE TABLE Semestre (
	id varchar(6) PRIMARY KEY NOT NULL,
	fechaInicio date NULL,
	fechaFin date NULL
)

CREATE TABLE tblUsuario(
	idUsuario int PRIMARY KEY IDENTITY(1,1),
	nombreUsuario nvarchar(100),
	passwordUsuario nvarchar(100),
	Nombre nvarchar(100),
	Correo nvarchar(100) UNIQUE,
	tipoUsuario int FOREIGN KEY REFERENCES TipoUsuario(id)
)

create table tblTipoProducto(
	idTipoProducto int identity(1,1) Primary Key Not null  ,
	NombreTipoP nvarchar(100) Not null,
)

create table tblTipoArticulo(
	idTipoArticulo int identity(1,1) Primary Key Not null  ,
	NombreTipoArt nvarchar(100) Not null,
)

create table tblTipoAportacion(
	idTipoAportacion int identity(1,1) Primary Key Not null  ,
	NombreTipoAport nvarchar(100) Not null,
)

create table tblCarrera(
	idCarrera int identity(1,1) Primary Key Not null  ,
	NombreCarrera nvarchar(100) Not null,
)

CREATE TABLE tblProductos (
	idProducto int identity(1,1) Primary Key NOT NULL ,
	NombreProducto nvarchar (100) Not null,
	Marca nvarchar (100) not null,
	idTipoProducto int,
	PrecioVenta decimal(10,2) not null,
	Stock int not null,
	PrecioTotaldeCompra decimal(10,2),
	idSemestre varchar(6) FOREIGN KEY REFERENCES Semestre(id),
	constraint FK_TipoProducto foreign key (idTipoProducto) references tblTipoProducto(idTipoProducto)
	ON DELETE CASCADE
	ON UPDATE CASCADE,
) 

create table tblArticulo(	
	idArticulo int identity(1,1) Primary Key NOT NULL ,
	NombreArticulo nvarchar (100) Not null,
	Marca nvarchar (100) not null,
	idTipoArticulo int,
	Costoenhoras decimal(10,2) not null,
	Cantidad int not null,
	PrecioTotaldeCompra decimal(10,2),
	idSemestre varchar(6) FOREIGN KEY REFERENCES Semestre(id),
	constraint FK_TipoArticulo foreign key (idTipoArticulo) references tblTipoArticulo(idTipoArticulo)
	ON DELETE CASCADE
	ON UPDATE CASCADE,
)

create table tblEstudiante(
	idEstudiante int identity(1,1) primary key not null,
	nombreCliente varchar(50),
	cedulaCliente varchar(13) UNIQUE,
	idCarrera int,
	correo varchar(50),
	telefono varchar(50),
	constraint FK_Carrera foreign key (idCarrera) references tblCarrera(idCarrera)
	ON DELETE CASCADE
	ON UPDATE CASCADE,
)

CREATE TABLE EstudianteSemestre (
	idSemestre varchar(6) FOREIGN KEY REFERENCES Semestre(id),
	idEstudiante int FOREIGN KEY REFERENCES tblEstudiante(idEstudiante) ON DELETE CASCADE,
	idTipoAportacion int FOREIGN KEY REFERENCES tblTipoAportacion(idTipoAportacion),
	valor_de_aportacion decimal(10,2),
	PRIMARY KEY (idSemestre, idEstudiante)
) 


create table tblGasto(
	idGasto int identity(1,1) primary key not null,
	fecha_registro datetime default getdate(),
	nombreGasto varchar(50),
	cantidad int,
	PrecioTotaldeCompra decimal(10,2),
	justificacion varchar(100),
	idSemestre varchar(6) FOREIGN KEY REFERENCES Semestre(id)
)



create table tblVenta(
	idVenta int identity(1,1) primary key not null,
	fechaVenta datetime default getdate(),
	total decimal(10,2),
	idUsuario int,
	idSemestre varchar(6) FOREIGN KEY REFERENCES Semestre(id),
	constraint FK_Usuario foreign key (idUsuario) references tblUsuario(idUsuario),
)

create table tblDetalle(
	idDetalle int identity(1,1) primary key not null,
	idProducto int,
	idVenta int,
	cantidadD int,
	importe decimal(10,2),
	constraint FK_Dproducto foreign key (idProducto) references tblProductos(idProducto),
	constraint FK_Dventa foreign key (idVenta) references tblVenta(idVenta)
)

CREATE TABLE tblPrestamos(
	idPrestamos int PRIMARY KEY IDENTITY (1,1),
	idArticulo int,
	idEstudiante int ,
	idUsuario int ,
	fechadeRegistro datetime default getdate(),
	tiempoAlquiler int,
	idSemestre varchar(6),
	CONSTRAINT fkEsReser FOREIGN KEY(idEstudiante) REFERENCES tblEstudiante (idEstudiante),
	CONSTRAINT fkRecReser FOREIGN KEY(idArticulo) REFERENCES tblArticulo (idArticulo),
	CONSTRAINT fkUsuReser FOREIGN KEY(idUsuario) REFERENCES tblUsuario (idUsuario),
	CONSTRAINT fkPrestamoSemestre FOREIGN KEY(idSemestre) REFERENCES Semestre (id)
)

create table tblDevoluciones(
	idDevoluciones int PRIMARY KEY IDENTITY (1,1) not null,
	idPrestamos int ,
	idUsuario int ,
	fechaDevolucion date,
	penalizacion decimal(10,2),
	justificacion varchar(250),
	totalaPagar decimal(10,2),
	idSemestre varchar(6),
	CONSTRAINT fkIdFactura FOREIGN KEY(idPrestamos) REFERENCES tblPrestamos (idPrestamos),
	CONSTRAINT fkUser FOREIGN KEY(idUsuario) REFERENCES tblUsuario (idUsuario),
	CONSTRAINT fkDevolucionesSemestre FOREIGN KEY(idSemestre) REFERENCES Semestre (id)
)

create table tblTotales(
	idTotales int PRIMARY KEY IDENTITY (1,1) not null,
	idGasto int,
	idVenta int,
	idEstudiante int,
	idDevoluciones int,
	idSemestre varchar(6),
	CONSTRAINT fkIdGastos FOREIGN KEY(idGasto) REFERENCES tblGasto (idGasto),
	CONSTRAINT fkIdVenta FOREIGN KEY(idVenta) REFERENCES tblVenta (idVenta),
	CONSTRAINT fkIdEstudiante FOREIGN KEY(idEstudiante) REFERENCES tblEstudiante (idEstudiante),
	CONSTRAINT fkIdDevoluciones FOREIGN KEY(idDevoluciones) REFERENCES tblDevoluciones (idDevoluciones),
	CONSTRAINT fkIdSemestre FOREIGN KEY(idSemestre) REFERENCES Semestre (id)
)

------------------------------------------------------------------------------------------------------------
----------------------------------------------- Data -------------------------------------------------------
------------------------------------------------------------------------------------------------------------

-- Tipos de Usuario

INSERT INTO TipoUsuario VALUES ('Administrador'), ('Asistente')

-- Usuarios

insert into tblUsuario values ('Administrador','admin','María Recalde','maria.recalde.admin01@gmail.com',0)
insert into tblUsuario values ('Asistente','asistente','Carla Zambrano','carla.zambrano.asis01@gmail.com',1)

-- Tipos de Producto

INSERT INTO tblTipoProducto 
VALUES 	('Informática'),
		('Deportes'),
		('Electrónicos'),
		('Varios')
		
-- Tipos de Articulo

INSERT INTO tblTipoArticulo
VALUES 	('Informática'),
		('Deportes'),
		('Electrónicos'),
		('Varios')

-- Semestres

INSERT INTO Semestre 
VALUES 	('2020-A', '2020-03-01', '2020-08-31'),
		('2020-B', '2020-11-01', '2021-04-30')
		
-- Carrera

insert into tblCarrera 
values 	('Electrónica y Redes de la Información'),
		('Electrónica y Telecomunicaciones'),
		('RRA-Tecnologías de la Información'),
		('RRA-Telecomunicaciones')
		
-- Tipo de Aportacion

insert into tblTipoAportacion 
values	('Aportante'),
		('No Aportante')
		
-- Estudiantes

INSERT INTO tblEstudiante
(idEstudiante, nombreCliente, cedulaCliente, idCarrera, correo, telefono)
VALUES	(1,'José Murillo', '1805094233', 1, 'jose@mail.com', '0979360024'),
		(2,'Joel Ruiz', '1805194133', 1, 'joel@mail.com', '0979360022')
		
-- EstudianteSemestre

INSERT INTO EstudianteSemestre
VALUES 	('2020-A', 1, 1, 10.75),
		('2020-B', 1, 1, 10.75),
		('2020-A', 2, 2, 0),
		('2020-B', 2, 1, 10.75)


------------------------------------------------------------------------------------------------------------
------------------------------------- Stored Procedures ----------------------------------------------------
------------------------------------------------------------------------------------------------------------

-- tblTipoProducto

CREATE PROCEDURE sp_listarTipoProducto
AS
	SELECT * FROM tblTipoProducto
GO

-- tblProductos

CREATE PROCEDURE sp_ingresarProducto
	@nombreProducto varchar(100),
	@marca varchar(100),
	@pvp float,
	@stock int,
	@precio float,
	@idSemestre varchar(6),
	@tipoProducto int
AS
	INSERT INTO tblProductos
	(NombreProducto, Marca, idTipoProducto, PrecioVenta, Stock, PrecioTotaldeCompra, idSemestre)
	VALUES(@nombreProducto, @marca, @tipoProducto, @pvp, @stock, @precio, @idSemestre);
GO

CREATE PROCEDURE sp_actualizarProducto
	@idProducto int,
	@nombreProducto varchar(100),
	@marca varchar(100),
	@pvp float,
	@stock int,
	@precio float,
	@idSemestre varchar(6),
	@tipoProducto int
AS
	UPDATE tblProductos
	SET	NombreProducto = @nombreProducto,
		Marca =  @marca, 
		idTipoProducto = @tipoProducto,
		PrecioVenta = @pvp,
		Stock = @stock,
		PrecioTotaldeCompra = @precio
	WHERE idProducto = @idProducto AND idSemestre = @idSemestre;
GO

CREATE PROCEDURE sp_eliminarProducto
	@idProducto int,
	@idSemestre varchar(6)
AS
	DELETE 
	FROM tblProductos
	WHERE idProducto = @idProducto AND idSemestre = @idSemestre
go

-- tblTipoArticulo

CREATE PROCEDURE sp_listarTipoArticulo
AS
	SELECT * FROM tblTipoArticulo
GO

-- tblProductos

CREATE PROCEDURE sp_ingresarArticulo
	@nombreArticulo varchar(100),
	@marca varchar(100),
	@costoEnHoras float,
	@cantidad int,
	@precio float,
	@idSemestre varchar(6),
	@idTipoArticulo int
AS
	INSERT INTO tblArticulo
	(NombreArticulo, Marca, idTipoArticulo, Costoenhoras, Cantidad, PrecioTotaldeCompra, idSemestre)
	VALUES(@nombreArticulo, @marca, @idTipoArticulo, @costoEnHoras, @cantidad, @precio, @idSemestre);
GO

CREATE PROCEDURE sp_actualizarArticulo
	@idArticulo int,
	@nombreArticulo varchar(100),
	@marca varchar(100),
	@costoEnHoras float,
	@cantidad int,
	@precio float,
	@idSemestre varchar(6),
	@idTipoArticulo int
AS
	UPDATE tblArticulo
	SET	NombreArticulo = @nombreArticulo,
		Marca =  @marca, 
		idTipoArticulo = @idTipoArticulo,
		Costoenhoras = @costoEnHoras,
		Cantidad = @cantidad,
		PrecioTotaldeCompra = @precio
	WHERE idArticulo = @idArticulo AND idSemestre = @idSemestre;
GO

CREATE PROCEDURE sp_eliminarArticulo
	@idArticulo int,
	@idSemestre varchar(6)
AS
	DELETE 
	FROM tblArticulo
	WHERE idArticulo = @idArticulo AND idSemestre = @idSemestre
go

-- tblGasto

CREATE PROCEDURE sp_ingresarGasto
	@nombreGasto varchar(100),
	@cantidad int,
	@precio float,
	@justificacion varchar(100),
	@idSemestre varchar(6)
AS
	INSERT INTO tblGasto
	(fecha_registro, nombreGasto, cantidad, PrecioTotaldeCompra, justificacion, idSemestre)
	VALUES(getdate(), @nombreGasto, @cantidad, @precio, @justificacion, @idSemestre);
GO

-- Semestre

CREATE PROCEDURE sp_ingresarSemestre
	@id varchar(6),
	@fechaInicio Date,
	@fechaFin Date
AS
	INSERT INTO Semestre 
	VALUES (@id, @fechaInicio, @fechaFin)
	INSERT INTO EstudianteSemestre
	SELECT 
		@id, 
		idEstudiante, 
		2, 
		0 
	FROM EstudianteSemestre e 
	WHERE e.idSemestre = 	(
								SELECT TOP(1) id 
								FROM Semestre s
								WHERE s.id <> @id
								ORDER BY s.fechaFin DESC
							)

--SP Ingresar estudiantes

CREATE PROCEDURE sp_Estudiantes
	@Tipodeaportacion int,
	@nombre varchar(50),
	@cedula varchar(50),
	@carrera int,
	@correo nvarchar(100),
	@telefono varchar(50),
	@valordeaportacion decimal(10,2),
	@idSemestre varchar(6),
	@id int OUTPUT
AS
	insert into tblEstudiante(nombreCliente, cedulaCliente, idCarrera, correo, telefono)
	values (@nombre, @cedula, @carrera, @correo, @telefono)
	select @id = Scope_Identity()
	INSERT INTO EstudianteSemestre (idSemestre, idEstudiante, idTipoAportacion, valor_de_aportacion)
	VALUES (@idSemestre, @id, @Tipodeaportacion, @valordeaportacion)
GO

--SP Actualizar estudiantes

CREATE PROCEDURE sp_ActualizarEstudiantes
	@idEstudiante int,
	@Tipodeaportacion int,
	@nombre varchar(50),
	@cedula varchar(50),
	@carrera int,
	@correo varchar(10),
	@telefono varchar(50),
	@valordeaportacion float,
	@idSemestre varchar(6)
AS
	UPDATE tblEstudiante
	set	nombreCliente=@nombre,
		cedulaCliente=@cedula,
		idCarrera=@carrera,
		correo=@correo,
		telefono=@telefono
	WHERE idEstudiante=@idEstudiante
	UPDATE EstudianteSemestre 
	SET	idTipoAportacion = @Tipodeaportacion,
		valor_de_aportacion = @valordeaportacion
	WHERE idEstudiante = @idEstudiante AND idSemestre = @idSemestre
GO

--SP eliminar estudiante

CREATE PROCEDURE sp_EliminarEstudiante
	@estudianteID int
AS
	DELETE FROM tblEstudiante
	WHERE idEstudiante=@estudianteID
go

create proc MostrarTipodeAportacion
as
	select* from tblTipoAportacion
GO
create proc MostrarCarrera
as
	select* from tblCarrera
GO

-- Ventas

CREATE PROCEDURE sp_AgregarVenta
	@totalVenta float,
	@idUsuario int,
	@idSemestre varchar(6),
	@id int OUTPUT
AS
	INSERT INTO tblVenta
	(fechaVenta, total, idUsuario, idSemestre)
	VALUES(CONVERT(DATE, getdate()), @totalVenta, @idUsuario, @idSemestre);
	SELECT @id = Scope_Identity()
GO
	
CREATE PROCEDURE sp_AgregarDetalle
	@idProducto int,
	@idVenta int,
	@cantidad int,
	@total float
AS
	INSERT INTO tblDetalle
	(idProducto, idVenta, cantidadD, importe)
	VALUES(@idProducto, @idVenta, @cantidad, @total);
	UPDATE tblProductos
	SET Stock = Stock - @cantidad
	WHERE idProducto = @idProducto
GO

-- Prestamo

CREATE PROCEDURE sp_AgregarPrestamo
	@idArticulo int,
	@idEstudiante int,
	@idUsuario int,
	@tiempo int,
	@idSemestre varchar(6)
AS
	INSERT INTO tblPrestamos
	(idArticulo, idEstudiante, idUsuario, fechadeRegistro, tiempoAlquiler, idSemestre)
	VALUES(@idArticulo, @idEstudiante, @idUsuario, CONVERT(DATE, getdate()), @tiempo, @idSemestre);
	UPDATE tblArticulo
	SET Cantidad = Cantidad - 1
	WHERE idArticulo = @idArticulo
GO

-- Devolución

CREATE PROCEDURE sp_AgregarDevolucion
	@idPrestamo int,
	@idUsuario int,
	@penalizacion float,
	@justificacion varchar(250),
	@total float,
	@idSemestre varchar(6),
	@idArticulo int
AS
	INSERT INTO tblDevoluciones
	(idPrestamos, idUsuario, fechaDevolucion, penalizacion, justificacion, totalaPagar, idSemestre)
	VALUES(@idPrestamo, @idUsuario, CONVERT(DATE, getdate()), @penalizacion, @justificacion, @total, @idSemestre)
	UPDATE tblArticulo
	SET Cantidad = Cantidad + 1
	WHERE idArticulo = @idArticulo
GO



-------------------------------------------------------------------------------------------------------
------------------------------------------- Views -----------------------------------------------------
-------------------------------------------------------------------------------------------------------

CREATE VIEW VProductos
AS
SELECT 
	idProducto, 
	NombreProducto, 
	Marca, 
	ROUND(PrecioTotaldeCompra , 2) AS PrecioTotaldeCompra, 
	Stock, 
	ROUND(PrecioVenta , 2) AS PrecioVenta, 
	t.idTipoProducto, 
	NombreTipoP,
	s.id AS Semestre
FROM tblProductos p JOIN tblTipoProducto t
	ON p.idTipoProducto = t.idTipoProducto
	JOIN Semestre s
		ON p.idSemestre = s.id

CREATE VIEW VArticulos
AS
SELECT 
	idArticulo, 
	NombreArticulo , 
	Marca, 
	ROUND(PrecioTotaldeCompra , 2) AS PrecioTotaldeCompra, 
	Cantidad, 
	ROUND(Costoenhoras , 2) AS Costoenhoras, 
	t.idTipoArticulo, 
	t.NombreTipoArt,
	s.id AS Semestre
FROM tblArticulo a JOIN tblTipoArticulo t
	ON a.idTipoArticulo = t.idTipoArticulo 
	JOIN Semestre s
		ON a.idSemestre = s.id

CREATE VIEW VGastos
AS
SELECT 
	idGasto, 
	fecha_registro, 
	nombreGasto, 
	cantidad, 
	PrecioTotaldeCompra, 
	justificacion, 
	idSemestre
FROM tblGasto g JOIN Semestre s
	ON g.idSemestre = s.id

CREATE VIEW VEstudiantes
AS
SELECT 	tblEstudiante.idEstudiante,
		tblTipoAportacion.idTipoAportacion,
		tblTipoAportacion.NombreTipoAport,
		Semestre.id AS Semestre,
		tblEstudiante.nombreCliente,
		tblEstudiante.cedulaCliente,
		tblCarrera.idCarrera,
		tblCarrera.NombreCarrera,
		tblEstudiante.correo,
		tblEstudiante.telefono,
		EstudianteSemestre.valor_de_aportacion
FROM tblEstudiante JOIN EstudianteSemestre 
		ON tblEstudiante.idEstudiante = EstudianteSemestre.idEstudiante
		JOIN tblCarrera
			ON tblEstudiante.idCarrera =tblCarrera.idCarrera
			JOIN tblTipoAportacion
				ON EstudianteSemestre.idTipoAportacion = tblTipoAportacion.idTipoAportacion
				JOIN Semestre
					ON EstudianteSemestre.idSemestre = Semestre.id

CREATE VIEW VPrestamos
AS
SELECT 
	p.idPrestamos,
	a.idArticulo,
	a.NombreArticulo,
	ROUND(a.Costoenhoras, 2) as Costoenhoras,
	e.cedulaCliente,
	e.nombreCliente,
	es.idTipoAportacion,
	u.idUsuario,
	u.Nombre, 
	p.fechadeRegistro,
	p.tiempoAlquiler, 
	s.id
FROM tblPrestamos p JOIN tblArticulo a
	ON p.idArticulo = a.idArticulo 
	JOIN tblEstudiante e
		ON p.idEstudiante = e.idEstudiante 
		JOIN tblUsuario u
			ON p.idUsuario = u.idUsuario 
			JOIN Semestre s 
				ON p.idSemestre = s.id
				JOIN EstudianteSemestre es
					ON (es.idSemestre = s.id AND e.idEstudiante = es.idEstudiante)
					WHERE NOT EXISTS (	SELECT idDevoluciones 
										FROM tblDevoluciones dev
										WHERE dev.idPrestamos = p.idPrestamos)

					
SELECT * FROM VEstudiantes WHERE Semestre = '2020-A'
					
SELECT * FROM VProductos WHERE Semestre = '2020-B'





	