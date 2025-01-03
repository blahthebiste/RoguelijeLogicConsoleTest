public class EquipmentItem : Item {

    
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
                if(this.slot != ActionType.NONE && this.slot != hero.ActionList[actionIndex-1].actionType) {
                    Console.WriteLine("ERROR: item does not fit that action type!");
                    return false;
                }
                // At this point, the action is a valid equipment slot for this item.
                // Before we equip it, check for an item already in that slot. It must be removed first.
                if(hero.ActionList[actionIndex-1].equippedItem != null) {
                    hero.ActionList[actionIndex-1].Unequip();
                }
                // Finally, equip the item.
                hero.ActionList[actionIndex-1].equippedItem = this;
                return true;
            }
        }
        Console.WriteLine("ERROR: hero not found!");
        return false;
    }
}