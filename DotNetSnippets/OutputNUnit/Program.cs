/// <summary> Load a locally stored XML file and convert it to CSV also saved locally. Uses subset of available fields.
/// https://learn.microsoft.com/en-us/dotnet/standard/linq/generate-text-files-xml 
/// </summary>

using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using CommandLine;

namespace OutputNUnit;

internal class Options
{
    [Option('i', "input", Required = true, HelpText = "Input XML file to be processed.")]
    public String InputFile { get; set; }

    [Option('o', "output", Required = true, HelpText = "Output txt file to be processed.")]
    public String OutFile { get; set; }
}
internal class Program
{
    static void Main(string[] args)
    {
        CommandLine.Parser.Default.ParseArguments<Options>(args)
          .WithParsed(RunOptions)
          .WithNotParsed(HandleParseError);
    }
    static void RunOptions(Options opts)
    {
        // Construct absolute path for input and output files based on user supplied relative paths
        
        PathManager _pathManager = new PathManager();
        String pathInput = _pathManager.MakeRelativePath(opts.InputFile);
        String pathOutput = _pathManager.MakeRelativePath(opts.OutFile);

        NUnitConvertXML _nUnitConvertXML = new NUnitConvertXML();
        _nUnitConvertXML.ConvertCSV(pathInput, pathOutput);
    }
    static void HandleParseError(IEnumerable<Error> errs)
    {
        Console.WriteLine(errs);
    }
}

