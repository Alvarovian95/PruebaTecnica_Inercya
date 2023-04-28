using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace Random
{
    internal class Program
    {
        static void Main(string[] args)
        {
            System.Random numeroAleatorio = new System.Random();
            int numTotal = 10000000;

            var rutaArchivo = ConfigurationManager.AppSettings["ArchivoNumeros"];
            rutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, rutaArchivo);
          
            HashSet<int> usedNums = new HashSet<int>();
        
            using (StreamWriter writer = new StreamWriter(rutaArchivo))
            {
                for (int i = 0; i < numTotal; i++)
                {
                    int numero = numeroAleatorio.Next();
            
                    while (usedNums.Contains(numero))
                    {
                        numero = numeroAleatorio.Next();
                    }
                  
                    usedNums.Add(numero);
                    writer.WriteLine(numero);
                }
            }
        }
    }
}
