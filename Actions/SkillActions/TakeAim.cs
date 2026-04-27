public class TakeAim : Action {

    public TakeAim() {
        this.name = "Take Aim";
        this.actionType = ActionType.SKILL;
        this.magicNumber = 1;
        this.targetting = TargetCategory.SELF;
        this.description = "Next attack deals double damage.";
    }

    public override bool useOnce(Modifier? modifier) {
        if (this.owner == null) {
            Console.WriteLine("ERROR: null owner for action '"+this+"'.");
            return false;
        }
        // Apply DeadlyAim status effect
        this.owner.AddStatusEffect(new DeadlyAim(magicNumber, this.owner));
        return true;
    }
}