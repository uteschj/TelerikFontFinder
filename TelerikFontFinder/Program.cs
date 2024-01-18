using System.IO;
using System.IO.Compression;
using System.Xml.Linq;
using System.Linq;

namespace TelerikFontFinder
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the source directory to search:");
            string sourceDirectory = Console.ReadLine();

            // Check if the directory exists
            if (!Directory.Exists(sourceDirectory))
            {
                Console.WriteLine("Directory does not exist.");
                return;
            }

            Console.WriteLine("Enter the font to search:");
            string fontName = Console.ReadLine();
            fontName = fontName.Trim();
            if (string.IsNullOrWhiteSpace(fontName))
            {
                Console.WriteLine("Font name cannot be empty.");
                return;
            }

            // Find all .trdp files in the directory
            string[] trdpFiles = Directory.GetFiles(sourceDirectory, "*.trdp");

            // Check if there are any .trdp files
            if (trdpFiles.Length == 0)
            {
                Console.WriteLine("No .trdp files found.");
                return;
            }

            Directory.CreateDirectory("TelerikFontFinder");

            foreach (string file in trdpFiles)
            {
                ProcessFile(file, fontName);
            }

            //Clean up
            Directory.Delete("TelerikFontFinder", true);

            Console.WriteLine("Processing complete.");
        }

        private static void ProcessFile(string file, string fontName)
        {
            try
            {
                string directoryName = Path.Combine(Path.GetFileNameWithoutExtension(file));
                string destinationDirectory = Path.Combine("TelerikFontFinder", directoryName);
                Directory.CreateDirectory(destinationDirectory);

                // Unzip the file into the new directory
                ZipFile.ExtractToDirectory(file, destinationDirectory, true);

                string definitionFilePath = Path.Combine(destinationDirectory, "definition.xml");
                if (File.Exists(definitionFilePath))
                {
                    XDocument xmlDoc = XDocument.Load(definitionFilePath);
                    
                    var xmlns = xmlDoc.Root.GetDefaultNamespace();

                    var fontElements = xmlDoc.Descendants(xmlns + "Font")
                                                .Where(x => (string)x.Attribute("Name") == fontName) //"Courier New"
                                                .ToList();
                    Console.WriteLine($"Report: {directoryName}");
                    foreach (var element in fontElements)
                    {
                        var s = element.Parent.Parent.Attribute("Name").ToString();
                        int start = s.IndexOf("\"") + 1;
                        int end = s.LastIndexOf("\"");
                        string elementName = s.Substring(start, end - start);
                        Console.WriteLine($"      Found Font in element: {elementName}");
                    }
                }
                else
                {
                    Console.WriteLine($"definition.xml not found in {directoryName}");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing {file}: {ex.Message}");
            }
        }
    }
}
