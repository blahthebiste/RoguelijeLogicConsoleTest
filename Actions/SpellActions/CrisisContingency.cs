public class CrisisContingency : Action {

    public CrisisContingency() {
        this.name = "Crisis Contingency";
        this.actionType = ActionType.SPELL;
        this.hasLimitedUses = true;
        this.uses = 3;
        this.magicNumber = 6;
        this.maxUses = this.uses;
        this.targetting = TargetCategory.DEAD_ALLY;
        this.description = "Resurrect a member of the King's Court. Cannot be used until turn "+magicNumber+".";
    }

    // Cannot use this action before turn 6
    public override bool canUse(Entity? target, Modifier? modifier)
    {
        if (Battlefield.turnNumber < this.magicNumber && this.owner != null)
        {
            Console.WriteLine(this.owner.name + " cannot use " + this.name + " until turn "+magicNumber+"!");
            return false;
        }
        return base.canUse(target, modifier);
    }

    public override bool CanTarget(Entity target)
    {
        // Check if the target is a member of the King's Court:
        if (target.name != "Hand of the King" && target.name != "Royal Sorcerer" && target.name != "Deadeye Assassin")
        {
            Console.WriteLine(this.name + " must target a member of the King's Court!");
            return false;    
        }
        // Base version of canUse will handle dead targeting for us
        return base.canUse(target, null);
    }

    public override bool useOnTarget(Entity? target, Modifier? modifier)
    {
        if (target == null)
        {
            Console.WriteLine("ERROR: null target for action '" + this + "'.");
            return false;
        }
        // Bring them back into the fight (need to use ReviveEnemy accordingly!)
        return Battlefield.ReviveEnemy(target.name, true, true) || Battlefield.ReviveHero(target.name, true, true);
    }
}