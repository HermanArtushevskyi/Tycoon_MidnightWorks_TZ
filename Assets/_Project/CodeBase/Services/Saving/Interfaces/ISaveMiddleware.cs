namespace _Project.CodeBase.Services.Saving.Interfaces
{
    public interface ISaveMiddleware
    {
        public void Save(object data, string saveName);
    }
}