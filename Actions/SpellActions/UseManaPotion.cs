public class UseManaPotion : Action {

    public UseManaPotion() {
        this.name = "Use Mana Potion";
        this.description = "Regain all other Spell uses.";
        this.actionType = ActionType.SPELL;
        this.hasLimitedUses = true;
        this.uses = 1;
        this.maxUses = this.uses;
        this.targetting = TargetCategory.SELF;
    }

    public override bool useOnce(Modifier? modifier) {
        if (this.owner == null) {
            Console.WriteLine("ERROR: null owner for action '"+this+"'.");
            return false;
        }
        // Restore spell uses.
        foreach (Action action in this.owner.ActionList)
        {
            if (action != this && action.hasLimitedUses)
            {
                Console.WriteLine("Regaining uses for " + action.name + " up to " + action.maxUses);
                action.uses = action.maxUses;
            }
        }
        return true;
    }
}