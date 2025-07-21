public class Pickpocket : Action {

    List<EquipmentItem> pickpocketedItems = new List<EquipmentItem>();

    public Pickpocket() {
        this.name = "Pickpocket";
        this.description = "Generate a random level 1 item for this combat only, and equip it.";
        this.actionType = ActionType.SPELL;
        this.hasLimitedUses = true;
        this.uses = 1;
        this.maxUses = this.uses;
        this.targetting = TargetCategory.NONE;
    }

    public override bool useOnce(Modifier? modifier) {
        if (this.owner == null) {
            Console.WriteLine("ERROR: null owner for action '"+this+"'.");
            return false;
        }
        // Generate a random item (not removed from the pool)
        EquipmentItem pickpocketedItem = CurrentRun.getRandomItemFromPool(1, false);
        pickpocketedItems.Add(pickpocketedItem);
        CurrentRun.Inventory.Add(pickpocketedItem);
        Console.WriteLine("Got "+pickpocketedItem.name+".");
        CurrentRun.equipItem(pickpocketedItem.name, this.owner.name);
        return true;
    }

    // Remove all items created by this spell at the end of combat.
    public override void endOfCombat(){
        
        // Loop through each hero's action list and remove matching equipped items
        foreach(Entity hero in CurrentRun.Party) {
            foreach(Action action in hero.ActionList.ToList()) {
                if(action.equippedItem == null) continue;
                if(pickpocketedItems.Contains(action.equippedItem)) {
                    action.Unequip();
                }
            }
        }
        foreach(Entity hero in CurrentRun.Bench) {
            foreach(Action action in hero.ActionList.ToList()) {
                if(action.equippedItem == null) continue;
                if(pickpocketedItems.Contains(action.equippedItem)) {
                    action.Unequip();
                }
            }
        }
        // Do inventory last since unequipped items go here
        foreach(Item item in CurrentRun.Inventory.ToList()) {
            if(item is EquipmentItem) {
                if(pickpocketedItems.Contains(item)) {
                    CurrentRun.Inventory.Remove(item);
                }
            }
        }
    }
}