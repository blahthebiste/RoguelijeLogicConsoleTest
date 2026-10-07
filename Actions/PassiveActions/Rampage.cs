public class Rampage : Action {

    public Rampage() {
        this.name = "Rampage";
        this.actionType = ActionType.PASSIVE;
        this.description = "Excess attack damage hits another random target.";
    }

    public override Attack onAttack(Attack atk)
    {
        if (this.owner == null) {
            Console.WriteLine("ERROR: " + this.name + " has null owner!");
            return atk;
        }
        if (atk.target == null) {
            Console.WriteLine("ERROR: attack has null target!");
            return atk;
        }
        // Apply the rampage victim effect to the attack target. It will remove itself.
        atk.target.AddStatusEffect(new RampageVictim(this.owner, atk.target));            
        return atk; 
    }
}