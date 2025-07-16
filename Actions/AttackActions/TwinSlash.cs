public class TwinSlash : Action {

    public TwinSlash() {
        this.name = "Twin Slash";
        this.description = "Deal 4 damage twice.";
        this.actionType = ActionType.ATTACK;
        this.damage = 4;
        this.magicNumber = 2; // Hit twice
        this.targetting = TargetCategory.SINGLE_ENEMY;
    }

    public override bool useOnTarget(Entity? target, Modifier? modifier) {
        // Deal damage to the target twice.
        for(int i = 0; i < magicNumber; i++) {
            Attack atk = new Attack(damage, this.owner!, target!, this.hitsAbove, this.hitsBelow);
            atk = owner!.onAttack(atk);
            target!.onReceiveAttack(atk);
        }
        return true;
    }
}