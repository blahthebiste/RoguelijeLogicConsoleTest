public class Coordinate : ActionCard {

    public Coordinate() {
        this.name = this.originalName = "Coordinate";
        this.description = this.originalDescription = "Perform a skill action. Draw 2 cards.";
        this.actionType = ActionType.SKILL;
    }

    public override bool AfterUse(Entity entityToUseAction, Entity? target, Action hoveredAction)
    {
        // On successful use, draw 2
        Console.WriteLine("Drawing 2 cards");
        CardManager.drawCard(2);
        return base.AfterUse(entityToUseAction, target, hoveredAction);      
    }
}