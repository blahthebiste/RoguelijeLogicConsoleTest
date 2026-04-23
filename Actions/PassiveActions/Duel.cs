public class Duel : Action
{

    // Track the target that is locked onto
    public Entity? lockedTarget;

    public Duel()
    {
        this.name = "Duel";
        this.description = "Enters a duel with the next hero that acts, locking them into single combat.";
        this.actionType = ActionType.PASSIVE;
        lockedTarget = null;
    }
    
    // Triggered every time an enemy acts
    public override Action onEnemyUsedAction(Action actionBeingUsed)
    {// Check if the action was used by a hero. If so, they become our locked target
        if (lockedTarget != null)
        {
            Console.WriteLine("Not locking; already locked to "+lockedTarget.name);
            return actionBeingUsed;
        }
        if (actionBeingUsed.owner == null)
        {
            Console.WriteLine("Not locking; action being used has null owner");
            return actionBeingUsed;
        }
        if (this.owner == null)
        {
            Console.WriteLine("Not locking; Duel action has null owner");
            return actionBeingUsed;
        }
        if (Battlefield.PlayerSide.Contains(actionBeingUsed.owner) && !actionBeingUsed.owner.HasStatusEffect("Locked"))
        {
            // Yes, they are a hero, and no, they are not already locked. Lock them.
            Console.WriteLine(this.owner.name + " has locked " + actionBeingUsed.owner.name + " in single combat!");
            actionBeingUsed.owner.AddStatusEffect(new Locked(99, actionBeingUsed.owner, this.owner));
            this.owner.AddStatusEffect(new Locked(99, this.owner, actionBeingUsed.owner));
            lockedTarget = actionBeingUsed.owner;
        }
        return actionBeingUsed;
    }

    // Remove the lock from both parties on death.
    public override void onDeath()
    {
        if (this.owner == null)
        {
            Console.WriteLine("ERROR: Duel action has null owner!");
            return;
        }
        if (lockedTarget != null)
        {
            Console.WriteLine(this.owner.name + " has died; removing lock from " + lockedTarget.name + ".");
            lockedTarget.RemoveStatusEffectByName("Locked");
        }
        this.owner.RemoveStatusEffectByName("Locked");
    }
}