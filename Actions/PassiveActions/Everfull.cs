public class Everfull : Action {

    public Everfull()
    {
        this.name = "Everfull";
        this.description = "Witch's spells have unlimited uses.";
        this.actionType = ActionType.PASSIVE;
    }

    // Refill all spell uses for all allies (witch)
    public override void startOfRound()
    {
        if (this.owner == null)
        {
            Console.WriteLine("ERROR: " + this.name + " has null owner!");
            return;
        }
        foreach(Enemy ally in Battlefield.EnemySide.ToList()) {
            foreach (Action act in ally.ActionListMinusPassives) {
                if (act.actionType == ActionType.SPELL && act.uses < act.maxUses)
                {
                    Console.WriteLine("Refilling spell uses for "+act.name);
                    act.uses = act.maxUses;
                }
            }
        }
    }
}