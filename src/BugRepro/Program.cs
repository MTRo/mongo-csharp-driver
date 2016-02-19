using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Nito.AsyncEx;
using Nito.AsyncEx.Synchronous;

namespace BugRepro
{
	class Program
	{
		static int Main(string[] args)
		{
			//return MainAsync(args).Result;
			return AsyncContext.Run(() => MainAsync(args));

			//Task.Run(() => MainAsync(args)).Wait();

			return 0;
		}

		static MongoClient client;
		static IMongoDatabase db;
		static IMongoCollection<BsonDocument> collection;

		static async Task TestFunc()
		{
		//	await Task.Run(async () =>
			{
				try
				{
					client = new MongoClient("mongodb://localhost:27217/");
					db = client.GetDatabase("TestDB");
					collection = db.GetCollection<BsonDocument>("TetsCollection");

					var doc = new BsonDocument()
					{
						{"key", "Value"}
					};

					await collection.InsertOneAsync(doc).ConfigureAwait(false);
					Console.Out.Write("Done!");
				}
				catch (Exception ex)
				{
					Console.Out.WriteLine($"Raise exception: {ex}");
					throw;
				}
			}
		//).ConfigureAwait(false);
		}



		static async Task<int> MainAsync(string[] args)
		{
			TestFunc();  //!< Just call. Don't wait.
			//await TestFunc().ConfigureAwait(false);

			while (true)
			{
				Console.ReadKey();
			}

			return 0;
			//return Task.FromResult(0);
		}

	}
}
