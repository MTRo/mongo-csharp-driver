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

		static async void TestFunc()
		{
			client = new MongoClient("mongodb://localhost/");
			db = client.GetDatabase("TestDB");
			collection = db.GetCollection<BsonDocument>("TetsCollection");

			var doc = new BsonDocument()
			{
				{"key", "Value"}
			};

			await collection.InsertOneAsync(doc).ConfigureAwait(false);
		}



		static Task<int> MainAsync(string[] args)
		{
			TestFunc();

			while (true)
			{
				Console.ReadKey();
			}

			return Task.FromResult(0);
		}

	}
}
