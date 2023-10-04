--ATENCION ESTO ES DE DISCOS_DB
USE [DISCOS_DB] 
GO

CREATE PROCEDURE [dbo].[storedAltaDisco]
@titulo varchar(100), 
@fecha smalldatetime,
@canCanciones int,
@urlImagen varchar(200),
@idEstilo int,
@idTipoEdicion int
as 
insert into DISCOS values (@titulo, @fecha, @canCanciones, @urlImagen, @idEstilo, @idTipoEdicion, 1)

go

CREATE PROCEDURE [dbo].[storedListar] as
select Titulo, FechaLanzamiento, CantidadCanciones, UrlImagenTapa, E.Descripcion as Estilo, T.Descripcion as Edicion, D.IdEstilo, D.IdTipoEdicion, D.Id, D.Activo
from DISCOS D, ESTILOS E, TIPOSEDICION T 
where E.Id = IdEstilo AND IdTipoEdicion = T.Id 

go

CREATE PROCEDURE [dbo].[storedModificarDisco]
@titulo varchar(100), 
@fecha smalldatetime,
@canciones int,
@urlImagen varchar(200),
@idEstilo int,
@idEdicion int,
@id int
as
update DISCOS set Titulo = @titulo, FechaLanzamiento = @fecha, CantidadCanciones = @canciones, 
UrlImagenTapa = @urlImagen, IdEstilo = @idEstilo, IdTipoEdicion = @idEdicion 
where Id = @id

