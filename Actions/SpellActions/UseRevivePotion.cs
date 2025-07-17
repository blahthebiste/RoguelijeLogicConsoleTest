public class UseRevivePotion : Action {

    public UseRevivePotion() {
        this.name = "Use Revive Potion";
        this.description = "Resurrect an ally to full HP.";
        this.targetting = TargetCategory.DEAD_ALLY;
        this.actionType = ActionType.SPELL;
        this.hasLimitedUses = true;
        this.uses = 1;
        this.maxUses = this.uses;
    }


    public override bool useOnTarget(Entity? target, Modifier? modifier) {
        if (target == null) {
            Console.WriteLine("ERROR: null target for action '"+this+"'.");
            return false;
        }
        // Bring them back into the fight
        return Battlefield.ReviveHero(target.name, true, true);
    }
}