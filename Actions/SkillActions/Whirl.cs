public class Whirl : Action {

    public Whirl() {
        this.name = "Whirl";
        this.description = "Next attack also hits adjacent targets.";
        this.actionType = ActionType.SKILL;
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
        // Apply the Whirl status effect
        owner.ReceiveStatusEffect(new Whirling(magicNumber, owner));
        return base.use(target, modifier);
    }
}