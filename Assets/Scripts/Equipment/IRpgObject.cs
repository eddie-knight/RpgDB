namespace RpgDB
{
    public interface IRpgObject
    {
        string Name { get; set; }
        int id { get; set; } // Primary Key
        string Category { get; set; }
        int Level { get; set; }
        int Price { get; set; }
        string Bulk { get; set; }
    }
}
