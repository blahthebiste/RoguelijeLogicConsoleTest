public class Pickpocket : Action {

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
    public override bool canUse() {
        return base.canUse();
    }

    public override bool use(Entity? target, Modifier? modifier) {
        if(owner == null) {
            Console.WriteLine("ERROR: no owner for action!");
            return false;
        }
        // Generate a random item
        
        return base.use(target, modifier);
    }
}