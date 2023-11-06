using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace server.Models
{
    public class GestorEmpleado
    {

        public List<Empleado> getEmpleados()
        {
            List<Empleado> lista = new List<Empleado>();
            string strConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();
            string queryString = "SELECT * FROM empleados";

            using (SqlConnection conn = new SqlConnection(strConn))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(queryString, conn);

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    long cuil = dr.GetInt64(0);
                    string nombre_completo = dr.GetString(1).Trim();
                    string pin = dr.GetString(2).Trim();


                    Empleado empleado = new Empleado((int)cuil, nombre_completo, pin);
                    lista.Add(empleado);


                }
                dr.Close();
                conn.Close();
            }

            return lista;
        }

        public bool addEmpleado(Empleado empleado)
        {
            bool res = false;

            string strConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand cmd = conn.CreateCommand();
          
                cmd.CommandText = "INSERT INTO empleados (cuil, nombre_completo, pin) VALUES (@cuil, @nombre_completo, @pin)";

                // Agregamos los parámetros necesarios
                cmd.Parameters.AddWithValue("@cuil", empleado.cuil);
                cmd.Parameters.AddWithValue("@nombre_completo", empleado.nombre_completo);
                cmd.Parameters.AddWithValue("@pin", empleado.pin);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    res = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    res = false;
                    throw;
                }
                finally
                {
                    cmd.Parameters.Clear();
                    conn.Close();
                }

                return res;
            }
        }

    }
}