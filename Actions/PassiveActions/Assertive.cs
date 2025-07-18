public class Assertive : Action {

    Taunt tauntInstance;
    public Assertive()
    {
        this.name = "Assertive";
        this.description = "Uses Taunt before turn 1.";
        this.actionType = ActionType.PASSIVE;
        tauntInstance = new Taunt();
    }

    public override void startOfCombat()
    {
        if(this.owner == null) {
            Console.WriteLine("ERROR: "+this.name+" has null owner!");
            return;
        }
        tauntInstance.owner = this.owner;
        tauntInstance.use(this.owner!, null);
    }
}