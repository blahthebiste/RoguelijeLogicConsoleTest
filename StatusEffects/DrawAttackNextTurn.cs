public class DrawAttackNextTurn : StatusEffect {

    
    public DrawAttackNextTurn(int amount, Entity owner) {
        this.amount = amount;
        this.name = "Draw Attack Next Turn";
        this.description = "At the start of your turn, draw an Attack card, for that many turns.";
        this.owner = owner;
    }

    // Decrement every turn
    public override void startOfTurn() {
        // Draw an attack card:
        CardManager.drawCardOfType(ActionType.ATTACK);
        this.Decrease(1);
    }
}