using LifoFifo.Models.orm.stock;

namespace LifoFifo.Controllers.State;

using Microsoft.AspNetCore.Mvc;

public class StateController : Controller
{
    public IActionResult StockState(EtatDeStock stockState)
    {
        if (ModelState.IsValid)
        {
            stockState.SetStocks();
        }
        else
        {
            stockState = new EtatDeStock();
        }
        ViewData["stockState"] = stockState;
        ViewData["Title"] = "Etat de stock";
        return View();
    }
}