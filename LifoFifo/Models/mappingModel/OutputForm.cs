using System.ComponentModel.DataAnnotations;
using LifoFifo.Models.orm.moovment;

namespace LifoFifo.Models.mappingModel;

public class OutputForm
{
    public void insert()
    {
    }
    public OutputForm(string shop, string product, double quantity, DateTime outputDate)
    {
        Shop = shop;
        Product = product;
        Quantite = quantity;
        OutputDate = outputDate;/*
    public double Quantity{ get; set; }
    [Column("date_sortie")]
    public DateTime OutPutDate{ get; set; }

    [Column("idmagasin")] 
    public string MagasinId { get; set; }
    [Column("code")]
    public string ProductCode { get; set; }*/
        Output output = new Output
        {
            Quantity = quantity,
            OutPutDate = outputDate,
            MagasinId = shop,
            ProductCode = product
        };
    }

    public void CreateMoovment()
    {
        Output output = new Output
        {
            Quantity = this.Quantite,
            OutPutDate = this.OutputDate,
            MagasinId = this.Shop,
            ProductCode = this.Product
        };
        Console.WriteLine("Code = "+output.ProductCode);
        output.insertOutPut();
    }
    public OutputForm()
    {
    }
    [Required(ErrorMessage = "Shop is required")]
    public string Shop
    {
        get;
        set;
    }
    [Required(ErrorMessage = "Product is required")]
    public string Product{ get; set; }
    [Required]
    public double Quantite{ get; set; }
    [Required]
    public DateTime OutputDate{ get; set; }
}
