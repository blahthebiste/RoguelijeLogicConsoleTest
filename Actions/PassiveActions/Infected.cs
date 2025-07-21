public class Infected : Action {

    public Infected() {
        this.name = "Infected";
        this.description = "On death, apply 3 Poison to the attacker.";
        this.actionType = ActionType.PASSIVE;
        this.magicNumber = 3;
    }

    public override Attack onReceiveAttack(Attack atk)
    {
        if (this.owner == null) {
            Console.WriteLine("ERROR: " + this.name + " has null owner!");
            return atk;
        }
        if (atk.source == null) {
            Console.WriteLine("ERROR: attack has null source!");
            return atk;
        }
        if (atk.damage >= this.owner.currentHP)
        {
            Console.WriteLine(this.owner.name + " infects " + atk.source.name + " with Poison(" + magicNumber + ") upon death!");
            atk.source.AddStatusEffect(new Poison(magicNumber, atk.source));
        }            
        return atk; 
    }
}