public class Charge : Action {

    public Charge() {
        this.name = "Charge";
        this.description = "Next spell has +5 to damage/block/healing.";
        this.actionType = ActionType.SKILL;
        this.magicNumber = 5;
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
        // Apply the Charged status effect
        owner.ReceiveStatusEffect(new Charged(magicNumber, owner));
        return base.use(target, modifier);
    }
}