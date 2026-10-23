public class DrawSkillNextTurn : StatusEffect {

    
    public DrawSkillNextTurn(int amount, Entity owner) {
        this.amount = amount;
        this.name = "Draw Skill Next Turn";
        this.description = "At the start of your turn, draw a Skill card, for that many turns.";
        this.owner = owner;
    }

    // Decrement every turn
    public override void startOfTurn() {
        // Draw an attack card:
        CardManager.drawCardOfType(ActionType.SKILL);
        this.Decrease(1);
    }
}