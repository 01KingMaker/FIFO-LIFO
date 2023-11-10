using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LifoFifo.Models.orm.moovment;

[Keyless]
[Table("v_etat_stock")]
public class VEtatStock
{
    [Column("identree")]public string IdEntree{ get; set;}
    [Column("date_entree")]public DateTime DateEntree{ get; set;}
    [Column("quantite")]public double Quantite{ get; set;}
    [Column("prix_unitaire")]public double PrixUnitaire{ get; set;}
    [Column("idmagasin")]public string IdMagasin{ get; set;}
    [Column("code")]public string Code{ get; set;}

    public VEtatStock[] GetStockByProductAndShop()
    {
        using (var ctx = ApplicationDbContextFactory.Create())
        {
            return ctx.VEtatStocks.Where(
                ve => ve.Code == this.Code & ve.IdMagasin == this.IdMagasin
            ).ToArray();
        }
    }
}