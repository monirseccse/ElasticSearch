using Nest;

namespace ElasticSearch
{
    public class Person
    {
        public int Id { get; set; }
        public string name { get; set; }
    }

    public class Road
    {
        public int id { get; set; }
        public string roadName { get; set; } = null!;
      //  public CompletionField Suggest { get; set; }
    }
    public class Product
    {
        public string Name { get; set; }
        public CompletionField Suggest { get; set; }
    }
    public class Village
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
