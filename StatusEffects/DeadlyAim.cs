public class DeadlyAim : StatusEffect {

    public DeadlyAim(int amount, Entity owner) {
        this.amount = amount;
        this.name = "DeadlyAim";
        this.description = "Next X attacks deal double damage.";
        this.owner = owner;
    }

    // Double damage
    public override Attack onAttack(Attack atk) {
        atk.damage *= 2;
        this.Decrease(1); // Wears down by 1
        return base.onAttack(atk);
    }

}