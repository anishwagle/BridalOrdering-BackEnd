namespace BridalOrdering.Helpers
{
   public interface IAppSettings
{
    string DatabaseName { get; set; }
    string ConnectionString { get; set; }
    public string Secret { get; set; }
}

public class AppSettings : IAppSettings
{
    public string DatabaseName { get; set; }
    public string ConnectionString { get; set; }
    public string Secret { get; set; }
}
}