public class EmbeddedSword : Action {

    public EmbeddedSword()
    {
        this.name = "Embedded Sword";
        this.description = "The sword calls to you.";
        this.actionType = ActionType.PASSIVE;
    }

    // Grant Excalibur (for this combat only)
    public override void onDeath()
    {
        if (this.owner == null)
        {
            Console.WriteLine("ERROR: " + this.name + " has null owner!");
            return;
        }
        Excalibur sword = new Excalibur();
        sword.temporary = true;
        sword.AddToInventory();
        Console.WriteLine("You have unseated the blade. The King's Court await your command.");
    }
}