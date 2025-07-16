public class Counter : Action {

    public Counter() {
        this.name = "Counter";
        this.description = "Strike all enemies who attack you this turn.";
        this.actionType = ActionType.SKILL;
        this.magicNumber = 1;
        this.targetting = TargetCategory.SELF;
    }


    public override bool useOnTarget(Entity? target, Modifier? modifier) {
        // Apply the Counter status effect
        target!.AddStatusEffect(new Countering(magicNumber, target));
        return true;
    }
    
}