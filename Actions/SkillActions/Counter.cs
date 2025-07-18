public class Counter : Action {

    public Counter() {
        this.name = "Counter";
        this.description = "Strike all enemies who attack you this turn.";
        this.actionType = ActionType.SKILL;
        this.magicNumber = 1;
        this.targetting = TargetCategory.SELF;
    }


    public override bool useOnce(Modifier? modifier) {
        if (this.owner == null) {
            Console.WriteLine("ERROR: null owner for action '"+this+"'.");
            return false;
        }
        // Apply the Counter status effect
        this.owner.AddStatusEffect(new Countering(magicNumber, this.owner));
        return true;
    }
    
}