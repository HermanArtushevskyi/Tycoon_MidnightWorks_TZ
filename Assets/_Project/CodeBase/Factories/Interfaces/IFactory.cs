namespace _Project.CodeBase.Factories.Interfaces
{
    public interface IFactory<T>
    {
        public T Create();
    }
    
    public interface IFactory<T, TArg>
    {
        public T Create(TArg arg);
    }
    
    public interface IFactory<T, TArg1, TArg2>
    {
        public T Create(TArg1 arg1, TArg2 arg2);
    }
    
    public interface IFactory<T, TArg1, TArg2, TArg3>
    {
        public T Create(TArg1 arg1, TArg2 arg2, TArg3 arg3);
    }
    
    public interface IFactory<T, TArg1, TArg2, TArg3, TArg4>
    {
        public T Create(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4);
    }
}