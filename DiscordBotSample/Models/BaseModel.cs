namespace DiscordBotSample.Models
{
    public class BaseModel
    {
        public UnitTime TimeUnit { get; set; }

        public enum UnitTime : int { H1 = 1, H2 = 2, H4 = 3, H12 = 4, Daily = 5, Weekly = 6, Monthly = 7 };
    }
}
