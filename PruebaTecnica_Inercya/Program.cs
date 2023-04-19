using PruebaTecnica_Inercya.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PruebaTecnica_Inercya
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Listado de productos
            var rutaArchivo = ConfigurationManager.AppSettings["ArchivoProductos"];
            rutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, rutaArchivo);

            List<Product> listaProductos = new List<Product>();

            using (var reader = new StreamReader(rutaArchivo))
            {
                reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    var linea = reader.ReadLine().Split(';');

                    listaProductos.Add(new Product
                    {
                        Id = int.Parse(linea[0]),
                        CategoryId = int.Parse(linea[1]),
                        Name = linea[2],
                        Price = Decimal.Parse(linea[3])
                    });
                }
            }

            //Listado de categorías
            rutaArchivo = ConfigurationManager.AppSettings["ArchivoCategorias"];
            rutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, rutaArchivo);

            List<Category> listaCategorias = new List<Category>();

            using (var reader = new StreamReader(rutaArchivo))
            {
                reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    var linea = reader.ReadLine().Split(';');

                    listaCategorias.Add(new Category
                    {
                        Id = int.Parse(linea[0]),
                        Name = linea[1],
                        Description = linea[2],

                    });
                }
            }

            //Asignar productos a su categoría correspondiente
            foreach(var categoria in listaCategorias)
            {
                categoria.Products = listaProductos.Where(p => p.CategoryId == categoria.Id).ToList();
            }

            //Serialización a xml   
            CrearFicheroXML(listaCategorias);

            //Serialización a json   
            CrearFicheroJSON(listaCategorias);
        }

        static void CrearFicheroXML(List<Category> catalogo)
        {
            try
            {
                var rutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Docs", "Catalog.xml");

                XmlSerializer serializer = new XmlSerializer(typeof(List<Category>));
                using (FileStream fileStream = new FileStream(rutaArchivo, FileMode.Create))
                {
                    serializer.Serialize(fileStream, catalogo);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        static void CrearFicheroJSON(List<Category> catalogo)
        {
            try
            {
                var rutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Docs", "Catalog.json");
                var json = JsonSerializer.Serialize(catalogo);

                File.WriteAllText(rutaArchivo, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
