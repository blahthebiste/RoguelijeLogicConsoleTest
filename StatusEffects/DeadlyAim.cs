public class DeadlyAim : StatusEffect {

    public DeadlyAim(int amount, Entity owner) {
        this.amount = amount;
        this.name = "Deadly Aim";
        this.description = "Next X attacks deal double damage.";
        this.owner = owner;
    }

    // Actual code is done on a per-action basis

}