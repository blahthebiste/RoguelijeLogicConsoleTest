public class Pray : Action
{

    public Pray()
    {
        this.name = "Pray";
        this.actionType = ActionType.REST;
        this.magicNumber = 2;
        this.targetting = TargetCategory.SELF;
        this.description = "Gain "+magicNumber+" Piety.";
    }
    
    public override bool useOnce(Modifier? modifier) {
        if (this.owner == null) {
            Console.WriteLine("ERROR: null owner for action '"+this+"'.");
            return false;
        }
        // Apply Piety status effect
        this.owner.AddStatusEffect(new Piety(magicNumber, this.owner));
        return true;
    }
}