using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OutputNUnit
{
    public class NUnitConvertXML
    {
        private string MultiToSingleLine(string inputString) {
           // Receive a multi line string and remove new line chars. Trim and remove surplus characters.
            string outputString = Regex.Replace(inputString, @"\n", " ");
            outputString = Regex.Replace(outputString, @"-{2,}\^", "");
            outputString = outputString.Trim();
            return outputString;
        }
        public void ConvertCSV(string pathInput, string pathOutput)
        /// <summary>Convert NUnit format XML into CSV, selected fields only </summary>
        /// <param name="pathInput">Relative path for input file</param>
        /// <param name="pathOutput">Relative path for output</param> 
        {
            XElement xmlIn = XElement.Load(pathInput);
            XElement xmlFiltered = new XElement("xml-filtered");
            xmlFiltered.Add(xmlIn.Descendants("test-case"));

            string header = "Name|Full name|Category|Start time|Result" + Environment.NewLine;

            string csv =
                (from el in xmlFiltered.Elements("test-case")
                 select
                     string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}",
                         (string)el.Attribute("name"),
                         (string)el.Attribute("fullname"),
                         (string)el.Element("properties").Elements("property")?.FirstOrDefault(p => p.Attribute("name")?.Value == "Category")?.Attribute("value") ?? "-",
                         (string)el.Attribute("start-time"),
                         (string)el.Attribute("result"),
                         MultiToSingleLine((string)el?.Element("failure")?.Element("message") ?? "-"),
                         Environment.NewLine
                     )
                )
                .Aggregate(
                    new StringBuilder(),
                    (sb, s) => sb.Append(s),
                    sb => sb.ToString()
                );

            File.WriteAllText(pathOutput, header);
            File.AppendAllText(pathOutput, csv);
        }
    }
}
