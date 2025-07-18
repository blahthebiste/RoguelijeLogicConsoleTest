public class Vanish : Action
{

    public Vanish()
    {
        this.name = "Vanish";
        this.description = "Dodge all attacks this turn.";
        this.actionType = ActionType.DEFEND;
        this.magicNumber = 1;
        this.targetting = TargetCategory.SELF;
    }


    public override bool useOnce(Modifier? modifier) {
        if (this.owner == null) {
            Console.WriteLine("ERROR: null owner for action '"+this+"'.");
            return false;
        }
        // Apply the Vanished status effect
        this.owner.AddStatusEffect(new Vanished(magicNumber, this.owner));
        return true;
    }
}