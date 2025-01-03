public class Focus : Action {

    public Focus() {
        this.name = "Focus";
        this.description = "Regain all Spell uses.";
        this.actionType = ActionType.REST;
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
        // Restore spell uses.
        foreach(Action action in owner.ActionList) {
            if(action.hasLimitedUses) {
                Console.WriteLine("Regaining uses for "+action.name+" up to "+action.maxUses);
                action.uses = action.maxUses;
            }
        }
        return base.use(target, modifier);
    }
}