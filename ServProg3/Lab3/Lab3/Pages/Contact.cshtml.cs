using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CsvHelper;
using System.Globalization;
using System.IO;

namespace Lab3.Pages
{
    public class ContactModel : PageModel
    {
        [BindProperty]
        public ContactForm Contact { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                SaveToCsv(Contact);
                return RedirectToPage("Thank-you");
            }

            return Page();
        }

        private void SaveToCsv(ContactForm contact)
        {
            var filePath = "contacts.csv";
            var append = System.IO.File.Exists(filePath);

            using (var writer = new StreamWriter(filePath, append))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                if (!append)
                {
                    csv.WriteHeader<ContactForm>();
                    csv.NextRecord();
                }

                csv.WriteRecord(contact);
                csv.NextRecord();
            }
        }

        public class ContactForm
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public string Message { get; set; }
        }
    }
}
