public class Plunder : Encounter {

    public Plunder() {
        this.name = "Plunder";
        this.description = "Gain a random tier 1 item.";
    }

    // This function contains the bulk of the Encounter code, where the player actually goes through it.
    public override void execute()
    {
        Console.WriteLine(this.name);
        Console.WriteLine(this.description);
        Console.WriteLine("");
        // Get the next one
        EquipmentItem plunderedItem = CurrentRun.getRandomItemFromPool(1, true);
        CurrentRun.Inventory.Add(plunderedItem);
        Console.WriteLine("Got " + plunderedItem.name);
        // Add a forge Encounter to the encounter pool to replace the plunder
        CurrentRun.EncounterPool.Add(new Forge());
    }
}