public class TemporaryStrength : StatusEffect {


    public TemporaryStrength(int amount, Entity owner) {
        this.amount = amount;
        this.name = "Temporary Strength";
        this.description = "Increases attack damage and block for the next action.";
        this.owner = owner;
    }

    // Actual code is done on a per-action basis
}