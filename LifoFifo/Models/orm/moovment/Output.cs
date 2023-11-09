using LifoFifo.Models.Exception;
using Microsoft.EntityFrameworkCore;

namespace LifoFifo.Models.orm.moovment;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Object;
using stock;

[Table("sortie")]
public class Output
{
    [Key]
    [Column("idsortie")]
    public string OutPutId{ get; set; }
    [Column("quantite")]
    public double Quantity{ get; set; }
    [Column("date_sortie")]
    public DateTime OutPutDate{ get; set; }

    [Column("idmagasin")] 
    public string MagasinId { get; set; }
    [Column("code")]
    public string ProductCode { get; set; }

    public void insert()
    {
        using (var ctx = ApplicationDbContextFactory.Create())
        {
            SequenceValueGetter sequenceValueGetter = new SequenceValueGetter();
            long value = sequenceValueGetter.GetNextSequenceValue("seq_sortie");
            Console.Out.WriteLine("keu="+value);
        //    this.OutPutId = "OUT" + ctx.Outputs.FromSqlRaw("select nextval('seq_sortie')").FirstOrDefault();
            this.OutPutId = "S" + value;
            ctx.Outputs.Add(this);
            ctx.SaveChanges();
        }
    }

    public void insertOutPut()
    {
        EtatDeStock etatDeStock = new EtatDeStock();
        int typeMouvement;
        Product product = new Product();
        product.Code = this.ProductCode;
        Console.WriteLine("Product code = "+product.Code);
        product = product.GetProductByCode();
        typeMouvement = product.Type; // index anl'ilay input lasa reste
        int index = this.CheckIfRepoNotBlank(etatDeStock);
        Input[] inputs = new Input().GetInputNotUsed(etatDeStock.Stocks[index].LastInputDate, "asc");
        
        VEtatStock vEtatStock = new VEtatStock
        {
            Code = product.Code,
            IdMagasin = this.MagasinId
        };
        VEtatStock[] etatStocks = vEtatStock.GetStockByProductAndShop();
        
        if (product.Type == 1) // lifo
        {
            etatStocks = etatStocks.OrderBy(item => item.DateEntree).ToArray();
        }
        else // fifo
        {
            etatStocks = etatStocks.OrderByDescending(item => item.DateEntree).ToArray();
        }
        // ici on doit trier inputs
        this.insert();
        new Moove().InsertMooveForOutPut(etatStocks, this);
    }
    
    public static Stock getMappingStockByProduct(Product product, EtatDeStock etatDeStock)
    {
        for (int i = 0; i < etatDeStock.Stocks.Length; i++)
        {
            if (etatDeStock.Stocks[i].ProductCode == product.Code)
            {
                return etatDeStock.Stocks[i];
            }
        }

        throw new StockException("Tsy misy ilay produit.");
    }
    
    public int CheckIfRepoNotBlank(EtatDeStock etatDeStock)
    {
        //EtatDeStock etatDeStock = new EtatDeStock();
        for (int i = 0; i < etatDeStock.Stocks.Length; i++)
        {
            if (etatDeStock.Stocks[i].ProductCode == this.ProductCode & etatDeStock.Stocks[i].Rest >= this.Quantity)
            {
                return i;
            }
        }
        throw new StockException("Stock vide.");
    }
    public Output[] GetOutputByTwoDatesProductAndShop(DateTime date1, DateTime date2)
    {
        using (var ctx = ApplicationDbContextFactory.Create())
        {
            return ctx.Outputs.Where(o => o.OutPutDate >= date1 
                                          & o.OutPutDate <= date2
                                          & o.ProductCode == this.ProductCode
                                          & o.MagasinId == this.MagasinId
                                          ).ToArray();
        }
    }

    public static void Main(string[] args)
    {
        Console.Out.WriteLine("h");
    }
}