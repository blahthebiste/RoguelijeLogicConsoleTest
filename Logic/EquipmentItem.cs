public class EquipmentItem : Item {
    
    public ActionType slot = ActionType.ANY;

    public Action? parentAction = null;

        // Useful for printing what would be shown to the player
	public override string ToString() {
		string actionString = "(Slot - "+this.slot+") "+this.name + ": " + this.description;
		return actionString;
	}
    
    // Equip this item to the specified action.
    // Boolean return code signifies whether the equip attempt succeeded.
    public bool Equip(Action action) {
        if(this.slot != ActionType.ANY && this.slot != action.actionType) {
            Console.WriteLine("ERROR: item does not fit that action type!");
            return false;
        }
        // Before we equip it, check for an item already in that slot. It must be removed first.
        if(action.equippedItem != null) {
            action.Unequip();
        }
        // Finally, equip the item.
        this.parentAction = action;
        action.equippedItem = this;
        if(action.owner != null) {
            Console.WriteLine("Equipping "+this.name+" to "+action.owner.name);
            // Exhaust the hero who equipped/unequipped this in combat
            if(CurrentRun.InCombat) {
                action.owner.exhausted = true;
            }
        }
        CurrentRun.Inventory.Remove(this);
        this.onEquip();
        return true;
    }

    // Equip this item to the action of the specified hero, at the specified index in their action list.
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
                Console.WriteLine("Equipping "+this.name+" to "+this.parentAction.owner!.name);
                // Exhaust the hero who equipped/unequipped this in combat
                if(CurrentRun.InCombat) {
                    this.parentAction!.owner!.exhausted = true;
                }
                CurrentRun.Inventory.Remove(this);
                this.onEquip();
                return true;
            }
        }
        Console.WriteLine("ERROR: hero not found!");
        return false;
    }

    
    
    // Unequip this item from the specified action.
    // Boolean return code signifies whether the unequip attempt succeeded.
    public bool Unequip(Action action) {
        // Unequip the item.
        this.onUnequip();
        if(action.owner != null) {
            Console.WriteLine("Unequipping "+this.name+" from "+this.parentAction!.owner!.name);
            // Exhaust the hero who equipped/unequipped this in combat
            if(CurrentRun.InCombat) {
                action.owner.exhausted = true;
            }
        }
        action.equippedItem = null;
        CurrentRun.Inventory.Add(this);
        return true;
    }

    // Unequip this item from the specified index of the specified hero's action list.
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
                this.onUnequip();
                Console.WriteLine("Unequipping "+this.name+" from "+this.parentAction!.owner!.name);
                // Exhaust the hero who equipped/unequipped this in combat
                if(CurrentRun.InCombat) {
                    this.parentAction!.owner!.exhausted = true;
                }
                this.parentAction = null;
                hero.ActionList[actionIndex-1].equippedItem = null;
                CurrentRun.Inventory.Add(this);
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

    public bool matchesActionType(ActionType type) {
        if(this.slot == ActionType.ANY) {
            return true;
        }
        if(type == this.slot) {
            return true;
        }
        return false;
    }

    // Counts how many actions on the specified entity are allowed to equip this item
    public int numberMatchingActions(Entity entityToEquip) {
        int matches = 0;
        foreach(Action action in entityToEquip.ActionList) {
            if(this.matchesActionType(action.actionType)) {
                matches++;
            }
        }
        return matches;
    }

    public bool isEquipped(){
        return this.parentAction != null;
    }

    public virtual void onEquip() {

    }


    public virtual void onUnequip() {
        
    }
}