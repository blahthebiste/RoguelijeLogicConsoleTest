public class ReturnDagger : Action {

    public ReturnDagger()
    {
        this.name = "Return Dagger";
        this.description = "On death, replenish all spell uses to The Dark One.";
        this.actionType = ActionType.PASSIVE;
    }

    // Refill all spell uses for Dark One
    public override void onDeath()
    {
        if (this.owner == null)
        {
            Console.WriteLine("ERROR: " + this.name + " has null owner!");
            return;
        }
        foreach (Enemy enemy in Battlefield.EnemySide.ToList())
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