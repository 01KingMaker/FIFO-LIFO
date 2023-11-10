using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LifoFifo.Models.orm.Object;

[Table("article")]
public class Product
{
    [Key]
    [Column("code")] 
    public string Code { get; set; }
    [Column("idarticle")] 
    public string ProductId{ get; set;}
    [Column("nom")]
    public string ProductName{ get; set;}
    [Column("type")]
    public int Type { get; set; }
    [Column("unite")]
    public string Unite { get; set; }
    
    public Product GetProductById()
    {
        throw new NotImplementedException("Function no implemented right now");
    }

    public Product[] SearchProduct()
    {
        using (var ctx = ApplicationDbContextFactory.Create())
        {
            return ctx.Products./*Where(p => p.Code == this.Code).*/ToArray();
        }
    }

    public Product GetProductByCode()
    {
        using (var ctx = ApplicationDbContextFactory.Create())
        {
            return ctx.Products.Where(p => p.Code == this.Code).First();
        }
    }
}