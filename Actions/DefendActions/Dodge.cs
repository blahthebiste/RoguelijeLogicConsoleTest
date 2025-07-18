public class Dodge : Action
{

    public Dodge()
    {
        this.name = "Dodge";
        this.description = "Dodge the next attack this turn.";
        this.actionType = ActionType.DEFEND;
        this.magicNumber = 1;
        this.targetting = TargetCategory.SELF;
    }


    public override bool useOnce(Modifier? modifier) {
        if (this.owner == null) {
            Console.WriteLine("ERROR: null owner for action '"+this+"'.");
            return false;
        }
        // Apply the Dodge status effect
        this.owner.AddStatusEffect(new Dodging(magicNumber, this.owner));
        return true;
    }
}