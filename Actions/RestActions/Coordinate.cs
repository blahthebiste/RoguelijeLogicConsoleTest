public class Coordinate : Action {

    public Coordinate() {
        this.name = "Coordinate";
        this.actionType = ActionType.REST;
        this.magicNumber = 3;
        this.targetting = TargetCategory.NONE;
        this.description = "Draw "+magicNumber+" cards.";
    }

    public override bool useOnce(Modifier? modifier) {
        // Draw 3
        Console.WriteLine("Drew "+magicNumber+" cards.");
        CardManager.drawCard(this.magicNumber);
        return true;
    }
}