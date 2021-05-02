using CanviaTest.Data.Contratos;
using CanviaTest.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Text;

namespace CanviaTest.Data.Repositorios
{
    public class EmpleadoRepositorio : IEmpleadoRepositorio
    {
        private readonly string _conn;
        public EmpleadoRepositorio(IConfiguration configuration)
        {
            _conn = configuration.GetConnectionString("DBConexion");
        }

        public Empleado Actualizar(Empleado entidad)
        {
            try
            {
                int res;
                using (OleDbConnection connection = new OleDbConnection(_conn))
                {
                    using (OleDbCommand command = new OleDbCommand())
                    {
                        connection.Open();

                        command.CommandText = $"Actualizar_Empleado";

                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.Add("@IdEmpleado", OleDbType.Integer).Value = entidad.IdEmpleado;
                        command.Parameters.Add("@Nombre", OleDbType.VarChar, 100).Value = entidad.Nombre;
                        command.Parameters.Add("@Apellido", OleDbType.VarChar, 100).Value = entidad.Apellido;
                        command.Parameters.Add("@@IdCargo", OleDbType.Integer).Value = entidad.IdCargo;
                       

                        command.Connection = connection;


                        res = command.ExecuteNonQuery();
                        connection.Close();

                    }

                }
                if (res > 0)
                {
                    return entidad;
                }
                return null;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool Eliminar(int id)
        {
            try
            {
                int res;
                using (OleDbConnection connection = new OleDbConnection(_conn))
                {
                    using (OleDbCommand command = new OleDbCommand())
                    {
                        connection.Open();

                        command.CommandText = $"Eliminar_Empleado";

                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.Add("@IdEmpleado", OleDbType.Integer).Value = id;


                        command.Connection = connection;


                        res = command.ExecuteNonQuery();
                        connection.Close();

                    }

                }
                if (res > 0)
                {
                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Empleado Insertar(Empleado entidad)
        {
            try
            {
                int res;
                using (OleDbConnection connection = new OleDbConnection(_conn))
                {
                    using (OleDbCommand command = new OleDbCommand())
                    {
                        connection.Open();

                        command.CommandText = $"Insertar_Empleado";

                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.Add("@Nombre", OleDbType.VarChar, 100).Value = entidad.Nombre;
                        command.Parameters.Add("@Apellido", OleDbType.VarChar, 100).Value = entidad.Apellido;
                        command.Parameters.Add("@@IdCargo", OleDbType.Integer).Value = entidad.IdCargo;

                        command.Connection = connection;


                        res = command.ExecuteNonQuery();
                        connection.Close();

                    }

                }
                if (res > 0)
                {
                    return entidad;
                }
                return null;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Empleado> Listar()
        {
            try
            {
                List<Empleado> Lista = new List<Empleado>();
                using (OleDbConnection connection = new OleDbConnection(_conn))
                {
                    using (OleDbCommand command = new OleDbCommand())
                    {
                        connection.Open();

                        command.CommandText = $"Listar_Empleado";

                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Connection = connection;


                        OleDbDataReader reader = command.ExecuteReader();


                        while (reader.Read())
                        {
                            Lista.Add(new Empleado()
                            {
                                IdEmpleado = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Apellido = reader.GetString(2),
                                IdCargo = reader.GetInt32(3)
                            });

                        }
                    }

                }
                return Lista;
            }
            catch (Exception ex)
            {
                return new List<Empleado>();
            }
        }
    }
}
