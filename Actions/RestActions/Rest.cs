public class Rest : Action {

    public Rest() {
        this.name = "Rest";
        this.description = "Recover 3 HP.";
        this.actionType = ActionType.REST;
        this.healing = 3;
        this.targetting = TargetCategory.SELF;
    }

    public override bool useOnce(Modifier? modifier) {
        if (this.owner == null) {
            Console.WriteLine("ERROR: null owner for action '"+this+"'.");
            return false;
        }
        // Restore HP.
        this.owner.ReceiveHealing(healing);
        return true;
    }
}