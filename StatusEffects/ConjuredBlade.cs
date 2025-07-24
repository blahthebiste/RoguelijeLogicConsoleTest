public class ConjuredBlade : StatusEffect {

    TwinSlash twinslashInstance;

    public Action? oldAction;
    int actionIndex = -1;

    public ConjuredBlade(int amount, Entity owner)
    {
        this.amount = amount;
        this.name = "Conjured Blade";
        this.description = "Replace Strike with Twin Slash for that many uses.";
        this.owner = owner;
        twinslashInstance = new TwinSlash();
        twinslashInstance.owner = owner;

    }

    public override void onApplied()
    {
        if (this.owner == null)
        {
            return;
        }
        // Search for Strike:
        foreach (Action act in this.owner.ActionListMinusPassives)
        {
            if (act.name == "Strike")
            {
                // Remember the old action
                oldAction = act;
                actionIndex = this.owner.ActionList.IndexOf(oldAction); // Keep the index in the action list
                if (actionIndex == -1)
                {
                    Console.WriteLine("ERROR: " + this.name + " could not find an index for the old action!");
                    return;
                }
                // Replace with new action
                this.owner.ActionList[actionIndex] = twinslashInstance;
                this.owner.assignActionOwnership();
                break;
            }
        }
        base.onApplied();
    }

    public override void onRemoved()
    {
        // Return the strike action
        if (oldAction == null)
        {
            Console.WriteLine("ERROR: " + this.name + " cannot restore old action; old action is null!");
            return;
        }
        if (this.owner == null)
        {
            Console.WriteLine("ERROR: " + this.name + " cannot restore old action -- null owner!");
            return;
        }
        if (actionIndex == -1)
        {
            Console.WriteLine("ERROR: " + this.name + " could not find an index for the old action!");
            return;
        }
        this.owner.ActionList[actionIndex] = oldAction;
        this.owner.assignActionOwnership();
        // Reset values
        actionIndex = -1;
        oldAction = null;
        base.onRemoved();
    }


    public override Action onUseAction(Action act)
    {
        // if used Twin Slash, decrement:
        if (act == twinslashInstance)
        {
            this.Decrease(1); // Wears down by 1
        }
        return act;
    }

}