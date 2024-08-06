using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ExampleCore.Controllers
{
    //[Route("[controller]")]
    public class UserController : Controller
    {
        //peticion HTTP GET por default pero se pude modificar
        //[HttpPost] como este ejemplo
        [Route("/User/index")]//se genera esta ruta pero muestra el mismo resultado, se pueden hacer multiples rutas 
        [Route("[controller]/[action]/{data}")]
        public IActionResult IndexUser(string data, int age)
        {
            string info = data + " " + age;
            return View( "IndexUser" , info );
        }

        public IActionResult IndexUser()
        {
            return View();
        }


    }
}