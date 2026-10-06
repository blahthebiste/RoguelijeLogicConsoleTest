public class Engage : ActionCard {

    public Engage() {
        this.name = this.originalName = "Engage";
        this.description = this.originalDescription = "Perform an attack action. Draw 2 cards.";
        this.actionType = ActionType.ATTACK;
        this.owner = null;
        this.modifier = null;
    }

    public override bool AfterUse(Entity entityToUseAction, Entity? target, Action hoveredAction)
    {
        // On successful use, draw 2
        Console.WriteLine("Drawing 2 cards");
        CardManager.drawCard(2);
        return base.AfterUse(entityToUseAction, target, hoveredAction);      
    }
}