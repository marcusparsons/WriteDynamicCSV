/// <summary>
/// Writes a double quote qualified CSV file from an IEnumerable<dynamic> list. This is the default return type when querying when using the Dapper library.
/// </summary>
/// <param name="dataList">A list of dynamic data returned from dapper</param>
/// <param name="path">The path (including filename) to write the file to</param>
/// <param name="delim">The delimiter to use in between each individual field. Defaults to a comma ,</param>
/// <returns></returns>
public void WriteDynamicCSV(IEnumerable<dynamic> dataList, string path, string delim = ",")
{
	using (TextWriter tw = System.IO.File.CreateText(path))
	{
		//header set flag
		bool headerSet = false;

		foreach (var rowDynamic in dataList)
		{
			//Dapper implements IEnumerable<dynamic> as an IDictionary<string, object>
			var row = rowDynamic as IDictionary<string, object>;

			//If the header hasn't been set yet
			if (headerSet == false)
			{
				//Write the keys (header) of the CSV
				tw.WriteLine(string.Join(delim, row.Keys.ToArray().Select(k => "\"" + k + "\"").ToArray()));
				headerSet = true;
			}

			//Write the values to the CSV
			tw.WriteLine(string.Join(delim, row.Values.Select(r => (r == null) ? "\"\"" : "\"" + r.ToString() + "\"")));
		}
	}
}
