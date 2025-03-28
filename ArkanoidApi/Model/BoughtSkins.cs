namespace ArkanoidApi.Model
{
    public class BoughtSkins
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SkinId { get; set; }

        public User User { get; set; }
        public BallSkin BallSkin { get; set; }
    }
}
