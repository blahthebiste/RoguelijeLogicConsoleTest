public class Focus : Action {

    public Focus() {
        this.name = "Focus";
        this.description = "Regain all Spell uses.";
        this.actionType = ActionType.REST;
        this.targetting = TargetCategory.SELF;
    }

    public override bool useOnTarget(Entity? target, Modifier? modifier) {
        // Restore spell uses.
        foreach(Action action in target!.ActionList) {
            if(action.hasLimitedUses) {
                Console.WriteLine("Regaining uses for "+action.name+" up to "+action.maxUses);
                action.uses = action.maxUses;
            }
        }
        return true;
    }
}