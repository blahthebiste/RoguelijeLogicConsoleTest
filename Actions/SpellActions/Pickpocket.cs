public class Pickpocket : Action {

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
        // Make the item temporary:
        pickpocketedItem.temporary = true;
        CurrentRun.Inventory.Add(pickpocketedItem);
        Console.WriteLine("Got "+pickpocketedItem.name+".");
        CurrentRun.equipItem(pickpocketedItem.name, this.owner.name);
        return true;
    }

}