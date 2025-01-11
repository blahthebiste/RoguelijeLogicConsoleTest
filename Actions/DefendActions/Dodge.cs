public class Dodge : Action {

    public Dodge() {
        this.name = "Dodge";
        this.description = "Dodge the next attack this turn.";
        this.actionType = ActionType.DEFEND;
        this.magicNumber = 1;
        this.targetting = TargetCategory.NONE;
    }

    // For now, nothing special.
    public override bool canUse(Entity? target, Modifier? modifier) {
        return base.canUse(target, modifier);
    }

    public override bool use(Entity? target, Modifier? modifier) {
        if(base.use(target, modifier)) {
            // Apply the Dodge status effect
            owner!.AddStatusEffect(new Dodging(magicNumber, owner));
            return true;
        }
        return false;
    }
}