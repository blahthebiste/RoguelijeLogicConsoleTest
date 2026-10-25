public class Leadership : Action {

    public Leadership() {
        this.name = "Leadership";
        this.actionType = ActionType.SPELL;
        this.magicNumber = 1;
        this.hasLimitedUses = true;
        this.uses = 1;
        this.maxUses = this.uses;
        this.targetting = TargetCategory.SELF;
        this.description = "Draw "+magicNumber+" additional card each turn.";
    }

    public override bool useOnce() {
        if (this.owner == null) {
            Console.WriteLine("ERROR: null owner for action '"+this+"'.");
            return false;
        }
        // Apply Leading status effect
        this.owner.AddStatusEffect(new Leading(magicNumber, this.owner));
        return true;
    }
}