
using System.Data;
using System.Data.SqlClient;

Console.WriteLine("Hello from My Ordinary World.");


SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder();
stringBuilder.UserID = ".";
stringBuilder.InitialCatalog = "DotNetTrainingBatch4";
stringBuilder.UserID = "sa";
stringBuilder.Password = "sa@123";



SqlConnection connection = new SqlConnection(stringBuilder.ConnectionString);


connection.Open();
Console.WriteLine("Connection Opened.");

string query = "select * from Tbl_Blog";
SqlCommand cmd = new SqlCommand(query, connection);
SqlDataAdapter adapter = new SqlDataAdapter(cmd);
DataTable dt = new DataTable();
adapter.Fill(dt);



connection.Close();
Console.WriteLine("Connection Closed");

// dataset => datatable
// datatable => datarow
// datarow => datacolumn

foreach(DataRow dr in dt.Rows)
{
    Console.WriteLine("Blog Id => " + dr["BlogId"]);
    Console.WriteLine("Blog Title => " + dr["BlogTitle"]);
    Console.WriteLine("Blog Author => " + dr["BlogAuthor"]);
    Console.WriteLine("Blog Content => " + dr["BlogContent"]);
    Console.WriteLine("----------");
}


// Ado.Net Read

Console.ReadKey();