// Extended by EquipmentItem, ModifierItem, etc
public class Item {
    public string name = "MISSING NAME";
    public string description = "MISSING DESCRIPTION";

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

}