public class Recover : Action {

    public Recover() {
        this.name = "Recover";
        this.description = "Recover 5 HP. Draw 3 cards.";
        this.actionType = ActionType.REST;
        this.healing = 5;
        this.magicNumber = 3;
        this.targetting = TargetCategory.SELF;
    }

    public override bool useOnTarget(Entity? target, Modifier? modifier) {
        // Restore HP.
        target!.ReceiveHealing(healing);
        return true;
    }

    public override bool useOnce(Modifier? modifier){
        // Draw cards.
        Console.WriteLine("Drawing 3 cards");
        CardManager.drawCard(3);
        return true;
    }
}