using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Posts.Api.Dtos;
using Shared.Events;

namespace Posts.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        public readonly IPublishEndpoint _publishEndpoint;

        private static List<PostsDto> posts = new()
        {
            // Posts para Juan Pérez (UserId = 1)
            new PostsDto { Id = 1, UserId = 1, Title = "Introducción a C#", Description = "Este post trata sobre cómo empezar a programar en C#." },
            new PostsDto { Id = 2, UserId = 1, Title = "Conceptos Avanzados de C#", Description = "Exploramos características avanzadas del lenguaje C#." },

            // Posts para María López (UserId = 2)
            new PostsDto { Id = 3, UserId = 2, Title = "Tendencias en Diseño Web", Description = "En este artículo, exploramos las últimas tendencias en diseño web para 2024." },
            new PostsDto { Id = 4, UserId = 2, Title = "UX/UI en el Diseño Web", Description = "Cómo la experiencia del usuario (UX) y la interfaz de usuario (UI) pueden mejorar tus sitios." },

            // Posts para Carlos García (UserId = 3)
            new PostsDto { Id = 5, UserId = 3, Title = "Desarrollo de APIs REST", Description = "Un tutorial sobre cómo construir una API RESTful utilizando ASP.NET Core." },
            new PostsDto { Id = 6, UserId = 3, Title = "Autenticación y Autorización en APIs", Description = "Exploramos cómo implementar seguridad en tus APIs usando JWT y OAuth." },

            // Posts para Ana Martínez (UserId = 4)
            new PostsDto { Id = 7, UserId = 4, Title = "Guía de SQL para Principiantes", Description = "Este post está destinado a quienes comienzan con bases de datos y SQL." },
            new PostsDto { Id = 8, UserId = 4, Title = "Optimización de Consultas SQL", Description = "Aprende cómo mejorar el rendimiento de tus consultas SQL más comunes." },

            // Posts para Pedro Rodríguez (UserId = 5)
            new PostsDto { Id = 9, UserId = 5, Title = "Patrones de Diseño en Programación", Description = "Exploramos algunos de los patrones de diseño más utilizados en programación." },
            new PostsDto { Id = 10, UserId = 5, Title = "Patrón Singleton y su Uso en C#", Description = "Aprende cómo implementar y cuándo usar el patrón Singleton en C#." }
        };

        public PostsController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        // POST: api/posts
        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] PostsDto post)
        {
            posts.Add(post);

            await _publishEndpoint.Publish<IPostCreatedProductor>(new
            {
                Message = "se creo un post oe"
            });

            return Ok();
        }

        // GET: api/posts
        [HttpGet]
        public ActionResult<List<PostsDto>> ListPosts()
        {
            return Ok(posts);
        }
    }
}
