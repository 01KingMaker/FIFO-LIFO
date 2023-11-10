using LifoFifo.Models.orm.infrastructure;
using LifoFifo.Models.orm.Object;

namespace LifoFifo.Models.orm.stock;


public class EtatDeStock
{
    public DateTime Date1 { get; set; }
    public DateTime Date2 { get; set; }
    public string productId { get; set; }
    public string shopId { get; set; }
    public Stock[] Stocks { get; set; }

    public EtatDeStock()
    {
        this.Date1 = DateTime.MinValue;
        this.Date2 = DateTime.MaxValue;
        this.productId = "%";
        this.shopId = "%";
        this.SetStocks();
    }
    public void SetStocks()
    {
        List<Stock> stocks = new List<Stock>();
        Product[] products = new Product().SearchProduct();
        Shop[] shops = new Shop().GetShop();
        
        for (int i = 0; i < products.Length; i++)
        {
            for (int j = 0; j < shops.Length; j++)
            {
                Stock stock = new Stock
                {
                    ShopId = shops[j].ShopId, ProductCode = products[i].Code
                };
                stock.buildStock(this.Date1, this.Date2);
                stocks.Add(stock);
            }
        }

        this.Stocks = stocks.ToArray();
    }
}