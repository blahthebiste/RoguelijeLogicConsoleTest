public class Royal : Action {

    public Royal() {
        this.name = "Royal";
        this.description = "Cannot lose HP while the King's Court are alive.";
        this.actionType = ActionType.PASSIVE;
    }

    // Prevent HP loss
    public override int onHPChange(int HPdelta)
    {
        if (this.owner == null)
        {
            Console.WriteLine("ERROR: null owner for action '" + this.name + "'.");
            return false;
        }
        // check if court members are alive:
        foreach (Entity enemy in Battlefield.EnemySide.ToList())
        {
            if (enemy.name == "Hand of the King" || enemy.name == "Royal Sorcerer" || enemy.name == "Deadeye Assassin")
            {
                if (HPdelta < 0)
                {
                    Console.WriteLine(this.owner.name + " cannot lose HP, " + enemy.name + " is still alive!");
                    return 0;
                }
            }
        }
        return HPdelta;
    }
}