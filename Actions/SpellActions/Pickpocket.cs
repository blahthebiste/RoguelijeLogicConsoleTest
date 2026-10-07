public class Pickpocket : Action {

    int timesUsed;
    public Pickpocket() {
        this.name = "Pickpocket";
        this.actionType = ActionType.SPELL;
        this.hasLimitedUses = true;
        this.uses = 1;
        this.magicNumber = 10;
        this.magicNumber2 = 30;
        this.maxUses = this.uses;
        this.targetting = TargetCategory.NONE;
        this.description = "Steal some money.";
        this.timesUsed = 0;
    }

    public override bool useOnce(Modifier? modifier) {
        if (this.owner == null) {
            Console.WriteLine("ERROR: null owner for action '"+this+"'.");
            return false;
        }
        if(timesUsed > 0)
        {
            Console.WriteLine("Already stole all their money!");
            return false;
        }
        int stolenCash = CurrentRun.rng.Next(magicNumber, magicNumber2+1);
        CurrentRun.Money += stolenCash;
        Console.WriteLine("Stole $"+stolenCash+"!");
        timesUsed++;
        return true;
    }

    public override void startOfCombat()
    {
        timesUsed = 0;
        base.startOfCombat();
    }
}