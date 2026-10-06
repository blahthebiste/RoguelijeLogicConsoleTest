public class NotYet : Action
{

    public NotYet()
    {
        this.name = "Not Yet";
        this.actionType = ActionType.REST;
        this.targetting = TargetCategory.SELF;
        this.description = "Recover all missing HP below half.";
    }
    
    public override bool useOnce(Modifier? modifier) {
        if (this.owner == null) {
            Console.WriteLine("ERROR: null owner for action '"+this+"'.");
            return false;
        }
        // Restore HP.
        double halfHP = owner.maxHP/2.0;
        int halfHPRounded = (int)Math.Ceiling(halfHP); // Round up
        int missingHPBelowHalf = halfHPRounded - owner.currentHP;
        if(missingHPBelowHalf > 0)
        {
            Console.WriteLine("Not... Yet...");
            this.owner.ReceiveHealing(missingHPBelowHalf + (modifier == null? 0 : modifier.healMod));
        }
        return true;
    }
}