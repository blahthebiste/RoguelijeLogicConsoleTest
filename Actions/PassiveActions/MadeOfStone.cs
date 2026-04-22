public class MadeOfStone : Action {

    public MadeOfStone() {
        this.name = "Made of Stone";
        this.description = "Cannot lose more than 1 HP at a time.";
        this.actionType = ActionType.PASSIVE;
    }

    // Prevent HP loss
    public override int onHPChange(int HPdelta)
    {
        if(HPdelta > 1) return 1;
        return HPdelta;
    }
}