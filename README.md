# dotnet-snippets
Miscellaneous handy code for .net 

## OutputNUnit

Loads a locally stored NUnit test framework fomratted XML file and converts it to CSV. also saved locally. Includes a subset of available fields.

[NUnit test framework](https://nunit.org/)

### Example usage

`dotnet run bin/Debug/net8.0/OutputNUnit.dll Input/nunit-results.xml Output/nunit-results.txt` 