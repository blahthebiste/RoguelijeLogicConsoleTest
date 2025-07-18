public class Taunt : Action {

    public Taunt()
    {
        this.name = "Taunt";
        this.description = "Forces enemies to target me instead of my allies.";
        this.actionType = ActionType.SKILL;
        this.targetting = TargetCategory.SELF;
        this.magicNumber = 1;
    }


    public override bool useOnce(Modifier? modifier) {
        if (this.owner == null) {
            Console.WriteLine("ERROR: null owner for action '"+this+"'.");
            return false;
        }
        // Apply taunting status effect, which updates enemy targets and the Battlefield.Taunters list accordingly:
        this.owner.AddStatusEffect(new Taunting(magicNumber, this.owner));
        return true;
    }
}