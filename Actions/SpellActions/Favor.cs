public class Favor : Action {

    public Favor() {
        this.name = "Favor";
        this.actionType = ActionType.SPELL;
        this.magicNumber = 1;
        this.hasLimitedUses = true;
        this.uses = 6;
        this.maxUses = this.uses;
        this.targetting = TargetCategory.SELF;
        this.description = "Your next attack will always find its mark, and pierces through Block.";
    }

    public override bool useOnce(Modifier? modifier) {
        if (this.owner == null) {
            Console.WriteLine("ERROR: null owner for action '"+this+"'.");
            return false;
        }
        // Apply Favor status effect
        this.owner.AddStatusEffect(new Favored(magicNumber, this.owner));
        return true;
    }
}