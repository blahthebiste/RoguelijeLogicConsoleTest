public class Injured : Action {

    public Injured() {
        this.name = "Injured";
        this.actionType = ActionType.PASSIVE;
        this.magicNumber = 1;
        this.description = "Start each combat Bleeding.";
    }

    public override void startOfCombat()
    {
        if(this.owner == null) {
            Console.WriteLine("ERROR: "+this.name+" has null owner!");
            return;
        }
        this.owner!.AddStatusEffect(new Bleed(1, this.owner!));
    }
}