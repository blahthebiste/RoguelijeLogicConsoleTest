public class Climb : Action {

    public Climb() {
        this.name = "Climb";
        this.actionType = ActionType.SKILL;
        this.magicNumber = 2;
        this.targetting = TargetCategory.NONE;
        this.description = "Draw "+magicNumber+" cards.";
    }

    public override bool useOnce(Modifier? modifier) {
        // Draw 2
        Console.WriteLine("Drew "+magicNumber+" cards.");
        CardManager.drawCard(this.magicNumber);
        return true;
    }
}