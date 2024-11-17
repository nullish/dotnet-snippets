using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OutputNUnit
{
    public class NUnitConvertXML
    {
        public void ConvertCSV(string pathInput, string pathOutput)
        {
            XElement xmlIn = XElement.Load(pathInput);
            XElement xmlFiltered = new XElement("xml-filtered");
            xmlFiltered.Add(xmlIn.Descendants("test-case"));

            string header = "Name|Full name|Start time|Result|Category" + Environment.NewLine;

            string csv =
                (from el in xmlFiltered.Elements("test-case")
                 select
                     string.Format("{0}|{1}|{2}|{3}|{4}|{5}",
                         (string)el.Attribute("name"),
                         (string)el.Attribute("fullname"),
                         (string)el.Attribute("start-time"),
                         (string)el.Attribute("result"),
                         (string)el.Element("properties").Element("property").Attribute("value"),
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
