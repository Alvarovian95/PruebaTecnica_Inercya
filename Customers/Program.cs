using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System;
using System.Data.Entity;
using Customers.Data;
using System.Data.SqlClient;
using System.Data;

namespace Customers
{
    public class Program
    {
        static void Main(string[] args)
        {       
            var rutaArchivo = ConfigurationManager.AppSettings["ArchivoCustomers"];
            rutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, rutaArchivo);

            List<Customer> listaCustomers = new List<Customer>();

            using (var reader = new StreamReader(rutaArchivo))
            {
                reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    var linea = reader.ReadLine().Split(';');

                    listaCustomers.Add(new Customer
                    {
                        Id = linea[0],
                        Name = linea[1],
                        Address = linea[2],
                        City = linea[3],
                        Country = linea[4],
                        Phone = linea[5],
                        PostalCode = linea[6]    
                    });
                }
            }
            using (var context = new ApplicationDbContext())
            {
                using (var bulkCopy = new SqlBulkCopy(context.Database.Connection.ConnectionString))
                {
                    bulkCopy.DestinationTableName = "Customers";

                    // Crear un DataTable
                    DataTable table = new DataTable();
                    table.Columns.Add("Id", typeof(string));
                    table.Columns.Add("Name", typeof(string));
                    table.Columns.Add("Address", typeof(string));
                    table.Columns.Add("City", typeof(string));
                    table.Columns.Add("Country", typeof(string));
                    table.Columns.Add("Phone", typeof(string));
                    table.Columns.Add("PostalCode", typeof(string));

                    foreach (var customer in listaCustomers)
                    {
                        table.Rows.Add(
                            customer.Id, 
                            customer.Name, 
                            customer.Address,
                            customer.City,
                            customer.Country,
                            customer.Phone,
                            customer.PostalCode
                            );
                    }              
                    bulkCopy.WriteToServer(table);                
                }
            }
        }
    }
}
