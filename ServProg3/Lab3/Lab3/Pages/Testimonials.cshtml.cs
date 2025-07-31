using Lab3.Data;
using Lab3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab3.Pages
{
    public class TestimonialsModel : PageModel
    {
        private readonly AppDbContext _context;

        public TestimonialsModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Testimonial Testimonial { get; set; }
        public List<Testimonial> Testimonials { get; set; }

        public async Task OnGetAsync()
        {
            Testimonials = await _context.Testimonials.ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Testimonials = await _context.Testimonials.ToListAsync();
                return Page();
            }

            _context.Testimonials.Add(Testimonial);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }
    }
}
