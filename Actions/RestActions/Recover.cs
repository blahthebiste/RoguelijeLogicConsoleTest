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

    public override bool use(Entity? target, Modifier? modifier) {
        if(owner == null) {
            Console.WriteLine("ERROR: no owner for action!");
            return false;
        }
        // Restore HP.
        owner.ReceiveHealing(healing);
        // Draw cards.
        CardManager.drawCard(3);
        return base.use(target, modifier);
    }
}