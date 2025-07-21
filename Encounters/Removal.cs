public class Removal : Encounter {

    public Removal() {
        this.name = "Removal";
        this.description = "Shrink your deck size by 1.";
    }

    // This function contains the bulk of the Encounter code, where the player actually goes through it.
    public override void execute()
    {
        Console.WriteLine(this.name);
        Console.WriteLine(this.description);
        Console.WriteLine("");
        // Shrink deck size
        CurrentRun.MinimumDeckSize--;
        Console.WriteLine("Deck size has been decreased to " + CurrentRun.MinimumDeckSize);
        // Dump last card back into collection:
        CurrentRun.MoveToCollection(CurrentRun.MasterDeck[CurrentRun.MasterDeck.Count-1]);
    }
}