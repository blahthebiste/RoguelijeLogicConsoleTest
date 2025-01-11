public class EquipmentItem : Item {
    
    public ActionType slot = ActionType.ANY;

    public Action? parentAction = null;

        // Useful for printing what would be shown to the player
	public override string ToString() {
		string actionString = "(Slot - "+this.slot+") "+this.name + ": " + this.description;
		return actionString;
	}
    
    // Boolean return code signifies whether the equip attempt succeeded.
    public bool Equip(string heroName, int actionIndex) {
        foreach(PlayerCharacter hero in CurrentRun.Party){
            if(hero.name.ToLower().Trim() == heroName) {
                // Check that the index is valid:
                if(actionIndex < 1) {
                    Console.WriteLine("ERROR: action index must be at least 1!");
                    return false;
                }
                if(actionIndex > hero.ActionList.Count) {
                    Console.WriteLine("ERROR: action index is too large!");
                    return false;
                }
                if(this.slot != ActionType.ANY && this.slot != hero.ActionList[actionIndex-1].actionType) {
                    Console.WriteLine("ERROR: item does not fit that action type!");
                    return false;
                }
                // At this point, the action is a valid equipment slot for this item.
                // Before we equip it, check for an item already in that slot. It must be removed first.
                if(hero.ActionList[actionIndex-1].equippedItem != null) {
                    hero.ActionList[actionIndex-1].Unequip();
                }
                // Finally, equip the item.
                this.parentAction = hero.ActionList[actionIndex-1];
                hero.ActionList[actionIndex-1].equippedItem = this;
                this.onEquip();
                return true;
            }
        }
        Console.WriteLine("ERROR: hero not found!");
        return false;
    }

    
    
    // Boolean return code signifies whether the unequip attempt succeeded.
    public bool Unequip(string heroName, int actionIndex) {
        foreach(PlayerCharacter hero in CurrentRun.Party){
            if(hero.name.ToLower().Trim() == heroName) {
                // Check that the index is valid:
                if(actionIndex < 1) {
                    Console.WriteLine("ERROR: action index must be at least 1!");
                    return false;
                }
                if(actionIndex > hero.ActionList.Count) {
                    Console.WriteLine("ERROR: action index is too large!");
                    return false;
                }
                if(hero.ActionList[actionIndex].equippedItem == null) {
                    Console.WriteLine("ERROR: action at requested index has no item equipped!");
                    return false;
                }
                // Finally, unequip the item.
                this.parentAction = null;
                hero.ActionList[actionIndex-1].equippedItem = null;
                this.onUnequip();
                return true;
            }
        }
        Console.WriteLine("ERROR: hero not found!");
        return false;
    }

    public Entity? getOwner() {
        if(this.parentAction == null) return null;
        if(this.parentAction.owner == null) return null;
        return this.parentAction.owner;
    }

    public bool isEquipped(){
        return this.parentAction != null;
    }

    public virtual void onEquip() {
        Console.WriteLine("Equipping "+this.name);
    }


    public virtual void onUnequip() {
        Console.WriteLine("Unequipping "+this.name);
    }
}