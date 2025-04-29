public class Climb : Action {

    public Climb() {
        this.name = "Climb";
        this.description = "Draw 2 cards.";
        this.actionType = ActionType.SKILL;
        this.magicNumber = 2;
        this.targetting = TargetCategory.NONE;
    }

    public override bool useOnce(Modifier? modifier) {
        // Draw 2
        Console.WriteLine("Drew 2 cards.");
        CardManager.drawCard(this.magicNumber);
        return true;
    }
}