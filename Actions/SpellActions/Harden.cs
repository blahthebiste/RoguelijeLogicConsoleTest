public class Harden : Action {

    public Harden() {
        this.name = "Harden";
        this.description = "Gain 1 Toughness.";
        this.actionType = ActionType.SPELL;
        this.magicNumber = 1;
        this.hasLimitedUses = true;
        this.uses = 3;
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
        // Apply the Whirl status effect
        owner.ReceiveStatusEffect(new Toughness(magicNumber, owner));
        return base.use(target, modifier);
    }
}