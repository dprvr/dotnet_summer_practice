namespace FilesStorage.Entities.Entities
{
    public class FileAndTag
    {
        public int Id { get; set; }
        public virtual StorageTag Tag { get; set; }
        public virtual StorageFile File { get; set; }
    }
}
