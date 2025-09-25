public class TargetLock : Action
{

    // Track the target that is locked onto
    public Entity lockedTarget;

    public TargetLock()
    {
        this.name = "Target Lock";
        this.description = "Locks onto the next hero that acts. That hero, and this enemy, can only target each other. (Only 1 Target Lock triggers per action.)";
        this.actionType = ActionType.PASSIVE;
        lockedTarget = null;
    }
    
    // Triggered every time an entity acts
    public override Action onUseAction(Action actionBeingUsed)
    {// Check if the action was used by a hero. If so, they become our locked target
        // TODO
        return actionBeingUsed;
    }

}