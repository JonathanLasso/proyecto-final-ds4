using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace proyectoFinalDs4.Models
{
    public class Agendar
    {
        public int idTarea { set; get; }

        [Display(Name = "Título de la tarea")]
        [Required(ErrorMessage = "El título es obligatorio")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string titulo { set; get; }

        [Display(Name = "Descripción")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string descripcion { set; get; }

        [Display(Name = "Dia de la semana")]
        [Required(ErrorMessage = "Debe seleccionar un día")]
        [Range(1, 7, ErrorMessage = "Seleccione un día válido")]
        public int diaSemana { set; get; }

        [Display(Name = "Hora de inicio")]
        [Required(ErrorMessage = "La hora de inicio es obligatoria")]
        public TimeSpan horaInicio { set; get; }

        [Display(Name = "Hora de finalización")]
        [Required(ErrorMessage = "La hora de fin es obligatoria")]
        public TimeSpan horaFin {  set; get; }

        [Display(Name = "¿Tarea completada?")]
        public bool completada { set; get; }

        ConnectionStringSettings connString = ConfigurationManager.ConnectionStrings["ConexionAgendarTarea"];
        SqlConnection conexion = new SqlConnection();

        public void abrirConexion()
        {
            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.ConnectionString = connString.ConnectionString;
                    conexion.Open();
                }

            }
            catch (Exception) { throw new Exception("Error al abrir la conexion"); }
        }
        public void cerrarConexion()
        {
            try
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();

            }
            catch (Exception) { throw new Exception("Error al cerrar la conexion"); }

        }

        public static List<Agendar> todasLasTareasAgendadas()
        {
            string consulta = "SELECT IdTarea, Titulo, Descripcion, DiaSemana, HoraInicio, HoraFin, TareaCompletada FROM TAREA ORDER BY IdTarea DESC";
            return obtenerListasTareas(consulta);
        }

        public static Agendar ObtenerPorId(int idTarea)
        {
            Agendar tarea = null;
            Agendar conexionTarea = new Agendar();
            try
            {
                conexionTarea.abrirConexion();
                SqlCommand comando = new SqlCommand(
                    "SELECT IdTarea, Titulo, Descripcion, DiaSemana, HoraInicio, HoraFin, TareaCompletada " +
                    "FROM TAREA WHERE IdTarea = @idTarea", conexionTarea.conexion);

                comando.Parameters.AddWithValue("@idTarea", idTarea);
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.Read())
                {
                    tarea = new Agendar
                    {
                        idTarea = Convert.ToInt32(reader["IdTarea"]),
                        titulo = reader["Titulo"].ToString(),
                        descripcion = reader["Descripcion"].ToString(),
                        diaSemana = Convert.ToInt32(reader["DiaSemana"]),
                        horaInicio = (TimeSpan)reader["HoraInicio"],
                        horaFin = (TimeSpan)reader["HoraFin"],
                        completada = Convert.ToBoolean(reader["TareaCompletada"])
                    };
                }
                conexionTarea.cerrarConexion();
                return tarea;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la tarea por ID: " + ex.Message);
            }
        }


        public static List<Agendar> obtenerListasTareas(string consulta)
        {
            List<Agendar> agendasTareas = new List<Agendar>();
            Agendar tarea = new Agendar();
            try
            {
                tarea.abrirConexion();
                SqlCommand comando = new SqlCommand(consulta, tarea.conexion);
                SqlDataReader leerDatos = comando.ExecuteReader();
                while (leerDatos.Read())
                {
                    agendasTareas.Add(new Agendar
                    {
                        idTarea = Convert.ToInt32(leerDatos["IdTarea"]),
                        titulo = leerDatos["Titulo"].ToString(),
                        descripcion = leerDatos["Descripcion"].ToString(),
                        diaSemana = Convert.ToInt32(leerDatos["DiaSemana"]), 
                        horaInicio = (TimeSpan)leerDatos["HoraInicio"], 
                        horaFin = (TimeSpan)leerDatos["HoraFin"], 
                        completada = Convert.ToBoolean(leerDatos["TareaCompletada"])
                    });
                }
                tarea.cerrarConexion();
                return agendasTareas;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: " + ex.ToString());
            }
        }
        public void insertarTarea()
        {
            try
            {
                abrirConexion();
                SqlCommand comando = new SqlCommand("INSERT INTO TAREA (Titulo, Descripcion, DiaSemana, HoraInicio, HoraFin, TareaCompletada) " +
                    "VALUES (@titulo, @descripcion, @diaSemana, @horaInicio, @horaFin, @tareaCompletada)", conexion);
                comando.Parameters.AddWithValue("@titulo", titulo);
                comando.Parameters.AddWithValue("@descripcion", descripcion);
                comando.Parameters.AddWithValue("@diaSemana", diaSemana);
                comando.Parameters.AddWithValue("@horaInicio", horaInicio);
                comando.Parameters.AddWithValue("@horaFin", horaFin);
                comando.Parameters.AddWithValue("@tareaCompletada", completada);
                comando.ExecuteNonQuery();
                cerrarConexion();
            }
            catch (Exception) { throw new Exception("Error al insertar una tarea"); }
        }
        public void actualizarTarea()
        {
            try
            {
                abrirConexion();
                SqlCommand comando = new SqlCommand(
                    "UPDATE TAREA SET " +
                    "Titulo = @titulo, " +
                    "Descripcion = @descripcion, " +
                    "DiaSemana = @diaSemana, " +
                    "HoraInicio = @horaInicio, " +
                    "HoraFin = @horaFin, " +
                    "TareaCompletada = @tareaCompletada " +
                    "WHERE IdTarea = @idTarea", conexion);
                comando.Parameters.AddWithValue("@idTarea", idTarea);
                comando.Parameters.AddWithValue("@titulo", titulo);
                comando.Parameters.AddWithValue("@descripcion", descripcion);
                comando.Parameters.AddWithValue("@diaSemana", diaSemana);
                comando.Parameters.AddWithValue("@horaInicio", horaInicio);
                comando.Parameters.AddWithValue("@horaFin", horaFin);
                comando.Parameters.AddWithValue("@tareaCompletada", completada);
                comando.ExecuteNonQuery();
                cerrarConexion();
            }
            catch (Exception)
            {
                throw new Exception("Error al actualizar la tarea");
            }
        }
        public void eliminarTarea(int idTarea)
        {
            try
            {
                abrirConexion();
                SqlCommand comando = new SqlCommand(
                    "DELETE FROM TAREA WHERE IdTarea = @idTarea", conexion);
                comando.Parameters.AddWithValue("@idTarea", idTarea);
                comando.ExecuteNonQuery();
                cerrarConexion();
            }
            catch (Exception)
            {
                throw new Exception("Error al eliminar la tarea");
            }
        }
        public void eliminarTodasLasTareas()
        {
            try
            {
                abrirConexion();
                SqlCommand comando = new SqlCommand(
                    "DELETE FROM TAREA", conexion);
                comando.ExecuteNonQuery();
                cerrarConexion();
            }
            catch (Exception)
            {
                throw new Exception("Error al eliminar todas las tareas");
            }
        }
    }
}