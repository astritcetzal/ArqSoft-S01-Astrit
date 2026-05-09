using Catalogo.Models;
using Microsoft.AspNetCore.Mvc;

namespace Catalogo.Controllers
{
    public class CatalogoController : Controller
    {
        private static List<Item> _items = new()
        {
            new Item
            {
                Id = 1,
                Titulo = "Skool luv Affair",
                tiposLanzamiento = "Mini álbum (EP)",
                Ano = 2014,
                numeroCanciones = 10,
                Descripcion = "Marca el clímax de la School Trilogy con un estilo Hip-Hop y temas sobre el amor y la escuela."
            },
            new Item
            {
                Id = 2,
                Titulo = "The Most Beautiful Moment in Life: Young Forever",
                tiposLanzamiento = "Álbum recopilado",
                Ano = 2016,
                numeroCanciones = 23,
                Descripcion ="Cierra la aclamada era HYYH, donde su popularidad explotó con temáticas sobre la juventud."
            },
            new Item
            {
                Id = 3,
                Titulo = "Wings",
                tiposLanzamiento = "Álbum de estudio",
                Ano = 2016,
                numeroCanciones = 15,
                Descripcion = "Una era oscura y conceptual que incluyó por primera canciones en solitario para cada miembro."
            },
            new Item

            {
                Id = 4,
                Titulo = "Love Yourself Tear",
                tiposLanzamiento = "Álbum de estudio",
                Ano = 2018,
                numeroCanciones = 11,
                Descripcion = "La parte central y más introspectiva de la era Love Yourself."
            },

            new Item
            {
                Id = 5,
                Titulo = "Map of the Soul: 7",
                tiposLanzamiento = "Álbum de estudio",
                Ano = 2020,
                numeroCanciones = 19,
                Descripcion = "Una celebración masiva de sus 7 años de carrera, resumiendo todas las eras anteriores."
            }

        };
        public IActionResult Index(String? tipo)
        {
            var resultado = string.IsNullOrEmpty(tipo)
                ? _items
                : _items.Where(i => i.tiposLanzamiento == tipo).ToList();
            ViewBag.Tipos = _items.Select(i => i.tiposLanzamiento).Distinct().ToList();
            ViewBag.TipoActual = tipo;

            return View(resultado);
        }

        public IActionResult Detalle(int id)
        {
            var item = _items.FirstOrDefault(i => i.Id == id);
            return item == null ? NotFound() : View(item);
        }
        public IActionResult Agregar()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Agregar(Item item)
        {
            item.Id = _items.Count > 0 ? _items.Max(i => i.Id) + 1 : 1;
            // 3. LÓGICA DE IMAGEN EN MEMORIA (Sin guardar archivos)
            if (item.ArchivoPortada != null && item.ArchivoPortada.Length > 0)
            { 
                item.TipoImagen = item.ArchivoPortada.ContentType;

            using (var memoryStream = new MemoryStream())
            
                    
                    {
                        // Copiamos la foto a la memoria temporal
                        item.ArchivoPortada.CopyTo(memoryStream);

                        // Convertimos la foto a un arreglo de bytes
                        byte[] bytesImagen = memoryStream.ToArray();

                        // Convertimos los bytes a texto y lo guardamos en nuestro objeto
                        item.ImagenBase64 = Convert.ToBase64String(bytesImagen);
                    }
                }


            if (item.ArchivoCanciones != null && item.ArchivoCanciones.Length > 0)
            {
                item.TipoCanciones = item.ArchivoCanciones.ContentType;

                using (var memoryStreamTracklist = new MemoryStream())
                {
                    item.ArchivoCanciones.CopyTo(memoryStreamTracklist);
                    byte[] bytesTracklist = memoryStreamTracklist.ToArray();
                    item.CancionesListaBase64 = Convert.ToBase64String(bytesTracklist);
                }
            }

            _items.Add(item);
            return RedirectToAction("Index");

        }
    }
}
