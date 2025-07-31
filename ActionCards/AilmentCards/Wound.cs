public class Wound : ActionCard {

    public Wound() {
        this.name = this.originalName = "Wound";
        this.description = this.originalDescription = "Does nothing.";
        this.actionType = ActionType.AILMENT;
        this.owner = null;
        this.modifier = null;
    }

}