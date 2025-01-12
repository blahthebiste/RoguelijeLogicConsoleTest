public class Pickpocket : Action {

    List<EquipmentItem> pickpocketedItems = new List<EquipmentItem>();

    public Pickpocket() {
        this.name = "Pickpocket";
        this.description = "Gain a random level 1 item for this combat only.";
        this.actionType = ActionType.SPELL;
        this.hasLimitedUses = true;
        this.uses = 1;
        this.maxUses = this.uses;
        this.targetting = TargetCategory.NONE;
    }

    // For now, nothing special.
    public override bool canUse(Entity? target, Modifier? modifier) {
        return base.canUse(target, modifier);
    }

    public override bool use(Entity? target, Modifier? modifier) {
        if(base.use(target, modifier)) {
            // Generate a random item
            EquipmentItem pickpocketedItem = CurrentRun.getRandomItemFromTier1Pool();
            pickpocketedItems.Add(pickpocketedItem);
            CurrentRun.Inventory.Add(pickpocketedItem);
            Console.WriteLine("Got a(n) "+pickpocketedItem.name+".");
            return true;
        }
        return false;
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
        }// Do inventory last since unequipped items go here
        foreach(EquipmentItem item in CurrentRun.Inventory.ToList()) {
            if(pickpocketedItems.Contains(item)) {
                CurrentRun.Inventory.Remove(item);
            }
        }
    }
}