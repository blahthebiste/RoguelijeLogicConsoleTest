public class EquipmentItem : Item {
    
    public ActionType slot = ActionType.ANY;

    public Action? parentAction = null;

    public int? price; // A baseline price the item is usually sold for at shops
    
    // These are only used for items that replace actions
    public Action? oldAction;
    int actionIndex = -1;

        // Useful for printing what would be shown to the player
	public override string ToString() {
		string actionString = "(Slot - "+this.slot+") "+this.name + ": " + this.description;
		return actionString;
	}
    
    // Equip this item to the specified action.
    // Boolean return code signifies whether the equip attempt succeeded.
    public bool Equip(Action action) {
        // Beware of replaced actions:
        if(action.equippedItem != null && action.equippedItem.oldAction != null) {
            Console.WriteLine("Attempting to equip item to replacement action. Using old action "+action.equippedItem.oldAction.name+" instead.");
            action = action.equippedItem.oldAction;
        }
        
        if(this.slot != ActionType.ANY && this.slot != action.actionType) {
            Console.WriteLine("ERROR: item does not fit that action type!");
            return false;
        }
        // Before we equip it, check for an item already in that slot. It must be removed first.
        if(action.equippedItem != null) {
            Console.WriteLine("Unequipping existing item "+action.equippedItem);
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
            Console.WriteLine("Unequipping "+this.name+" from "+action.owner.name);
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

    public bool matchesActionType(Action action) {
        ActionType type = action.actionType;
        if(this.slot == ActionType.ANY) {
            return true;
        }
        if(type == this.slot && action.hasEquipmentSlot) {
            return true;
        }
        // No need to check for DUAL; Actions can never be DUAL, only ActionCards
        return false;
    }

    // Counts how many actions on the specified entity are allowed to equip this item
    public int numberMatchingActions(Entity entityToEquip) {
        int matches = 0;
        foreach(Action action in entityToEquip.ActionList) {
            if(this.matchesActionType(action) && action.hasEquipmentSlot) {
                matches++;
            }
        }
        return matches;
    }

    // Replaces an action in-place in the equipped action's owner's action list
    public void replaceAction(Action newAction) {
        if(this.parentAction == null) {
            Console.WriteLine("ERROR: "+this.name+" cannot replace action -- null parentAction!");
            return;
        }
        if(this.getOwner() == null) {
            Console.WriteLine("ERROR: "+this.name+" cannot replace action -- null owner!");
            return;
        }
        this.oldAction = this.parentAction!;
        this.actionIndex = this.getOwner()!.ActionList.IndexOf(this.oldAction); // Keep the index in the action list
        if(actionIndex == -1) {
            Console.WriteLine("ERROR: "+this.name+" could not find an index for the old action!");
            return;
        }
        this.getOwner()!.ActionList[this.actionIndex] = newAction;
    }

    // Restores the original action
    public void restoreOriginalAction(bool emptyOriginalActionsItemSlot=true) {
        if(this.oldAction == null) {
            Console.WriteLine("ERROR: "+this.name+" cannot restore old action; old action is null!");
            return;
        }
        if(this.getOwner() == null) {
            Console.WriteLine("ERROR: "+this.name+" cannot restore old action -- null owner!");
            return;
        }
        if(actionIndex == -1) {
            Console.WriteLine("ERROR: "+this.name+" could not find an index for the old action!");
            return;
        }
        if(emptyOriginalActionsItemSlot) {
            // Useful, since this is usually only called when unequipping the item anyway
            // Necessary, because the item will remember what was equipped to it
            this.oldAction.equippedItem = null;
        } 
        this.getOwner()!.ActionList[this.actionIndex] = this.oldAction;
        // Reset values
        this.actionIndex = -1;
        this.oldAction = null;
    }

    public bool isEquipped(){
        return this.parentAction != null;
    }

    public virtual void onEquip() {

    }


    public virtual void onUnequip() {
        
    }


    // Triggers whenever the action that the item is equipped to is used
    public virtual Action onUseEquippedAction(Action actionBeingUsed) {
        return actionBeingUsed;
    }

}