public class Curse : StatusEffect
{

    public Curse(int amount, Entity owner)
    {
        this.amount = amount;
        this.name = "Curse";
        this.owner = owner;
        this.isDebuff = true;
        this.description = "Take X damage after losing HP.";
    }

    // Trigger damage when HP is lost
    public override int onHPChange(int HPdelta)
    { // If HP is lost:
        if(HPdelta < 0)
        {
            Console.WriteLine(owner.name+" was ravaged by their curse for "+amount+" damage!");
             // Deal damage to the target.
            Attack atk = new Attack(this.amount, this.owner, this.owner);
            // Don't trigger onAttack for the owner, since it is a status effect
            this.owner.onReceiveAttack(atk);
            this.Remove(); // Remove the curse
        }
        return HPdelta;
    }
  
}