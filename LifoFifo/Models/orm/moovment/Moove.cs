using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LifoFifo.Models.orm.moovment;

[Table("mouvement")]
public class Moove
{
    [Key]
    [Column("idmouvement")] 
    public int IdMouvement { get; set; }
    [Column("idsortie")]
    public string OutPudId { get; set; }
    [Column("identree")]
    public string InPutId { get; set; }
    [Column("quantite")]
    public double Quantity { get; set; }

    public void InsertMoove()
    {
        using (var ctx = ApplicationDbContextFactory.Create())
        {
            ctx.Mooves.Add(this);
            ctx.SaveChanges();
        }
    }

    public static void printVStock(VEtatStock[] vEtatStocks)
    {
        for (int i = 0; i < vEtatStocks.Length; i++)
        {
            Console.WriteLine("etat de stock = "+vEtatStocks[i].Quantite);
        }
    }
    public void InsertMooveForOutPut(VEtatStock[] inputs, Output output)
    {
        printVStock(inputs);
        double Quantity = output.Quantity; // argent à déboursser
        int i = 0;
        while (Quantity > 0)
        {
            double quantiteASortir= 0;
            double temp = inputs[i].Quantite - Quantity;
            if (temp >= 0)
            { // quantite à sortir inférieur à la quantité initiale
                quantiteASortir = Quantity; // 
                Quantity = 0;
            }
            else
            { // quantité à sortir insuffisant pour cet entrée
                quantiteASortir = (temp) + Quantity;
                Quantity = Quantity - quantiteASortir;
            }
            Moove moove = new Moove
                { 
                    InPutId = inputs[i].IdEntree, 
                    OutPudId = output.OutPutId,
                    Quantity = quantiteASortir }
                ;
            moove.InsertMoove();
            i += 1;
        }
    }
    public Moove[] GetMooveByOutPuts(Output[] outputs)
    {
        List<Moove[]> mooves = new List<Moove[]>();
        using (var ctx = ApplicationDbContextFactory.Create())
        {
            for (int i = 0; i < outputs.Length; i++)
            {
                mooves.Add(ctx.Mooves.Where(m => m.OutPudId == outputs[i].OutPutId).ToArray());   
            }
        }
        return TwoDimensionsMoovesToOne(mooves.ToArray());
    }

    public Moove[] TwoDimensionsMoovesToOne(Moove[][] mooves)
    {
        List<Moove> retour = new List<Moove>();
        int i, j;//mooves.Length;
        //mooves[0].Length;
        for (i = 0; i < mooves.Length; i++)
        {
            for (j = 0; j < mooves[i].Length; j++)
            {
                retour.Add(mooves[i][j]);
            }  
        }
        return retour.ToArray();
    }

    public double GetSumQuantityMooves(Moove[] mooves)
    {
        double sum = 0;
        for (int i = 0; i < mooves.Length; i++)
        {
            sum += mooves[i].Quantity;
        }

        return sum;
    }
}