namespace _Project.CodeBase.Services.Saving.Interfaces
{
    public interface ISaver
    {
        public bool Save<T>(object data, string saveName = null);
    }
}