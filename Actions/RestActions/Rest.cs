public class Rest : Action {

    public Rest() {
        this.name = "Rest";
        this.description = "Recover 3 HP.";
        this.actionType = ActionType.REST;
        this.healing = 3;
        this.targetting = TargetCategory.NONE;
    }

    // For now, nothing special.
    public override bool canUse() {
        return base.canUse();
    }

    public override bool use(Entity? target, Modifier? modifier) {
        if(owner == null) {
            Console.WriteLine("ERROR: no owner for action!");
            return false;
        }
        // Restore HP.
        owner.ReceiveHealing(healing);
        return base.use(target, modifier);
    }
}