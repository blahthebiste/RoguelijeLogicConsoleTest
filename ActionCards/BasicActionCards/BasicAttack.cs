public class BasicAttack : ActionCard {

    public BasicAttack() {
        this.name = this.originalName = "Basic Attack";
        this.description = this.originalDescription = "Perform an attack action.";
        this.actionType = ActionType.ATTACK;
        this.owner = null;
        this.modifier = null;
    }

}