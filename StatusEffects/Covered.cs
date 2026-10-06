public class Covered : StatusEffect {

    public Entity origin;

    public Covered(int amount, Entity owner, Entity origin) {
        this.amount = amount;
        this.name = "Covered";
        this.description = "Attackers take that much damage back for 1 turn.";
        this.owner = owner;
        this.origin= origin;
    }

    public override Attack onReceiveAttack(Attack atk) {
        // Origin entity makes an attack on the attacker:
        Attack responseAtk = new Attack(amount, origin, atk.source);
        Battlefield.performAttack(responseAtk);
        return base.onAttack(atk);
    }

    // Remove at the start of each round
    public override void startOfRound() {
        Console.WriteLine(this.name+" wore off!");
        owner.RemoveStatusEffectByName(this.name);
    }
}