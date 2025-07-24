public class Skitter : Action
{

    public Skitter()
    {
        this.name = "Skitter";
        this.description = "Dodge the next attack.";
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