using Lab3.Models;
using Lab3.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab3.Pages
{
    public class PortfolioModel : PageModel
    {
        public PortfolioService portfolioItemsService;
        public IEnumerable<PortfolioItem> PortfolioItems { get; private set; }
        public PortfolioModel(PortfolioService itemsService)
        {
            portfolioItemsService = itemsService;
        }
        public void OnGet()
        {
            PortfolioItems = portfolioItemsService.GetPortfolioItems();
        }
    }
}
