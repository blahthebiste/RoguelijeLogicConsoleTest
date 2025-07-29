public class Scavenge : Encounter {

    public Scavenge() {
        this.name = "Scavenge";
        this.description = "Gain a random tier 1 item.";
    }

    // This function contains the bulk of the Encounter code, where the player actually goes through it.
    public override void execute()
    {
        Console.WriteLine(this.name);
        Console.WriteLine(this.description);
        Console.WriteLine("");
        // Get the next one
        EquipmentItem scavengedItem = CurrentRun.getRandomItemFromPool(1, true);
        CurrentRun.Inventory.Add(scavengedItem);
        Console.WriteLine("Got " + scavengedItem.name);
        // Add a forge Encounter to the encounter pool to replace the scavenge
        CurrentRun.EncounterPool.Add(new Forge());
    }
}