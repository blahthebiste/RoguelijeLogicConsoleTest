public class Warding : Action {

    Forcefield forcefieldInstance;
    public Warding()
    {
        this.name = "Warding";
        this.description = "Uses Force Field before turn 1.";
        this.actionType = ActionType.PASSIVE;
        forcefieldInstance = new Forcefield();
    }

    public override void startOfCombat()
    {
        if(this.owner == null) {
            Console.WriteLine("ERROR: "+this.name+" has null owner!");
            return;
        }
        forcefieldInstance.owner = this.owner;
        forcefieldInstance.use(this.owner, null);
    }
}