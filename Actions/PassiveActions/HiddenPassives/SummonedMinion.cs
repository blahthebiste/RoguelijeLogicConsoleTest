public class SummonedMinion : Action {

    public SummonedMinion()
    {
        this.name = "SummonedMinion";
        this.description = "This entity will die whenever the entity that summoned them dies or flees.";
        this.actionType = ActionType.PASSIVE;
        this.hiddenAction = true;
    }

    // Check whether the master is still alive.
    public override void startOfTurn()
    {
        if (this.owner == null)
        {
            Console.WriteLine("ERROR: " + this.name + " has null owner!");
            return;
        }
        if (((Enemy)this.owner).master == null)
        {
            Console.WriteLine("ERROR: " + this.name + " has null master!");
            return;
        }
        foreach (Enemy ally in Battlefield.EnemySide.ToList())
        {
            if (ally.name.ToLower().Trim() == ((Enemy)this.owner).master!.ToLower().Trim())
            {
                // Master is still here, chill
                Console.WriteLine(this.owner.name + "'s master is alive and well.");
                return;
            }
        }
        foreach (PlayerCharacter ally in Battlefield.PlayerSide.ToList())
        {
            if (ally.name.ToLower().Trim() == ((Enemy)this.owner).master!.ToLower().Trim())
            {
                // Master is still here, chill
                Console.WriteLine(this.owner.name + "'s master is alive and well.");
                return;
            }
        }
        // Could not find master, disappear!
        Console.WriteLine(this.owner.name + "'s master is gone!");
        this.owner.die();
        return;
    }
}