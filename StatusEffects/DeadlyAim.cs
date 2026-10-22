public class DeadlyAim : StatusEffect {

    public List<string> AffectedList = Action.AttacksWithDamage;

    public DeadlyAim(int amount, Entity owner) {
        this.amount = amount;
        this.name = "Deadly Aim";
        this.description = "Next X attacks deal double damage.";
        this.owner = owner;
        this.AffectedList.Add("Rain Of Arrows");
    }

    // New version: only affect actions within the list. Modifies the action in place until it resolves.
    public override Action onUseAction(Action actionBeingUsed)
    {
        if(AffectedList.Contains(actionBeingUsed.name))
        {
            actionBeingUsed.damage *= 2;
            Console.WriteLine("DEBUG: apply DeadlyAim bonus");
        }
        return actionBeingUsed;
    }

    public override void onActionResolved(Action actionUsed)
    {
        if(AffectedList.Contains(actionUsed.name) && actionUsed.owner == this.owner)
        {
            actionUsed.damage /= 2;
            Console.WriteLine(actionUsed.name+" used up "+this.name+".");
            this.Decrease(amount);
        }
        base.onActionResolved(actionUsed);
    }
}