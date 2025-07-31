public class Upgrade : Encounter {

    public Upgrade() {
        this.name = "Upgrade";
        this.description = "Gain a random modifier to upgrade a card.";
    }

    // This function contains the bulk of the Encounter code, where the player actually goes through it.
    public override void execute()
    {
        Console.WriteLine(this.name);
        Console.WriteLine(this.description);
        Console.WriteLine("");
        // Geta random one
        CurrentRun.Shuffle(CurrentRun.ModifierPool);
        Modifier newModifier = CurrentRun.ModifierPool[0];
        CurrentRun.Inventory.Add(newModifier);
        Console.WriteLine("Got card modifier, '" + newModifier.name+"'.");
    }
}