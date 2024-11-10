using ApiGateway.Dtos;
using Newtonsoft.Json;
using Ocelot.Middleware;
using Ocelot.Multiplexer;
using System.Net;
using System.Net.Http.Headers;

namespace ApiGateway.Aggregators
{
    public class UserPostsAggregator : IDefinedAggregator
    {
        public async Task<DownstreamResponse> Aggregate(List<HttpContext> responseHttpContexts)
        {
            // El agregador recibe una lista de respuestas downstream como parámetro.
            // Ahora puedes implementar tu propia lógica para agregar las respuestas (incluidos los cuerpos y encabezados) de los servicios downstream.
            var responses = responseHttpContexts.Select(x => x.Items.DownstreamResponse()).ToArray();

            // Este es solo un ejemplo de lo que puedes hacer.
            var userResponse = await responses[0].Content.ReadAsStringAsync();
            var postsResponse = await responses[1].Content.ReadAsStringAsync();

            var users = JsonConvert.DeserializeObject<List<UserDto>>(userResponse) ?? [];
            var posts = JsonConvert.DeserializeObject<List<PostsDto>>(postsResponse) ?? [];

            foreach (var user in users)
            {
                user.Posts = posts.Where(x => x.UserId == user.Id).ToList();
            }

            var aggregatorResponse = new StringContent(JsonConvert.SerializeObject(users))
            {
                Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
            };

            // La única restricción aquí: Debes devolver un objeto DownstreamResponse.
            return new DownstreamResponse(
                aggregatorResponse,
                HttpStatusCode.OK,
                aggregatorResponse.Headers,
                "Ok");
        }
    }
}
