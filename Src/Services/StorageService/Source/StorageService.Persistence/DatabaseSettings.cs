namespace StorageService.Persistence
{
    public class DatabaseSettings
    {
        public bool MigrationEnabled { get; set; } = false;
        public string ConnectionString { get; set; }
    }
}
