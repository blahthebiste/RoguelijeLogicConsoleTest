public class Recover : Action {

    public Recover() {
        this.name = "Recover";
        this.description = "Recover 5 HP. Draw 3 cards.";
        this.actionType = ActionType.REST;
        this.healing = 5;
        this.magicNumber = 3;
        this.targetting = TargetCategory.SELF;
    }



    public override bool useOnce(Modifier? modifier) {
        if (this.owner == null) {
            Console.WriteLine("ERROR: null owner for action '"+this+"'.");
            return false;
        }
        // Restore HP.
        this.owner.ReceiveHealing(healing + (modifier == null? 0 : modifier.healMod));
        // Draw cards.
        Console.WriteLine("Drawing 3 cards");
        CardManager.drawCard(3);
        return true;
    }
}