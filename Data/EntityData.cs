
public class EntityData {
    public string Name { get; set; }
    public string Description { get; set; }
    public int HP { get; set; }
    public List<string> ActionList { get; set; }
    public string Master { get; set; }

    public EntityData()
    {
        Name = "MISSING NAME";
        Description = "MISSING DESCRIPTION";
        ActionList = new List<string>();
        Master = "UNDEFINED";
    }
}