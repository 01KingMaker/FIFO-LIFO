using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LifoFifo.Models.orm.infrastructure;

[Table("magazin")]
public class Shop
{
    [Key]
    [Column("idmagasin")]
    public string ShopId{ get; set;}
    [Column("nom")]
    public string ShopName{ get; set; }

    public Shop[] GetShop()
    {
        using (var ctx = ApplicationDbContextFactory.Create())
        {
            return ctx.Shops.ToArray();
        }
    }
}