namespace Users.Api.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public List<PostsDto> Posts { get; set; } = new List<PostsDto>();
    }
}
