using Lab3.Data;
using Lab3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab3.Pages
{
    public class TestimonialForm
    {
        public string? Status { get; set; } = default;
        public string Name { get; set; }
        public string? Position { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }

        public string Author { get; set; }
    }

    public class TestimonialsModel : PageModel
    {
        [BindProperty]
        public  TestimonialForm Form { get; set; }
        private readonly AppDbContext _context;
        public List<Testimonial> Testimonials { get; private set; }
        public TestimonialsModel(AppDbContext context)
        {
            _context = context;
            LoadTestimonials();
        }

        public IActionResult? OnPost()
        {
            if (Form.Name == null)
            {
                Form.Status = "Attention! You must enter your name.";
                return null;
            }

            if (Form.Title == null)
            {
                Form.Status = "Attention! Please, enter a title.";
                return null;
            }

            if (Form.Text == null)
            {
                Form.Status = "Attention! Please, enter your message.";
                return null;
            }


            Testimonial testimonial = new()
            {
                Review = Form.Title,
                Comment = Form.Text,
                Author = Form.Author
            };

            _context.Testimonials.Add(testimonial);
            _context.SaveChanges();

            return Redirect("/Testimonials");
        }


        async void LoadTestimonials()
        {
            Testimonials = await _context.Testimonials
                .ToListAsync();
        }
    }
}


