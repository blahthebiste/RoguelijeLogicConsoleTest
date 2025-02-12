public class Climb : Action {

    public Climb() {
        this.name = "Climb";
        this.description = "Draw 2 cards.";
        this.actionType = ActionType.SKILL;
        this.magicNumber = 2;
        this.targetting = TargetCategory.NONE;
    }

    // For now, nothing special.
    public override bool canUse(Entity? target, Modifier? modifier) {
        return base.canUse(target, modifier);
    }

    public override bool use(Entity? target, Modifier? modifier) {
        if(base.use(target, modifier)) {
            // Draw 2
            Console.WriteLine("Drew 2 cards.");
            CardManager.drawCard(this.magicNumber);
            return true;
        }
        return false;
    }
}