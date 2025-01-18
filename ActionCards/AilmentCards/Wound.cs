public class Wound : ActionCard {

    public Wound() {
        this.name = "Wound";
        this.description = "Does nothing.";
        this.actionType = ActionType.AILMENT;
        this.owner = null;
        this.modifier = null;
    }

}