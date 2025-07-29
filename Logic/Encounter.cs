public class Encounter
{

    public String name = "MISSING NAME";
    public String description = "MISSING DESCRIPTION";

    // This function contains the bulk of the Encounter code, where the player actually goes through it.
    public virtual void execute()
    {
        Console.WriteLine("MISSING EXECUTE FUNCTION!");
    }

    public override string ToString()
    {
        return name + " - " + description;
    }
}