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
                    Review = "Loved it",
                    Comment = "Really did",
                    Author = "Hopefully you"
                },
                new Testimonial
                {
                    Review = "Great game",
                    Comment = "The only thing better is the Necrofarm",
                    Author = "Also hopefully you"
                },
                new Testimonial
                {
                    Review = "Great soup",
                    Comment = "Best soup ever",
                    Author = "Hopefully me after this"
                }
            };

            context.Testimonials.AddRange(testimonials);
            context.SaveChanges();
        }
    }
}
