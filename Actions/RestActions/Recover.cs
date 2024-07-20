public class Recover : Action {

    public Recover() {
        this.name = "Recover";
        this.description = "Recover 5 HP. Draw 3 cards.";
        this.actionType = ActionType.REST;
        this.healing = 5;
        this.magicNumber = 3;
        this.targetting = TargetCategory.NONE;
    }

    // For now, nothing special.
    public override bool canUse() {
        return base.canUse();
    }

    public override void use(Entity? target, Modifier? modifier) {
        // Restore HP.
        owner.recieveHealing(healing);
        // Draw cards.
        CardManager.drawCard(3);
    }
}