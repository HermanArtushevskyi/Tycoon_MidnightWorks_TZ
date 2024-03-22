namespace _Project.CodeBase.Services.Saving.Interfaces
{
    public interface ILoader
    {
        public bool Load<T>(out T result, string saveName = null) where T : class;
    }
}