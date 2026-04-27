public class Hook : Action {

    public Hook() {
        this.name = "Hook";
        this.actionType = ActionType.SKILL;
        this.targetting = TargetCategory.NONE;
        this.description = "Return a card to your hand from your discard pile.";
    }

    public override bool useOnce(Modifier? modifier) {
        Console.WriteLine(this.description);
        return CardManager.retrieveCard();
    }
}