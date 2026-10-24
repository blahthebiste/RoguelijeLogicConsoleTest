public class Curse : StatusEffect
{

    bool triggered;
    public Curse(int amount, Entity owner)
    {
        this.amount = amount;
        this.name = "Curse";
        this.owner = owner;
        this.isDebuff = true;
        this.description = "Take X damage once upon losing HP.";
        this.triggered = false;
    }

    // Trigger damage when HP is lost
    public override int onHPChange(int HPdelta)
    { // If HP is lost:
        if(!triggered && HPdelta < 0)
        {
            triggered = true;
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