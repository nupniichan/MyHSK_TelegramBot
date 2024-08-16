using MyHSK;

class Program
{
    static async Task Main(string[] args)
    {
        var bot = new MyHSK_bot();
        await bot.Run();
    }
}