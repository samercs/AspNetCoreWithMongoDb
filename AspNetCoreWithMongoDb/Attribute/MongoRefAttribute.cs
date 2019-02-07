namespace AspNetCoreWithMongoDb.Attribute
{
    public class MongoRefAttribute: System.Attribute
    {
        public string Table { get; set; }
        public string RefId { get; set; }
        public string Id { get; set; }

        public MongoRefAttribute(string princepleTable, string princepleCol, string refCol)
        {
            Table = princepleTable;
            RefId = princepleCol;
            Id = refCol;
        }
    }
}