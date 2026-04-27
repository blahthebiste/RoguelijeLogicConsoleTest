public class Repose : Action {

    public Repose() {
        this.name = "Repose";
        this.actionType = ActionType.REST;
        this.healing = 4;
        this.targetting = TargetCategory.SELF;
        this.description = "Recover "+healing+" HP.";
    }

    public override bool useOnce(Modifier? modifier) {
        if (this.owner == null) {
            Console.WriteLine("ERROR: null owner for action '"+this+"'.");
            return false;
        }
        // Restore HP.
        this.owner.ReceiveHealing(healing + (modifier == null? 0 : modifier.healMod));
        return true;
    }
}