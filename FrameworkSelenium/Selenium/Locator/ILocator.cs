    public interface ILocator
    {
        By ToBy();
        LocatorType Type { get; }
    }
