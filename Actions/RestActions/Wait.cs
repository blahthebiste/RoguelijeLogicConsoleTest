public class Wait : Action {

    public Wait() {
        this.name = "Wait";
        this.actionType = ActionType.REST;
        this.targetting = TargetCategory.SELF;
        this.description = "The King waits patiently for his Court to handle this.";
    }

    public override bool useOnce() {
        // Do nothing.
        return true;
    }

    public override bool canUse(Entity? target)
    {
        return true; // You can ALWAYS do nothing!
    }
}