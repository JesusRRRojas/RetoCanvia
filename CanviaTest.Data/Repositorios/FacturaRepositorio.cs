using CanviaTest.Data.Contratos;
using CanviaTest.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Text;

namespace CanviaTest.Data.Repositorios
{
    public class FacturaRepositorio : IFacturaRepositorio
    {
        private readonly string _conn;
        public FacturaRepositorio(IConfiguration configuration)
        {
            _conn = configuration.GetConnectionString("DBConexion");
        }
        public Factura Actualizar(Factura entidad)
        {
            try
            {
                int res;
                using (OleDbConnection connection = new OleDbConnection(_conn))
                {
                    using (OleDbCommand command = new OleDbCommand())
                    {
                        connection.Open();

                        command.CommandText = $"Actualizar_Factura";

                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.Add("@IdFactura", OleDbType.Integer).Value = entidad.IdFactura;
                        command.Parameters.Add("@Serie_Factura", OleDbType.VarChar, 50).Value = entidad.Serie_Factura;
                        command.Parameters.Add("@Numero_Factura", OleDbType.Integer).Value = entidad.Numero_Factura;
                        command.Parameters.Add("@Fecha", OleDbType.Date).Value = entidad.Fecha;

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

                        command.CommandText = $"Eliminar_Factura";

                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.Add("@IdFactura", OleDbType.Integer).Value = id;


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

        public List<Factura> Facturas_x_Cliente(int idCliente)
        {
            try
            {
                List<Factura> Lista = new List<Factura>();
                using (OleDbConnection connection = new OleDbConnection(_conn))
                {
                    using (OleDbCommand command = new OleDbCommand())
                    {
                        connection.Open();

                        command.CommandText = $"Listar_Factura_x_Cliente";

                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.Add("@IdCliente", OleDbType.Integer).Value = idCliente;

                        command.Connection = connection;


                        OleDbDataReader reader = command.ExecuteReader();


                        while (reader.Read())
                        {
                            Lista.Add(new Factura()
                            {
                                IdFactura = reader.GetInt32(0),
                                Serie_Factura = reader.GetString(1),
                                Numero_Factura = reader.GetInt32(2),
                                //Fecha = reader.GetDateTime(3),
                                IdCliente = reader.GetInt32(4),
                                IdEmpleado = reader.GetInt32(5)

                            });

                        }
                    }

                }
                return Lista;
            }
            catch (Exception ex)
            {
                return new List<Factura>();
            }
        }

        public Factura Insertar(Factura entidad)
        {
            try
            {
                int res;
                using (OleDbConnection connection = new OleDbConnection(_conn))
                {
                    using (OleDbCommand command = new OleDbCommand())
                    {
                        connection.Open();

                        command.CommandText = $"Insertar_Factura";

                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.Add("@Serie_Factura", OleDbType.VarChar, 50).Value = entidad.Serie_Factura;
                        command.Parameters.Add("@Numero_Factura", OleDbType.Integer).Value = entidad.Numero_Factura;
                        command.Parameters.Add("@Fecha", OleDbType.Date).Value = entidad.Fecha;
                        command.Parameters.Add("@IdCliente", OleDbType.Integer).Value = entidad.IdCliente;
                        command.Parameters.Add("@IdEmpleado", OleDbType.Integer).Value = entidad.IdEmpleado;

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

        public List<Factura> Listar()
        {
            try
            {
                List<Factura> Lista = new List<Factura>();
                using (OleDbConnection connection = new OleDbConnection(_conn))
                {
                    using (OleDbCommand command = new OleDbCommand())
                    {
                        connection.Open();

                        command.CommandText = $"Listar_Factura";

                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Connection = connection;


                        OleDbDataReader reader = command.ExecuteReader();


                        while (reader.Read())
                        {
                            Lista.Add(new Factura()
                            {
                                IdFactura = reader.GetInt32(0), 
                                Serie_Factura = reader.GetString(1),
                                Numero_Factura = reader.GetInt32(2),
                                //Fecha = reader.GetDateTime(3),
                                IdCliente = reader.GetInt32(4),
                                IdEmpleado = reader.GetInt32(5)

                            });

                        }
                    }

                }
                return Lista;
            }
            catch (Exception ex)
            {
                return new List<Factura>();
            }
        }
    }
}
