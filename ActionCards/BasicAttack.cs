public class BasicAttack : ActionCard {

    public BasicAttack() {
        this.name = "Basic Attack";
        this.description = "Perform an attack action.";
        this.actionType = ActionType.ATTACK;
        this.owner = null;
        this.modifier = null;
    }

}