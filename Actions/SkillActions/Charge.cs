public class Charge : Action {

    public Charge() {
        this.name = "Charge";
        this.description = "Next spell has +5 to damage/block/healing.";
        this.actionType = ActionType.SKILL;
        this.magicNumber = 5;
        this.targetting = TargetCategory.SELF;
    }

    public override bool useOnce(Modifier? modifier) {
        if (this.owner == null) {
            Console.WriteLine("ERROR: null owner for action '"+this+"'.");
            return false;
        }
        // Apply the Charged status effect
        this.owner.AddStatusEffect(new Charged(magicNumber, this.owner));
        return true;
    }
}