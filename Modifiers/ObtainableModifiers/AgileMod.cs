public class AgileMod : Modifier {

    public AgileMod()
    {
        this.name = "Agile";
        this.description = "When played, draw a card.";
    }

    public override void useOnce(Action act)
    {
        Console.WriteLine("Drawing a card due to Agile modifier!");
        CardManager.drawCard(1);
    }
}