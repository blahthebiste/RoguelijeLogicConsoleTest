public class Intimidated : StatusEffect
{


    public Intimidated(int amount, Entity owner)
    {
        this.amount = amount;
        this.name = "Intimidated";
        this.description = "Temporary Strength-down.";
        this.owner = owner;
        this.isDebuff = true;
    }

    // Intimidated saps attack damage
    public override Attack onAttack(Attack atk)
    {
        atk.damage -= this.amount;
        return atk;
    }

    // ...and block gain.
    public override int onGainBlock(int block)
    {
        block -= this.amount;
        if (block < 0) block = 0;
        return block;
    }
    
    // Remove Intimidated when at the start of each round
    public override void startOfRound() {
        Console.WriteLine(this.name+" wore off!");
        owner.RemoveStatusEffectByName(this.name);
    }
}