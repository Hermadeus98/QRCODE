namespace QRTools
{
    public interface IPoolable
    {
        void OnPool();
        void OnPush();
    }
}