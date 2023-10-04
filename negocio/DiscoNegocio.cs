using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;  // Agrego la libreria para poder usar los objetos de conexion. 
using dominio;                // Agrego la libreria para usar lo que esta en "dominio".
using System.Configuration;

namespace negocio 
{
    // En esta clase voy a crear los metodos de Acceso a Datos: ¡ATENCION! Esto se realizo antes de tener la clase "AccesoDatos".
    public class DiscoNegocio 
    {
        public List<Disco> listar(string id = "") // Le agrego un parametro opcional para modificar.
        {
            List<Disco> lista = new List<Disco>();
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();       
            SqlDataReader lector;                       
           
            try
            {
                conexion.ConnectionString = ConfigurationManager.AppSettings["cadenaConexion"];
                //conexion.ConnectionString = "server=.\\SQLEXPRESS; database=DISCOS_DB; integrated security=true";
                comando.CommandType = System.Data.CommandType.Text;                                                                                                                                                                        // D.Activo me trae solo los Discos Act.
                comando.CommandText = "select Titulo, FechaLanzamiento, CantidadCanciones, UrlImagenTapa, E.Descripcion as Estilo, T.Descripcion as Edicion, D.IdEstilo, D.IdTipoEdicion, D.Id, D.Activo from DISCOS D, ESTILOS E, TIPOSEDICION T where E.Id = IdEstilo AND IdTipoEdicion = T.Id "; // Ahora le digo "Cual" va a ser el texto, la Consulta.
                if (id != "")
                {
                    comando.CommandText += " and D.Id = " + id; // Esta consulta "lista" me trae un solo DISCO.
                }
                comando.Connection = conexion; 
                
                conexion.Open(); 
                lector = comando.ExecuteReader(); 
               
                while (lector.Read())
                {                  
                    Disco aux = new Disco();
                    aux.Id = (int)lector["Id"];
                    aux.Titulo = (string)lector["Titulo"];
                    aux.FechaLanzamiento = (DateTime)lector["FechaLanzamiento"];
                    aux.CantidadCanciones = (int)lector["CantidadCanciones"];
                            
                    if (!(lector["UrlImagenTapa"] is DBNull)) 
                    {
                        aux.UrlImagen = (string)lector["UrlImagenTapa"]; 
                    }
                    aux.Genero = new Estilo(); 
                    aux.Genero.Id = (int)lector["IdEstilo"];           
                    aux.Genero.Descripcion = (string)lector["Estilo"];
                    aux.Edicion = new TiposEdicion();
                    aux.Edicion.Id = (int)lector["IdTipoEdicion"];   
                    aux.Edicion.Descripcion = (string)lector["Edicion"];

                    aux.Activo = bool.Parse(lector["Activo"].ToString()); // Agrego a la lista para ver los Activos.

                    lista.Add(aux);                  
                }                
                conexion.Close(); 

                return lista; 
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Disco> listarConSP()
        {
            List<Disco> lista = new List<Disco>();
            AccesoDatos datos = new AccesoDatos();
            try
            {    
                //string consulta = "select Titulo, FechaLanzamiento, CantidadCanciones, UrlImagenTapa, E.Descripcion as Estilo, T.Descripcion as Edicion, D.IdEstilo, D.IdTipoEdicion, D.Id from DISCOS D, ESTILOS E, TIPOSEDICION T where E.Id = IdEstilo AND IdTipoEdicion = T.Id And D.Activo = 1";       
                //datos.setearConsulta(consulta); // Ahora si paso la Consulta Filtrada con lo que se necesite.

                datos.setearProcedimiento("storedListar"); // Uso el Procedimento de la DB.
                datos.ejecutarLectura();        

                while (datos.Lector.Read())     
                {                               
                                                
                    Disco aux = new Disco();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Titulo = (string)datos.Lector["Titulo"];
                    aux.FechaLanzamiento = (DateTime)datos.Lector["FechaLanzamiento"];
                    aux.CantidadCanciones = (int)datos.Lector["CantidadCanciones"];

                    if (!(datos.Lector["UrlImagenTapa"] is DBNull))
                    {
                        aux.UrlImagen = (string)datos.Lector["UrlImagenTapa"];
                    }

                    aux.Genero = new Estilo();
                    aux.Genero.Id = (int)datos.Lector["IdEstilo"];
                    aux.Genero.Descripcion = (string)datos.Lector["Estilo"];
                    aux.Edicion = new TiposEdicion();
                    aux.Edicion.Id = (int)datos.Lector["IdTipoEdicion"];
                    aux.Edicion.Descripcion = (string)datos.Lector["Edicion"];

                    aux.Activo = bool.Parse(datos.Lector["Activo"].ToString()); // Agrego a la lista para ver los Activos.

                    lista.Add(aux); 
                }

                return lista;        
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void agregar(Disco nuevo) 
        {
            AccesoDatos datos = new AccesoDatos(); 
            try
            {
                                                                                                                                              
                datos.setearConsulta("insert DISCOS (Titulo, FechaLanzamiento, CantidadCanciones, IdEstilo, IdTipoEdicion, UrlImagenTapa) values ('"+ nuevo.Titulo +"', '"+ nuevo.FechaLanzamiento.ToString("yyyy-MM-dd") + "', '"+ nuevo.CantidadCanciones +"', @idEstilo, @idTipoEdicion, @urlImagenTapa)");
                                  
                datos.setearParametro("idEstilo", nuevo.Genero.Id);       
                datos.setearParametro("idTipoEdicion", nuevo.Edicion.Id); 
                datos.setearParametro("urlImagenTapa", nuevo.UrlImagen);  
                datos.ejecutarAccion();                                   
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void agregarConSP(Disco nuevo) // Uso procedimiento almacenado.
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                //CREATE PROCEDURE storedAltaDisco
                //@titulo varchar(100), 
                //@fecha smalldatetime,
                //@canCanciones int,
                //@urlImagen varchar(200),
                //@idEstilo int,
                //@idTipoEdicion int
                //as
                //insert into DISCOS values(@titulo, @fecha, @canCanciones, @urlImagen, @idEstilo, @idTipoEdicion, 1)

                datos.setearProcedimiento("storedAltaDisco");
                datos.setearParametro("@titulo", nuevo.Titulo);
                datos.setearParametro("@fecha", nuevo.FechaLanzamiento);
                datos.setearParametro("@canCanciones", nuevo.CantidadCanciones);
                datos.setearParametro("@urlImagen", nuevo.UrlImagen);
                datos.setearParametro("@idEstilo", nuevo.Genero.Id);
                datos.setearParametro("@idTipoEdicion", nuevo.Edicion.Id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void modificar(Disco disco) 
        {
            AccesoDatos datos = new AccesoDatos();          
            try
            {
                datos.setearConsulta("update DISCOS set Titulo = @titulo, FechaLanzamiento = @fecha, CantidadCanciones = @canciones, UrlImagenTapa = @urlImagen, IdEstilo = @idEstilo, IdTipoEdicion = @idEdicion where Id = @id");
                datos.setearParametro("@titulo", disco.Titulo);             
                datos.setearParametro("@fecha", disco.FechaLanzamiento);
                datos.setearParametro("@canciones", disco.CantidadCanciones);
                datos.setearParametro("@urlImagen", disco.UrlImagen);
                datos.setearParametro("@idEstilo", disco.Genero.Id);
                datos.setearParametro("@idEdicion", disco.Edicion.Id);
                datos.setearParametro("@id", disco.Id);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

        }

        public void modificarConSP(Disco disco)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("storedModificarDisco");
                datos.setearParametro("@titulo", disco.Titulo);
                datos.setearParametro("@fecha", disco.FechaLanzamiento);
                datos.setearParametro("@canciones", disco.CantidadCanciones);
                datos.setearParametro("@urlImagen", disco.UrlImagen);
                datos.setearParametro("@idEstilo", disco.Genero.Id);
                datos.setearParametro("@idEdicion", disco.Edicion.Id);
                datos.setearParametro("@id", disco.Id);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void eliminar(int id) 
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("delete from DISCOS where Id = @id"); 
                datos.setearParametro("@id", id); 
                datos.ejecutarAccion();           
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void eliminarLogico(int id, bool activo = false) // Modifico el metodo para que Active y Desactive.
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("update DISCOS set Activo = @activo Where id = @id");
                datos.setearParametro("@id", id);
                datos.setearParametro("@activo", activo);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object filtrar(string campo, string criterio, string filtro, string estado)
        {
            List<Disco> lista = new List<Disco>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "select Titulo, FechaLanzamiento, CantidadCanciones, UrlImagenTapa, E.Descripcion as Estilo, T.Descripcion as Edicion, D.IdEstilo, D.IdTipoEdicion, D.Id, D.Activo from DISCOS D, ESTILOS E, TIPOSEDICION T where E.Id = IdEstilo AND IdTipoEdicion = T.Id AND ";

                if (campo == "Cantidad de Canciones")
                {
                    switch (criterio)
                    {
                        case "Mayor a":
                            consulta += " CantidadCanciones > " + filtro;
                            break;
                        case "Menor a":
                            consulta += " CantidadCanciones < " + filtro;
                            break;
                        default:
                            consulta += " CantidadCanciones = " + filtro;
                            break;
                    }
                }
                else if (campo == "Titulo")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "Titulo like '" + filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += "Titulo like '%" + filtro + "'";
                            break;
                        default:
                            consulta += "Titulo like '%" + filtro + "%'";
                            break;
                    }
                }
                else
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "E.Descripcion like '" + filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += "E.Descripcion like '%" + filtro + "'";
                            break;
                        default:
                            consulta += "E.Descripcion like '%" + filtro + "%'";
                            break;
                    }
                }
                if (estado == "Activo")
                {
                    consulta += " AND D.Activo = 1";
                }
                else if (estado == "Inactivo")
                {
                    consulta += " AND D.Activo = 0";
                }
                // Final de la consulta: where E.Id = IdEstilo AND IdTipoEdicion = T.Id   AND   Titulo like 's%'    AND    D.Activo = 1
                datos.setearConsulta(consulta); 
                datos.ejecutarLectura();        

                while (datos.Lector.Read())     
                {                               
                                               
                    Disco aux = new Disco();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Titulo = (string)datos.Lector["Titulo"];
                    aux.FechaLanzamiento = (DateTime)datos.Lector["FechaLanzamiento"];
                    aux.CantidadCanciones = (int)datos.Lector["CantidadCanciones"];

                    if (!(datos.Lector["UrlImagenTapa"] is DBNull)) 
                    {
                        aux.UrlImagen = (string)datos.Lector["UrlImagenTapa"]; 
                    }

                    aux.Genero = new Estilo(); 
                    aux.Genero.Id = (int)datos.Lector["IdEstilo"];          
                    aux.Genero.Descripcion = (string)datos.Lector["Estilo"];
                    aux.Edicion = new TiposEdicion();
                    aux.Edicion.Id = (int)datos.Lector["IdTipoEdicion"];    
                    aux.Edicion.Descripcion = (string)datos.Lector["Edicion"];

                    aux.Activo = bool.Parse(datos.Lector["Activo"].ToString());

                    lista.Add(aux);  
                }

                return lista;       
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}



