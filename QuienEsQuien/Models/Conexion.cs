using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.IO;
using QuienEsQuien.Models;

namespace QuienesQuien.Models
{
    public class Conexion
    {
       //  private static string SC = "Server=10.128.8.16;Database=QEQB07;User Id=QEQB07;Password=QEQB07;";

        // private static string SC = "Server=LAPTOP-BT997U35\\SQLEXPRESS;Database=QEQB07;User Id=ORT;Password=ort;";

       // private static string SC = "Server=LAPTOP-BT997U35\\SQLEXPRESS;Database=QEQB07;User Id=ORT;Password=ort;"; a685389e155591274e7f758a3e389a8b5a726026;
       
        Encriptar encriptar = new Encriptar();

        public SqlConnection Conectar()
        {
            SqlConnection Conexion = new SqlConnection(SC);
            Conexion.Open();
            return Conexion;
        }

        public void Desconectar(SqlConnection Conn)
        {
            Conn.Close();
        }

        public Login Login(string user, string pass)
        {
            Login x = new Login();

            SqlConnection Conexion = Conectar();
            SqlCommand Comando = Conexion.CreateCommand();

            Comando.CommandText = "sp_Login";
            Comando.CommandType = System.Data.CommandType.StoredProcedure;
            Comando.Parameters.AddWithValue("@pUser", user);
            SqlDataReader DataReader = Comando.ExecuteReader();

            if (DataReader.Read())
            {
                bool Admin = Convert.ToBoolean(DataReader["Admin"]);
                string Contraseñat = DataReader["Contraseña"].ToString();

                x.Nombre = user;
                x.Admin = Admin;
                x.Contraseña = Contraseñat;
            }
            else
            {
                x.Nombre = "-1";
                x.Admin = false;
                x.Contraseña = "-1";
            }

            Conexion.Close();

            return x;
        }

        public int Register(string user, string pass)
        {
            string contraseña = encriptar.Crypto(pass);

            SqlConnection Conexion = Conectar();
            SqlCommand Comando = Conexion.CreateCommand();

            Comando.CommandText = "sp_Registro";
            Comando.CommandType = System.Data.CommandType.StoredProcedure;
            Comando.Parameters.AddWithValue("@pNombre", user);
            Comando.Parameters.AddWithValue("@pContraseña", contraseña);
            int x = Comando.ExecuteNonQuery();

            Conexion.Close();

            return x;
        }
        //ABM PREGUNTAS
        public List<Preguntas> ListarPreguntas()
        {
            List<Preguntas> Preguntas = new List<Preguntas>();
            SqlConnection conexion = Conectar();
            SqlCommand consulta = conexion.CreateCommand();
            consulta.CommandText = "sp_ListarPreguntas";
            consulta.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataReader dataReader = consulta.ExecuteReader();
            while (dataReader.Read())
            {
                int IdPregunta = Convert.ToInt32(dataReader["IdPregunta"]);
                string texto = (dataReader["Texto"].ToString());
                int IdCategoria = Convert.ToInt32(dataReader["IdCategoria"]);

                Preguntas P = new Preguntas(IdPregunta, texto, IdCategoria);
                Preguntas.Add(P);
            }

            Desconectar(conexion);
            return Preguntas;
        }
        public Preguntas ObtenerPregunta(int IdPregunta)
        {
            Preguntas UnaPregunta = new Preguntas();
            SqlConnection conexion = Conectar();
            SqlCommand consulta = conexion.CreateCommand();
            consulta.CommandText = "sp_SeleccionarPreguntas";
            consulta.CommandType = System.Data.CommandType.StoredProcedure;
            consulta.Parameters.AddWithValue("@PIDPregunta", IdPregunta);
            SqlDataReader dataReader = consulta.ExecuteReader();
            if (dataReader.Read())
            {
                UnaPregunta.IdPregunta = Convert.ToInt32(dataReader["IdPregunta"]);
                UnaPregunta.Texto = (dataReader["Texto"].ToString());
                UnaPregunta.IdCategoria = Convert.ToInt32(dataReader["IdCategoria"]);
            }

            Desconectar(conexion);
            return UnaPregunta;
        }
        public void InsertarPregunta(Preguntas C)
        {
            SqlConnection Conexion = Conectar();
            SqlCommand consulta = Conexion.CreateCommand();
            consulta.CommandText = "sp_AltaPreguntas";
            consulta.CommandType = System.Data.CommandType.StoredProcedure;
            consulta.Parameters.AddWithValue("@pTexto", C.Texto);
            consulta.Parameters.AddWithValue("@pIDCategoria", C.IdCategoria);
            consulta.ExecuteNonQuery();

            Desconectar(Conexion);
        }
        public void ModificarPregunta(Preguntas C)
        {
            SqlConnection conexion = Conectar();
            SqlCommand consulta = conexion.CreateCommand();
            consulta.CommandText = "sp_ModificarPregunta";
            consulta.CommandType = System.Data.CommandType.StoredProcedure;
            consulta.Parameters.AddWithValue("@pTexto", C.Texto);
            consulta.Parameters.AddWithValue("@pIDCategoria", C.IdCategoria);
            consulta.ExecuteNonQuery();

            Desconectar(conexion);
        }
        public void EliminarPregunta(int ID)
        {
            SqlConnection conexion = Conectar();
            SqlCommand consulta = conexion.CreateCommand();
            consulta.CommandText = "sp_BajaPreguntas";
            consulta.CommandType = System.Data.CommandType.StoredProcedure;
            consulta.Parameters.AddWithValue("@pID", ID);
            consulta.ExecuteNonQuery();

            Desconectar(conexion);
        }

        ///ABM CATEGORIA
        public void InsertarCategoria(Categorias C)
        {

            SqlConnection Conexion = Conectar();
            SqlCommand consulta = Conexion.CreateCommand();
            consulta.CommandText = "sp_AltaCategoria";
            consulta.CommandType = System.Data.CommandType.StoredProcedure;
            consulta.Parameters.AddWithValue("@pNombre", C.Nombre);
            consulta.ExecuteNonQuery();

            Desconectar(Conexion);
        }
        public List<Categorias> ListarCategorias()
        {
            List<Categorias> Categoria = new List<Categorias>();
            SqlConnection conexion = Conectar();
            SqlCommand consulta = conexion.CreateCommand();
            consulta.CommandText = "sp_ListarCategorias";
            consulta.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataReader dataReader = consulta.ExecuteReader();
            while (dataReader.Read())
            {
                int IdCategoria = Convert.ToInt32(dataReader["IdCategoria"]);
                string NombreCat = (dataReader["Nombre"].ToString());

                Categorias C = new Categorias(IdCategoria, NombreCat);
                Categoria.Add(C);
            }
            Desconectar(conexion);
            return Categoria;
        }
        public Categorias ObtenerCategoria(int IdCategoria)
        {
            Categorias UnaCategoria = new Categorias();
            SqlConnection conexion = Conectar();
            SqlCommand consulta = conexion.CreateCommand();
            consulta.CommandText = "sp_SeleccionarCategoria";
            consulta.CommandType = System.Data.CommandType.StoredProcedure;
            consulta.Parameters.AddWithValue("@pID", IdCategoria);
            SqlDataReader dataReader = consulta.ExecuteReader();
            if (dataReader.Read())
            {
                UnaCategoria.IdCategoria = Convert.ToInt32(dataReader["IdCategoria"]);
                UnaCategoria.Nombre = (dataReader["Nombre"].ToString());

            }

            Desconectar(conexion);
            return UnaCategoria;
        }
        public void ModificarCategoria(Categorias C)
        {

            SqlConnection conexion = Conectar();
            SqlCommand consulta = conexion.CreateCommand();
            consulta.CommandText = "sp_ModificacionCategoria";
            consulta.CommandType = System.Data.CommandType.StoredProcedure;
            consulta.Parameters.AddWithValue("@pID", C.IdCategoria);
            consulta.Parameters.AddWithValue("@pNombre", C.Nombre);
            consulta.ExecuteNonQuery();
            Desconectar(conexion);

        }

        public void EliminarTrabajador(int t)
        {


            SqlConnection conexion = Conectar();
            SqlCommand consulta = conexion.CreateCommand();
            consulta.CommandText = "sp_BajaCategoria";
            consulta.CommandType = System.Data.CommandType.StoredProcedure;
            consulta.Parameters.AddWithValue("@pID", t);
            consulta.ExecuteNonQuery();
            Desconectar(conexion);

        }


        // ABM PERSONAJES
        public void InsertarPersonaje(Personajes P)
        {

            SqlConnection Conexion = Conectar();
            SqlCommand consulta = Conexion.CreateCommand();
            consulta.CommandText = "sp_AltaPersonajes";
            consulta.CommandType = System.Data.CommandType.StoredProcedure;
            consulta.Parameters.AddWithValue("@pNombre", P.Nombre);
            consulta.Parameters.AddWithValue("@pImagen", P.Imagen);
            consulta.Parameters.AddWithValue("@pCategoria", P.IdCategoria);
            consulta.ExecuteNonQuery();
            Desconectar(Conexion);

        }
        public List<Personajes> ListarPersonajes()
        {
            List<Personajes> ListaPersonjaes = new List<Personajes>();
            SqlConnection conexion = Conectar();
            SqlCommand consulta = conexion.CreateCommand();
            consulta.CommandText = "sp_ListarPersonajes";
            consulta.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataReader dataReader = consulta.ExecuteReader();
            while (dataReader.Read())
            {
                int IdPersonaje = Convert.ToInt32(dataReader["IdPersonajes"]);
                string NombrePer = (dataReader["Nombre_personaje"].ToString());
                string Imagen = (dataReader["Imagen"].ToString());
                int IdCategoria = Convert.ToInt32(dataReader["IdCategoria"]);
                Personajes P = new Personajes(IdPersonaje, NombrePer, Imagen, IdCategoria);
                ListaPersonjaes.Add(P);
            }
            Desconectar(conexion);
            return ListaPersonjaes;
        }
        public Personajes ObtenerPersonaje(int IdPersonaje)
        {
            Personajes MiPersonaje = new Personajes();
            SqlConnection conexion = Conectar();
            SqlCommand consulta = conexion.CreateCommand();
            consulta.CommandText = "sp_SeleccionarPersonaje";
            consulta.CommandType = System.Data.CommandType.StoredProcedure;
            consulta.Parameters.AddWithValue("@pID", IdPersonaje);
            SqlDataReader dataReader = consulta.ExecuteReader();
            if (dataReader.Read())
            {
                MiPersonaje.IdPersonaje = Convert.ToInt32(dataReader["IdPersonajes"]);
                MiPersonaje.Nombre = (dataReader["Nombre_personaje"].ToString());
                MiPersonaje.Imagen = (dataReader["Imagen"].ToString());
                MiPersonaje.IdCategoria = Convert.ToInt32(dataReader["IdCategoria"]);

            }

            Desconectar(conexion);
            return MiPersonaje;
        }
        public void ModificarPersonaje(Personajes P)
        {

            SqlConnection conexion = Conectar();
            SqlCommand consulta = conexion.CreateCommand();
            consulta.CommandText = "sp_ModificacionPersonajes";
            consulta.CommandType = System.Data.CommandType.StoredProcedure;
            consulta.Parameters.AddWithValue("@pID", P.IdPersonaje);
            consulta.Parameters.AddWithValue("@pNombre", P.Nombre);
            consulta.Parameters.AddWithValue("@pImagen", P.Imagen);
            consulta.Parameters.AddWithValue("@pCategoria", P.IdCategoria);
            consulta.ExecuteNonQuery();
            Desconectar(conexion);

        }

        public void EliminarPersonaje(int t)
        {


            SqlConnection conexion = Conectar();
            SqlCommand consulta = conexion.CreateCommand();
            consulta.CommandText = "sp_BajaPersonajes";
            consulta.CommandType = System.Data.CommandType.StoredProcedure;
            consulta.Parameters.AddWithValue("@pID", t);
            consulta.ExecuteNonQuery();
            Desconectar(conexion);

        }

        //ABM PREGUNTAS_PERSONAJES (mateo)

        //public void InsertarPersonaje_Pregunta(Personajes_Preguntas PP)
        //{

        //    SqlConnection Conexion = Conectar();
        //    SqlCommand consulta = Conexion.CreateCommand();
        //    consulta.CommandText = "sp_AltaPersonajes";
        //    consulta.CommandType = System.Data.CommandType.StoredProcedure;
        //    consulta.Parameters.AddWithValue("@pNombre", P.Nombre);
        //    consulta.Parameters.AddWithValue("@pImagen", P.Imagen);
        //    consulta.Parameters.AddWithValue("@pCategoria", P.IdCategoria);
        //    consulta.ExecuteNonQuery();
        //    Desconectar(Conexion);

        //}
        public List<Personajes_Preguntas> ListarPersonajes_Pregunta()
        {
            List<Personajes_Preguntas> ListaPersonajes_Pregunta = new List<Personajes_Preguntas>();
            SqlConnection conexion = Conectar();
            SqlCommand consulta = conexion.CreateCommand();
            consulta.CommandText = "sp_ListarPersonajes_pregunta";
            consulta.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataReader dataReader = consulta.ExecuteReader();
            while (dataReader.Read())
            {
                int IdPersonaje = Convert.ToInt32(dataReader["IdPersonaje"]); 
                int IdPregunta = Convert.ToInt32(dataReader["IdPregunta"]);
                Personajes_Preguntas PP = new Personajes_Preguntas(IdPersonaje, IdPregunta);
                ListaPersonajes_Pregunta.Add(PP);
            }
            Desconectar(conexion);
            return ListaPersonajes_Pregunta;
        }
        public Personajes ObtenerPersonaje_Pregunta(int IdPersonaje)
        {
            Personajes MiPersonaje = new Personajes();
            SqlConnection conexion = Conectar();
            SqlCommand consulta = conexion.CreateCommand();
            consulta.CommandText = "sp_SeleccionarPersonaje";
            consulta.CommandType = System.Data.CommandType.StoredProcedure;
            consulta.Parameters.AddWithValue("@pID", IdPersonaje);
            SqlDataReader dataReader = consulta.ExecuteReader();
            if (dataReader.Read())
            {
                MiPersonaje.IdPersonaje = Convert.ToInt32(dataReader["IdPersonajes"]);
                MiPersonaje.Nombre = (dataReader["Nombre_personaje"].ToString());
                MiPersonaje.Imagen = (dataReader["Imagen"].ToString());
                MiPersonaje.IdCategoria = Convert.ToInt32(dataReader["IdCategoria"]);

            }

            Desconectar(conexion);
            return MiPersonaje;
        }
        public void ModificarPersonaje_Pregunta(Personajes P)
        {

            SqlConnection conexion = Conectar();
            SqlCommand consulta = conexion.CreateCommand();
            consulta.CommandText = "sp_ModificacionPersonajes";
            consulta.CommandType = System.Data.CommandType.StoredProcedure;
            consulta.Parameters.AddWithValue("@pID", P.IdPersonaje);
            consulta.Parameters.AddWithValue("@pNombre", P.Nombre);
            consulta.Parameters.AddWithValue("@pImagen", P.Imagen);
            consulta.Parameters.AddWithValue("@pCategoria", P.IdCategoria);
            consulta.ExecuteNonQuery();
            Desconectar(conexion);

        }

        public void EliminarPersonaje_Pregunta(int t)
        {


            SqlConnection conexion = Conectar();
            SqlCommand consulta = conexion.CreateCommand();
            consulta.CommandText = "sp_BajaPersonajes";
            consulta.CommandType = System.Data.CommandType.StoredProcedure;
            consulta.Parameters.AddWithValue("@pID", t);
            consulta.ExecuteNonQuery();
            Desconectar(conexion);

        }

        //--------------------------------------------------------------------------------
    }
}