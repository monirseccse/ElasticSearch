using Elasticsearch.Net;
using ElasticSearch;
using Nest;
using System;
using System.Reflection.Metadata;

var node = new Uri("https://localhost:9200");
var username = "elastic"; // Replace with your Elasticsearch username
var password = "5LBR-s7dDQbefu2MtA2m"; // Replace with your Elasticsearch password
List<Product> products = new List<Product>();

products.Add(new Product()
{
    Name = "Samsung Galaxy Note 8",
    Suggest = new CompletionField()
    {
        Input = new[] { "Samsung Galaxy Note 8", "Galaxy Note 8", "Note 8" }
    }
});

products.Add(new Product()
{
    Name = "Samsung Galaxy S8",
    Suggest = new CompletionField()
    {
        Input = new[] { "Samsung Galaxy S8", "Galaxy S8", "S8" }
    }
});

products.Add(new Product()
{
    Name = "Apple Iphone 8",
    Suggest = new CompletionField()
    {
        Input = new[] { "Apple Iphone 8", "Iphone 8" }
    }
});

products.Add(new Product()
{
    Name = "Apple Iphone X",
    Suggest = new CompletionField()
    {
        Input = new[] { "Apple Iphone X", "Iphone X" }
    }
});

products.Add(new Product()
{
    Name = "Apple iPad Pro",
    Suggest = new CompletionField()
    {
        Input = new[] { "Apple iPad Pro", "iPad Pro" }
    }
});
var connectionPool = new SingleNodeConnectionPool(node);

var settings = new ConnectionSettings(connectionPool)
    .BasicAuthentication(username, password)
    .ServerCertificateValidationCallback((sender, certificate, chain, sslPolicyErrors) => true)
    .DefaultIndex("myindex");

var client = new ElasticClient(settings);
var roadIndex = "roads";
var personsIndex = "persons";
//var document = new Road { Id = 7, RoadName = "MonirChottor" }; // Replace with your document data
//var initializerIndexResponse = client.Index(new IndexRequest<Road>(document, "roads"));


var searchQuery = "*Monir*"; // The search query
string[] indexNames = new string[] { roadIndex,personsIndex };

var roadSearchResponse = await client.SearchAsync<object>(s => s
        .Index(indexNames).Query
         (q => q
        .Bool(b => b
            .Should(sh => sh
                .Wildcard(wc => wc
                    .Field("name")
                    .Value(searchQuery.ToLower())
                ),
                sh => sh
                .Wildcard(wc => wc
                    .Field("roadName")
                    .Value(searchQuery.ToLower())
                )
            ))
        )
     );



//var roadSearchResponse = await client.SearchAsync<Road>(s => s
//         .Index(indexNames)
//         .Query(q => q
//             .MultiMatch(m => m
//                 .Fields(f => f.Field(i => i.roadName))
//                 .Query(searchQuery)
//             )
//         )
//     );

if ( roadSearchResponse.IsValid )
{
    Console.WriteLine("Matching documents:");
foreach (var hit in roadSearchResponse.Hits)
{
    Console.WriteLine($"Id :{hit.Id}, Name: {hit.Fields}");
}
 }
else
{
    Console.WriteLine("failed");
}
//Console.ReadKey();
// Get all index names
//if( !client.Indices.Exists("roads").Exists)
//{
//    var createRoadsIndexResponse = await client.Indices.CreateAsync("roads", c => c
//    .Map<Road>(m => m
//        .AutoMap()
//    )
//    );

//    if (createRoadsIndexResponse.IsValid)
//    {
//        Console.WriteLine("Index created Successfully");
//    }
//}

//var indices = await client.Indices.GetAliasAsync();

//foreach (var index in indices.Indices.Keys)
//{
//    Console.WriteLine(index);
//}


//InsertData(client);

//async void InsertData(ElasticClient client)
//{
//    var document = new Person { Id = 2, name = "Moniruzzaman" }; // Replace with your document data
//    var initializerIndexResponse = client.Index(new IndexRequest<Person>(document, "persons"));

//    var indexResponse = await client.IndexDocumentAsync(document);

//    if (indexResponse.IsValid)
//    {
//        Console.WriteLine("Document indexed successfully!");
//    }
//    else
//    {
//        Console.WriteLine($"Failed to index document. Error: {indexResponse.ServerError?.ToString()}");
//    }

//}