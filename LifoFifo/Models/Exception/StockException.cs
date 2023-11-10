namespace LifoFifo.Models.Exception;

public class StockException : InvalidOperationException
{
    public StockException(string message) : base(message) { }
}