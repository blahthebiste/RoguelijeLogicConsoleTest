public class Recover : Action {

    public Recover() {
        this.name = "Recover";
        this.actionType = ActionType.REST;
        this.healing = 5;
        this.magicNumber = 3;
        this.targetting = TargetCategory.SELF;
        this.description = "Recover "+healing+" HP. Draw "+magicNumber+" cards.";
    }



    public override bool useOnce() {
        if (this.owner == null) {
            Console.WriteLine("ERROR: null owner for action '"+this+"'.");
            return false;
        }
        // Restore HP.
        this.owner.ReceiveHealing(properties.healing);
        // Draw cards.
        Console.WriteLine("Drawing 3 cards");
        CardManager.drawCard(3);
        return true;
    }
}