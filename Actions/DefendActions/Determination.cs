public class Determination : Action
{

    public Determination()
    {
        this.name = "Determination";
        this.actionType = ActionType.DEFEND;
        this.magicNumber = 1;
        this.targetting = TargetCategory.SELF;
        this.description = "HP cannot fall below 1 until next turn.";
    }


    public override bool useOnce(Modifier? modifier) {
        if (this.owner == null) {
            Console.WriteLine("ERROR: null owner for action '"+this+"'.");
            return false;
        }
        // Apply the Determined status effect
        this.owner.AddStatusEffect(new Determined(magicNumber, this.owner));
        return true;
    }
}