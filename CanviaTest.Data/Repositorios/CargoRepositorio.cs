using CanviaTest.Data.Contratos;
using CanviaTest.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Text;

namespace CanviaTest.Data.Repositorios
{
    public class CargoRepositorio : ICargoRepositorio
    {
        private readonly string _conn;
        public CargoRepositorio(IConfiguration configuration)
        {
            _conn = configuration.GetConnectionString("DBConexion");
        }

        public Cargo Actualizar(Cargo entidad)
        {
            try
            {
                int res;
                using (OleDbConnection connection = new OleDbConnection(_conn))
                {
                    using (OleDbCommand command = new OleDbCommand())
                    {
                        connection.Open();

                        command.CommandText = $"Actualizar_Cargo";

                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.Add("@IdCargo", OleDbType.Integer).Value = entidad.IdCargo;
                        command.Parameters.Add("@Descripcion", OleDbType.VarChar, 50).Value = entidad.Descripcion;
                        command.Parameters.Add("@Descripcion_Largo", OleDbType.VarChar, 500).Value = entidad.Descripcion_Largo;

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

                        command.CommandText = $"Eliminar_Cargo ";

                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.Add("@IdCargo", OleDbType.Integer).Value = id;


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

        public Cargo Insertar(Cargo entidad)
        {
            try
            {
                int res;
                using (OleDbConnection connection = new OleDbConnection(_conn))
                {
                    using (OleDbCommand command = new OleDbCommand())
                    {
                        connection.Open();

                        command.CommandText = $"Execute Insertar_Cargo @IdCargo, @Descripcion, @Descripcion_Largo";

                        command.Parameters.Add("@IdCargo", OleDbType.Integer).Value = entidad.IdCargo;
                        command.Parameters.Add("@Descripcion", OleDbType.VarChar, 50).Value = entidad.Descripcion;
                        command.Parameters.Add("@Descripcion_Largo", OleDbType.VarChar, 500).Value = entidad.Descripcion_Largo;

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

        public List<Cargo> Listar()
        {
            try
            {
                List<Cargo> Lista = new List<Cargo>();
                using (OleDbConnection connection = new OleDbConnection(_conn))
                {
                    using (OleDbCommand command = new OleDbCommand())
                    {
                        connection.Open();

                        command.CommandText = $"Listar_Cargo";

                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Connection = connection;


                        OleDbDataReader reader = command.ExecuteReader();


                        while (reader.Read())
                        {
                            Lista.Add(new Cargo()
                            {
                                IdCargo = reader.GetInt32(0),
                                Descripcion = reader.GetString(1),
                                Descripcion_Largo =  reader.GetString(2)
                            });

                        }
                    }

                }
                return Lista;
            }
            catch (Exception ex)
            {
                return new List<Cargo>();
            }
        }
    }
}
