public class Favor : Action {

    public Favor() {
        this.name = "Favor";
        this.actionType = ActionType.SPELL;
        this.magicNumber = 1;
        this.hasLimitedUses = true;
        this.uses = 1;
        this.maxUses = this.uses;
        this.targetting = TargetCategory.SELF;
        this.description = "Your next Skill deals double damage, and will always find its mark.";
        this.freeAction = true;
    }

    // TODO: Favored only works on attacks!
    public override bool useOnce() {
        if (this.owner == null) {
            Console.WriteLine("ERROR: null owner for action '"+this+"'.");
            return false;
        }
        // Apply Favor status effect
        this.owner.AddStatusEffect(new Favored(magicNumber, this.owner));
        // Add deadly aim status effect
        this.owner.AddStatusEffect(new DeadlyAim(magicNumber, this.owner));
        return true;
    }
}