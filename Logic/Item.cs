// Extended by EquipmentItem, ModifierItem, etc
public class Item : Events {
    public string name = "MISSING NAME";
    public string description = "MISSING DESCRIPTION";
    public string type = "MISSING TYPE";


    // Useful for printing what would be shown to the player
	public override string ToString() {
		string actionString = this.name + ": " + this.description;

		return actionString;
	}

    // Removes the item from the player's inventory.
    public virtual void RemoveFromInventory(){
        CurrentRun.Inventory.Remove(this);
    }
    
    // Adds the item to the player's inventory.
    public virtual void AddToInventory(){
        CurrentRun.Inventory.Add(this);
    }

    // Most items do something on use. Some are consumed upon being uses.
    // Each one should define its own use function.
    public virtual void use()
    {
        Console.WriteLine("ERROR: item has not overrided use function!");
    }
}