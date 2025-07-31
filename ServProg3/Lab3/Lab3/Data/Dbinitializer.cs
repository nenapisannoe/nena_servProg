using Lab3.Models;

namespace Lab3.Data
{
    public class Dbinitializer
    {
        public static void Initialize(AppDbContext context)
        {
       

            Testimonial[] testimonials = new Testimonial[] {
                new Testimonial
                {
                    Map = "Map",
                    Score = "Score",
                    POTG = "POTG"
                },
                new Testimonial
                {
                    Map = "Map",
                    Score = "Score",
                    POTG = "POTG"
                },
                new Testimonial
                {
                    Map = "Map",
                    Score = "Score",
                    POTG = "POTG"
                }
            };

            context.Testimonials.AddRange(testimonials);
            context.SaveChanges();
        }
    }
}
