
using LazarusDotNetBatch4.ConsoleApp;
using System.Data;
using System.Data.SqlClient;

Console.WriteLine("Hello from My Ordinary World.");



// Ado.Net Read | CRUD 

// AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
// adoDotNetExample.Read();
// adoDotNetExample.Create( "Test", "James", "This is my Book.");
// adoDotNetExample.Update(1, "Harry", "Thomas", "Harry Potter with Forest.");
// adoDotNetExample.Create("Test", "James", "Book");
// adoDotNetExample.Delete(10);
// adoDotNetExample.Edit(10);
// adoDotNetExample.Edit(1);


// Dapper CRUD
// DapperExample dapperExample = new DapperExample();
// dapperExample.Run();

// EFCore CRUD
EFCoreExample efCoreExample = new EFCoreExample();
efCoreExample.Run();

Console.ReadKey();