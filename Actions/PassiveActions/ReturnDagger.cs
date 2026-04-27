public class ReturnDagger : Action {

    public ReturnDagger()
    {
        this.name = "Return Dagger";
        this.actionType = ActionType.PASSIVE;
        this.description = "On death, replenish all spell uses to The Dark One.";
    }

    // Refill all spell uses for Dark One
    public override void onDeath()
    {
        if (this.owner == null)
        {
            Console.WriteLine("ERROR: " + this.name + " has null owner!");
            return;
        }
        foreach (Entity enemy in Battlefield.EnemySide.ToList())
        {
            if (enemy.name == "Dark One")
            {
                foreach (Action act in enemy.ActionListMinusPassives)
                {
                    if (act.actionType == ActionType.SPELL && act.uses < act.maxUses)
                    {
                        Console.WriteLine("Refilling spell uses for " + act.name);
                        act.uses = act.maxUses;
                    }
                }
            }            
        }
    }
}