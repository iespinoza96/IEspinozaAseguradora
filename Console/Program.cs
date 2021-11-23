using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //seleccion de entidades 
            int entidad = 0;
            System.Console.WriteLine("Bienvenido");
            System.Console.WriteLine("Elija la entidad a utilizar");
            System.Console.WriteLine("1.- Ususario");
            System.Console.WriteLine("2.- Empleado");
            System.Console.WriteLine("3.- Empresa");
            entidad = int.Parse(System.Console.ReadLine());

            switch (entidad)
            {
                case 1:
                            //Procedimientos productos
                            int producto = 0;
                            System.Console.WriteLine("Bienvenido a Usuario");
                            System.Console.WriteLine("Elija el procedimiento a realizar");
                            System.Console.WriteLine("1.-Agregar Usuario");
                            System.Console.WriteLine("2.-Actualizar Usuario");
                            System.Console.WriteLine("3.-Eliminar Usuario");
                            System.Console.WriteLine("4.-Seleccionar Usuario");
                            System.Console.WriteLine("5.-Seleccionar solo un Usuario");
                            producto = int.Parse(System.Console.ReadLine());
                            switch (producto)
                            {
                                //case 1:
                                //    Console.Producto.Add();//actualizar con stored procedure
                                //    break;

                                //case 2:
                                //    PL.Producto.Update();//actualizar con stored procedure
                                //    break;

                                //case 3:
                                //    PL.Producto.Delete();//borrar con stored procedure

                                //    break;

                                //case 4:
                                //    PL.Producto.GetAll();//seleccionar
                                //    break;

                                //case 5:
                                //    PL.Producto.GetById();//traer solo uno
                                //    break;
                                case 1:
                                    break;

                            }
                    break;
                case 2:
                    break;
                case 3:
                    break;
             }
        }
    }
}
