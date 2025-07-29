public class Plunder : Encounter {

    public Plunder() {
        this.name = "Plunder";
        this.description = "Gain some money.";
    }

    // This function contains the bulk of the Encounter code, where the player actually goes through it.
    public override void execute()
    {
        Console.WriteLine(this.name);
        Console.WriteLine(this.description);
        Console.WriteLine("");
        int randomMoneyReward = CurrentRun.rng.Next(10, 61);
        CurrentRun.Money += randomMoneyReward;
        Console.WriteLine("Found $" + randomMoneyReward+".");
    }
}