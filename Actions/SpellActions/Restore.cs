public class Restore : Action {

    public Restore() {
        this.name = "Restore";
        this.description = "Restore 6 HP.";
        this.actionType = ActionType.SPELL;
        this.magicNumber = 6;
        this.hasLimitedUses = true;
        this.uses = 6;
        this.targetting = TargetCategory.SINGLE_ANY;
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
        // Apply the Whirl status effect
        owner.ReceiveHealing(magicNumber);
        return base.use(target, modifier);
    }
}