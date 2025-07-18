public class OnGuard : Action {

    Parry parryInstance;
    public OnGuard()
    {
        this.name = "On-Guard";
        this.description = "Uses Parry before turn 1.";
        this.actionType = ActionType.PASSIVE;
        parryInstance = new Parry();
    }

    public override void startOfCombat()
    {
        if(this.owner == null) {
            Console.WriteLine("ERROR: "+this.name+" has null owner!");
            return;
        }
        parryInstance.owner = this.owner;
        parryInstance.use(this.owner!, null);
    }
}