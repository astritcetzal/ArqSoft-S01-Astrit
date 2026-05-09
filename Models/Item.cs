using Microsoft.AspNetCore.Http;
namespace Catalogo.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string tiposLanzamiento { get; set; }
        public int Ano  { get; set; }
        public int numeroCanciones { get; set; }
        public string Descripcion { get; set; }
        public IFormFile? ArchivoPortada { get; set; }
        public string? ImagenBase64 { get; set; }
        public string? TipoImagen { get; set; }

        public IFormFile? ArchivoCanciones { get; set; }
        public string? CancionesListaBase64{ get; set; }
        public string? TipoCanciones { get; set; }
    }
}
