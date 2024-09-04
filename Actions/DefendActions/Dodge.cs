public class Dodge : Action {

    public Dodge() {
        this.name = "Dodge";
        this.description = "Dodge the next attack this turn.";
        this.actionType = ActionType.DEFEND;
        this.magicNumber = 1;
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
        // Apply the Dodge status effect
        owner.ReceiveStatusEffect(new Dodging(magicNumber, owner));
        return base.use(target, modifier);
    }
}