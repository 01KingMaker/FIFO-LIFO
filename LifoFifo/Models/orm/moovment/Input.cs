using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LifoFifo.Models.orm.individu;

namespace LifoFifo.Models.orm.moovment;

[Table("entree")]
public class Input
{
    [Key]
    [Column("identree")]
    public string InputId{ get; set;}
    [Column("code")]
    public string ProductCode{ get; set;}
    [Column("idmagasin")]
    [ForeignKey("id_magasin")]
    public string shopId{ get; set;}
    [Column("quantite")]
    public double Quantity{ get; set;}
    [Column("prix_unitaire")]
    public double Price{ get; set;}
    [Column("date_entree")]
    public DateTime MoovementDate{ get; set;}
    [Column("etat")]
    public int State { get; set; } // 1 etat non épuisé, 0 etat épuisé

    public void SetInputToEpuised()
    {
        using (var ctx = ApplicationDbContextFactory.Create())
        {
            Input input = ctx.Inputs.Where(i => i.InputId == this.InputId).First();
            input.State = 0;
            ctx.Inputs.Update(input);
            ctx.SaveChanges();
        }
    }
    public Input[] GetInputNotUsed(DateTime minDate, string sort)
    {
        using (var ctx = ApplicationDbContextFactory.Create())
        {
            return ctx.Inputs.Where(i => i.MoovementDate >= minDate & i.State == 1).OrderBy(i => i.MoovementDate).ToArray();
        }
    }
    
    public Input[] GetInputByPriorityAndProduct()
    {
        string fifo = "fifo";
        string lifo = "lifo";
        // eto miantso methode maka etat de stock anazy rehetra
        throw new NotImplementedException("In developpement");
    }

    public Input[] GetInputByTwoDatesWithProductAndShop(DateTime date1, DateTime date2)
    {
        using (var ctx = ApplicationDbContextFactory.Create())
        {
            return ctx.Inputs.Where(
                i => i.MoovementDate >= date1 
                 & i.MoovementDate <= date2
                 & i.ProductCode == this.ProductCode
                 & i.shopId == this.shopId
            ).ToArray();
        }
    }

    public double GetSumQuantityInput(Input[] inputs, DateTime date)
    {
        double sum = 0;
        for (int i = 0; i < inputs.Length; i++)
        {
            /*if(inputs[i].MoovementDate <= date)*/ sum += inputs[i].Quantity;
        }
        return sum;
    }

    public double GetPriceOfTotalInput(Input[] inputs)
    {
        double sum = 0;
        for (int i = 0; i < inputs.Length; i++)
        {
            sum += inputs[i].Quantity * inputs[i].Price;
        }
        return sum;
    }
}