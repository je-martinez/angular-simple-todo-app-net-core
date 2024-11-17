namespace simple_todo_bll.Auth.DTOs
{
    public class JwtConfigDto
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
    }
}