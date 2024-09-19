namespace APICatalog.Services
{
    public class MyService : IMyService
    {
        public string Saudacao(string name)
        {
           return $"Welcome, {name} \n\n {DateTime.UtcNow}";
        }
    }
}
