using System.Text;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;

namespace PdfToText.Console.Services
{
    public static class PdfToTextConverter
    {
        public static void ConvertPdfToText()
        {
            System.Console.Write("Enter the path to the PDF file: ");
            string pdfFilePath = System.Console.ReadLine();

            if (File.Exists(pdfFilePath))
            {
                string text = PdfToTextConverter.ExtractTextFromPdf(pdfFilePath);

                // Create a text file with the same name as the PDF but with a .txt extension
                string textFilePath = Path.ChangeExtension(pdfFilePath, "txt");

                File.WriteAllText(textFilePath, text);

                System.Console.WriteLine($"Text extracted and saved to: {textFilePath}");
            }
            else
            {
                System.Console.WriteLine("File not found. Please provide a valid path.");
            }
        }
        public static string ExtractTextFromPdf(string pdfFilePath)
        {
            using (PdfDocument document = PdfDocument.Open(pdfFilePath))
            {
                StringBuilder resultText = new StringBuilder();

                foreach (Page page in document.GetPages())
                {
                    string pageText = page.Text;

                    resultText.Append(pageText);

                    foreach (Word word in page.GetWords())
                    {
                        System.Console.WriteLine(word.Text);
                    }
                }

                return resultText.ToString();
            }
        }
    }
}
