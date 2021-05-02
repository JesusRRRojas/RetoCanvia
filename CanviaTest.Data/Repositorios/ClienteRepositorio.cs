using CanviaTest.Data.Contratos;
using CanviaTest.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace CanviaTest.Data.Repositorios
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly string _conn;
        public ClienteRepositorio(IConfiguration configuration)
        {
            _conn = configuration.GetConnectionString("DBConexion");
        }
        public Cliente Actualizar(Cliente entidad)
        {
            try
            {
                int res;
                using (OleDbConnection connection = new OleDbConnection(_conn))
                {
                    using (OleDbCommand command = new OleDbCommand())
                    {
                        connection.Open();

                        command.CommandText = $"Actualizar_Cliente";

                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.Add("@IdCliente", OleDbType.Integer).Value = entidad.IdCliente;
                        command.Parameters.Add("@Nombre", OleDbType.VarChar, 100).Value = entidad.Nombre;
                        command.Parameters.Add("@Apellido", OleDbType.VarChar, 100).Value = entidad.Apellido;
                        command.Parameters.Add("@Direccion", OleDbType.VarChar, 100).Value = entidad.Direccion;
                        command.Parameters.Add("@Telefono", OleDbType.VarChar, 50).Value = entidad.Telefono;
                        command.Parameters.Add("@Email", OleDbType.VarChar, 100).Value = entidad.Email;
                        command.Parameters.Add("@DNI", OleDbType.VarChar, 8).Value = entidad.DNI;

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

                        command.CommandText = $"Eliminar_Cliente";

                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.Add("@IdCliente", OleDbType.Integer).Value = id;


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

        public Cliente Insertar(Cliente entidad)
        {
            try
            {
                int res;
                using (OleDbConnection connection = new OleDbConnection(_conn))
                {
                    using (OleDbCommand command = new OleDbCommand())
                    {
                        connection.Open();

                      
                        command.CommandText = $"Insertar_Cliente";

                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.Add("@Nombre", OleDbType.VarChar, 100).Value = entidad.Nombre;
                        command.Parameters.Add("@Apellido", OleDbType.VarChar, 100).Value = entidad.Apellido;
                        command.Parameters.Add("@Direccion", OleDbType.VarChar, 100).Value = entidad.Direccion;
                        command.Parameters.Add("@Telefono", OleDbType.VarChar, 50).Value = entidad.Telefono;
                        command.Parameters.Add("@Email", OleDbType.VarChar, 100).Value = entidad.Email;
                        command.Parameters.Add("@DNI", OleDbType.VarChar, 8).Value = entidad.DNI;

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

        public List<Cliente> Listar()
        {
            try
            {
                List<Cliente> Lista = new List<Cliente>();
                using (OleDbConnection connection = new OleDbConnection(_conn))
                {
                    using (OleDbCommand command = new OleDbCommand())
                    {
                        connection.Open();

                        command.CommandText = $"Listar_Cliente";

                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Connection = connection;


                        OleDbDataReader reader = command.ExecuteReader();


                        while (reader.Read())
                        {
                            Lista.Add(new Cliente()
                            {
                                IdCliente = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Apellido = reader.GetString(2), 
                                Direccion = reader.GetString(3),
                                Telefono = reader.GetString(4),
                                Email = reader.GetString(5),
                                DNI = reader.GetString(6)
                            });

                        }
                    }

                }
                return Lista;
            }
            catch (Exception ex)
            {
                return new List<Cliente>();
            }
        }

        public Cliente ListarCliente(int Id)
        {
            try
            {
                Cliente _cliente = new Cliente();
                List<Cliente> Lista = new List<Cliente>();
                using (OleDbConnection connection = new OleDbConnection(_conn))
                {
                    using (OleDbCommand command = new OleDbCommand())
                    {
                        connection.Open();

                        command.CommandText = $"Buscar_Cliente ";

                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.Add("@IdCliente", OleDbType.Integer).Value = Id;

                        command.Connection = connection;


                        
                        OleDbDataReader reader = command.ExecuteReader();


                        while (reader.Read())
                        {
                            _cliente.IdCliente = reader.GetInt32(0);
                            _cliente.Nombre = reader.GetString(1);
                            _cliente.Apellido = reader.GetString(2);
                            _cliente.Direccion = reader.GetString(3);
                            _cliente.Telefono = reader.GetString(4);
                            _cliente.Email = reader.GetString(5);
                            _cliente.DNI = reader.GetString(6);

                        }

                        
                    }

                }
                return _cliente;
            }
            catch (Exception ex)
            {
                return new Cliente();
            }
        }

        public Cliente ListarClienteConDetalle(int Id)
        {
            return ListarCliente(Id);
        }
    }
}
