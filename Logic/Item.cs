// Extended by EquipmentItem, ModifierItem, etc
public class Item {
    public string name = "MISSING NAME";
    public string description = "MISSING DESCRIPTION";
    public ActionType slot = ActionType.NONE;


    // Useful for printing what would be shown to the player
	public override string ToString() {
		string actionString = "(Slot - "+this.slot+") "+this.name + ": " + this.description;

		return actionString;
	}

    // Removes the item from the player's inventory.
    public void RemoveFromInventory(){
        CurrentRun.Inventory.Remove(this);
    }
}