public class MadeOfStone : Action {

    public MadeOfStone() {
        this.name = "Made of Stone";
        this.magicNumber = 1;
        this.description = "Cannot lose more than "+magicNumber+" HP at a time.";
        this.actionType = ActionType.PASSIVE;
    }

    // Prevent HP loss
    public override int onHPChange(int HPdelta)
    {
        if(HPdelta > magicNumber) return magicNumber;
        return HPdelta;
    }
}