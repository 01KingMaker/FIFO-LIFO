using System.Runtime.InteropServices.JavaScript;
using LifoFifo.Models.Exception;
using LifoFifo.Models.mappingModel;
using Microsoft.AspNetCore.Mvc;

namespace LifoFifo.Controllers.Moovment;

public class MoovmentController : Controller
{
    // GET
    public IActionResult OutPut(OutputForm outputForm)
    {
        if (ModelState.IsValid)
        {
            try
            {
                outputForm.CreateMoovment();
            }
            catch (StockException stock)
            {
                Console.Out.WriteLine(stock.Message);
                HomeController p = new HomeController();
                return p.Error(stock.Message);
            }
        }
        else
        {
            outputForm = new OutputForm();
        }
        ViewData["_outputForm"] = outputForm;
        ViewData["Title"] = "Mouvement de sortie";
        return View();
    }

    public static void Main(string[] args)
    {
        
    }
}