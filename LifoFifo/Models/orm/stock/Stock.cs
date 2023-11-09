using LifoFifo.Models.orm.moovment;

namespace LifoFifo.Models.orm.stock;

public class Stock
{
    public string ProductCode{ get; set;}
    public string ShopId{ get; set;}
    public double InitialQuantity{ get; set;}
    public double Rest{ get; set;}
    public double Rest2 { get; set; }
    public double UnitairPrice{ get; set;}
    public double TotalPrice{ get; set;}
    public DateTime LastInputDate { get; set; }
    public double Somme { get; set; }
    public void buildStock(DateTime date1, DateTime date2)
    {
        Input input = new Input
        { ProductCode = this.ProductCode, shopId = this.ShopId }; // ici on ajoute l'id du magasin avec l'id du produit
        Input[] inputs = input.GetInputByTwoDatesWithProductAndShop(DateTime.MinValue, date2); // obtenir tous les entrées de ce produit dans les dates donnees
        double inputTotal = input.GetSumQuantityInput(inputs, date1); // ici on prends la quantité initiale avant tout mouvement
        Console.WriteLine("Tot = "+inputTotal);
        Output output = new Output{ ProductCode = this.ProductCode, MagasinId = this.ShopId }; // meme cas ligne 18
        Output[] outputs = output.GetOutputByTwoDatesProductAndShop(DateTime.MinValue, date2); // obtenir les declarations de sortie de toutes les sorties
        Moove[] moovesTotal = new Moove().GetMooveByOutPuts(outputs); // obtention de chaque mouvement au cour de la sortie
        
        this.SetOutMooveFromInput(inputs, moovesTotal); // ici on essais d"enlever les produits sortant des produits initiaux
        
        double outputTotal = new Moove().GetSumQuantityMooves(moovesTotal); // ici on calcul la somme total des sorties
        this.Rest = inputTotal - outputTotal; // ici on peut calculer le reste
        Console.WriteLine("Reste "+this.Rest+ " = "+inputTotal + "&" + outputTotal);
        this.Rest2 = this.CalculateRest(inputs)[0]; // ici on calcul le reste par rapport aux inputs pour verifier si le calcul est bon
        this.TotalPrice = this.CalculateRest(inputs)[1]; // this.CalculateTotalPriceOfRest(inputs, date1); // ici on calcul le prix total des restes 
        this.UnitairPrice = this.TotalPrice / this.Rest2;
        this.LastInputDate = this.GetLastMouvementDate(inputs);

        Input[] secondInput = input.GetInputByTwoDatesWithProductAndShop(DateTime.MinValue, date1);
        Output[] secondOutput = output.GetOutputByTwoDatesProductAndShop(DateTime.MinValue, date1);
        Moove[] secondMooves = new Moove().GetMooveByOutPuts(secondOutput);
        this.SetOutMooveFromInput(secondInput, secondMooves);
        this.InitialQuantity = this.CalculateRest(secondInput)[0];
    }

    public DateTime GetLastMouvementDate(Input[] inputs)
    {
        DateTime max = DateTime.MinValue;
        for (int i = 0; i < inputs.Length; i++)
        {
            if (inputs[i].MoovementDate > max)
            {
                max = inputs[i].MoovementDate;
            }
        }

        return max;
    }
    public double[] CalculateRest(Input[] inputs)
    {
        double[] retour = new double[2];
        double TotalPrice = 0;
        double Sum = 0;
        for (int i = 0; i < inputs.Length; i++)
        {
            TotalPrice += inputs[i].Quantity;
            Sum += inputs[i].Quantity * inputs[i].Price;
        }

        retour[0] = TotalPrice;
        retour[1] = Sum;
        return retour;
    }
    public double CalculateTotalPriceOfRest(Input[] inputs, DateTime date)
    {
        double TotalPrice = 0;
        for (int i = 0; i < inputs.Length; i++)
        {
             TotalPrice += inputs[i].Quantity * inputs[i].Price;
        }

        return TotalPrice;
    }

    public void SetOutMooveFromInput(Input[] inputs, Moove[] moovesTotal)
    {
        double SumInitialQuantity = 0;
        for (int i = 0; i < inputs.Length; i++)
        {
            for (int j = 0; j < moovesTotal.Length; j++)
            {
                if (inputs[i].InputId == moovesTotal[j].InPutId)
                {
                    //SumInitialQuantity = ;
                    SumInitialQuantity += inputs[i].Quantity - moovesTotal[j].Quantity;
                    Console.WriteLine("Input - Output "+ (inputs[i].Quantity - moovesTotal[j].Quantity));
                    inputs[i].Quantity = inputs[i].Quantity - moovesTotal[j].Quantity;
                    if (inputs[i].Quantity == 0)
                    {
                        inputs[i].SetInputToEpuised();
                    }
                }
            }
        }
    }
}