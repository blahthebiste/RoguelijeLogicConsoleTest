public class Nock : Action {

    public Nock() {
        this.name = "Nock";
        this.actionType = ActionType.SKILL;
        this.magicNumber = 1;
        this.targetting = TargetCategory.SELF;
        this.description = "Draw an attack card next turn.";
    }

    public override bool useOnce(Modifier? modifier) {
        if (this.owner == null) {
            Console.WriteLine("ERROR: null owner for action '"+this+"'.");
            return false;
        }
        // Apply DrawAttackNextTurn status effect
        this.owner.AddStatusEffect(new DrawAttackNextTurn(magicNumber, this.owner));
        return true;
    }
}