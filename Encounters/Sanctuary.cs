public class Sanctuary : Encounter {

    public Sanctuary() {
        this.name = "Sanctuary";
        this.description = "Regain 1 life.";
    }

    // This function contains the bulk of the Encounter code, where the player actually goes through it.
    public override void execute()
    {
        Console.WriteLine(this.name);
        Console.WriteLine(this.description);
        Console.WriteLine("");
        // get life
        CurrentRun.RegainLives(1);
        Console.WriteLine("Lives = " + CurrentRun.Lives);
    }
}