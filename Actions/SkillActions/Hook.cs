public class Hook : Action {

    public Hook() {
        this.name = "Hook";
        this.description = "Return a card to your hand from your discard pile.";
        this.actionType = ActionType.SKILL;
        this.targetting = TargetCategory.NONE;
    }

    public override bool useOnce(Modifier? modifier) {
        Console.WriteLine(this.description);
        return CardManager.retrieveCard();
    }
}