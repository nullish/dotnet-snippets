/// <summary Load a locally stored XML file and convert it to CSV also saved locally 
/// https://learn.microsoft.com/en-us/dotnet/standard/linq/generate-text-files-xml 
/// </summary>

using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace OutputNUnit;

internal class Program
{
    private static void Main(string[] args)
    {
        // Get first two command line arguments and check for empty strings
        if (args.Length < 3) {
            Console.WriteLine("Please supply command line paramaters for a relative Input path and Output path.");
            Environment.Exit(1);
        }

        PathManager _pathManager = new PathManager();
        String pathInput = _pathManager.MakeRelativePath(args[1]);
        String pathOutput = _pathManager.MakeRelativePath(args[2]);

        NUnitConvertXML _nUnitConvertXML = new NUnitConvertXML();
        _nUnitConvertXML.ConvertCSV(pathInput, pathOutput);
    }
}

