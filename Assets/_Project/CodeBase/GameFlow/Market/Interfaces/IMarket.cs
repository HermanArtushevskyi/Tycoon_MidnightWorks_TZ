namespace _Project.CodeBase.GameFlow.Market.Interfaces
{
    public interface IMarket
    {
        public string[] GetSellableResources();
        public bool IsSellable(string resourceId);
        public int GetPrice(string resourceId);
        
        public bool TrySell(string resourceId, int amount);
    }
}