public class PermaTaunt : Action {

    Taunt tauntInstance;
    public PermaTaunt()
    {
        this.name = "Perma-Taunt";
        this.description = "Always Taunting.";
        this.actionType = ActionType.PASSIVE;
        tauntInstance = new Taunt();
    }

    public override void startOfRound()
    {
        if(this.owner == null) {
            Console.WriteLine("ERROR: "+this.name+" has null owner!");
            return;
        }
        tauntInstance.owner = this.owner;
        tauntInstance.use(this.owner!, null);
    }
}