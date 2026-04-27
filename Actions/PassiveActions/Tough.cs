public class Tough : Action {

    public Tough() {
        this.name = "Tough";
        this.actionType = ActionType.PASSIVE;
        this.magicNumber = 1;
        this.description = "+"+magicNumber+" Toughness.";
    }

    public override void startOfCombat()
    {
        if(this.owner == null) {
            Console.WriteLine("ERROR: "+this.name+" has null owner!");
            return;
        }
        this.owner!.AddStatusEffect(new Toughness(1, this.owner!));
    }
}