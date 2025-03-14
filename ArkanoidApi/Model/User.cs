namespace ArkanoidApi.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int Coins { get; set; } = 0;
    }
}
