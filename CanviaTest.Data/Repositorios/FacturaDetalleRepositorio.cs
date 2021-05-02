using CanviaTest.Data.Contratos;
using CanviaTest.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Text;

namespace CanviaTest.Data.Repositorios
{
    public class FacturaDetalleRepositorio : IFacturaDetalleRepositorio
    {
        private readonly string _conn;
        public FacturaDetalleRepositorio(IConfiguration configuration)
        {
            _conn = configuration.GetConnectionString("DBConexion");
        }

        public Factura_Detalle Actualizar(Factura_Detalle entidad)
        {
            try
            {
                int res;
                using (OleDbConnection connection = new OleDbConnection(_conn))
                {
                    using (OleDbCommand command = new OleDbCommand())
                    {
                        connection.Open();

                        command.CommandText = $"Actualizar_Factura_Detalle";

                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.Add("@IdFactura", OleDbType.Integer).Value = entidad.IdFactura;
                        command.Parameters.Add("@IdProducto", OleDbType.Integer).Value = entidad.IdProducto;
                        command.Parameters.Add("@Cantidad", OleDbType.Integer).Value = entidad.Cantidad;
                        command.Parameters.Add("@Precio_Unidad", OleDbType.Decimal).Value = entidad.Precio_Unidad;

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


        public bool Eliminar(int idFactura, int idProducto)
        {
            try
            {
                int res;
                using (OleDbConnection connection = new OleDbConnection(_conn))
                {
                    using (OleDbCommand command = new OleDbCommand())
                    {
                        connection.Open();

                        command.CommandText = $"Eliminar_Factura_Detalle";

                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.Add("@IdFactura", OleDbType.Integer).Value = idFactura;
                        command.Parameters.Add("@IdProducto", OleDbType.Integer).Value = idProducto;

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

        public bool Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public Factura_Detalle Insertar(Factura_Detalle entidad)
        {
            try
            {
                int res;
                using (OleDbConnection connection = new OleDbConnection(_conn))
                {
                    using (OleDbCommand command = new OleDbCommand())
                    {
                        connection.Open();

                        command.CommandText = $"Insertar_Factura_Detalle";

                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.Add("@IdFactura", OleDbType.Integer).Value = entidad.IdFactura;
                        command.Parameters.Add("@IdProducto", OleDbType.Integer).Value = entidad.IdProducto;
                        command.Parameters.Add("@Cantidad", OleDbType.Integer).Value = entidad.Cantidad;
                        command.Parameters.Add("@Precio_Unidad", OleDbType.Decimal).Value = entidad.Precio_Unidad;

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

        public List<Factura_Detalle> Listar()
        {
            try
            {
                List<Factura_Detalle> Lista = new List<Factura_Detalle>();
                using (OleDbConnection connection = new OleDbConnection(_conn))
                {
                    using (OleDbCommand command = new OleDbCommand())
                    {
                        connection.Open();

                        command.CommandText = $"Listar_Factura_Detalle";

                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Connection = connection;


                        OleDbDataReader reader = command.ExecuteReader();


                        while (reader.Read())
                        {
                            Lista.Add(new Factura_Detalle()
                            {
                                IdFactura = reader.GetInt32(0),
                                IdProducto = reader.GetInt32(1),
                                Cantidad = reader.GetInt32(2),
                                Precio_Unidad = reader.GetDecimal(3)
                            });

                        }
                    }

                }
                return Lista;
            }
            catch (Exception ex)
            {
                return new List<Factura_Detalle>();
            }
        }

        public List<Factura_Detalle> Listar_x_Factura(int idFactura)
        {
            try
            {
                List<Factura_Detalle> Lista = new List<Factura_Detalle>();
                using (OleDbConnection connection = new OleDbConnection(_conn))
                {
                    using (OleDbCommand command = new OleDbCommand())
                    {
                        connection.Open();

                        command.CommandText = $"Listar_Factura_Detalle";

                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.Add("@IdFactura", OleDbType.Integer).Value = idFactura;

                        command.Connection = connection;


                        OleDbDataReader reader = command.ExecuteReader();


                        while (reader.Read())
                        {
                            Lista.Add(new Factura_Detalle()
                            {
                                IdFactura = reader.GetInt32(0),
                                IdProducto = reader.GetInt32(1),
                                Cantidad = reader.GetInt32(2),
                                Precio_Unidad = reader.GetDecimal(3)
                            });

                        }
                    }

                }
                return Lista;
            }
            catch (Exception ex)
            {
                return new List<Factura_Detalle>();
            }
        }
    }
}
