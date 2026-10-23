public class FleesWhenAlone : Action {

    public FleesWhenAlone()
    {
        this.name = "FleesWhenAlone";
        this.description = "This entity will flee if alone.";
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
        if(owner.hostile && Battlefield.EnemySide.Count < 2)
        {
            // Could not find allies, disappear!
            Console.WriteLine(this.owner.name + " is all alone. Fleeing!");
            this.owner.flee();
        }
        if(!owner.hostile && Battlefield.PlayerSide.Count < 2)
        {
            // Could not find allies, disappear!
            Console.WriteLine(this.owner.name + " is all alone. Fleeing!");
            this.owner.flee();
        }        
        return;
    }
}