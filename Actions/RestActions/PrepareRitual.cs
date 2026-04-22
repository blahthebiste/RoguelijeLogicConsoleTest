public class PrepareRitual : Action
{

    public PrepareRitual()
    {
        this.name = "Prepare Ritual";
        this.description = "Gain 1 use of a Conjuration Spell if it is missing.";
        this.magicNumber = 1;
        this.actionType = ActionType.REST;
        this.targetting = TargetCategory.SELF;
    }

    public override bool useOnce(Modifier? modifier)
    {
        if (this.owner == null)
        {
            Console.WriteLine("ERROR: null owner for action '" + this.name + "'.");
            return false;
        }
        // Find a spell with "conjure" in the name:
        foreach (Action action in this.owner.ActionList)
        {
            if (action.hasLimitedUses && action.uses < action.maxUses && action.name.Contains("Conjure"))
            { // Only restore a use if one is missing.
                Console.WriteLine("Gaining "+this.magicNumber+" use for " + action.name);
                action.uses++;
            }
        }
        return true;
    }

    // Requires that there is a valid Conjure spell missing a use.
    public override bool canUse(Entity? target, Modifier? modifier)
    {
        if (this.owner == null)
        {
            Console.WriteLine("ERROR: null owner for action '" + this.name + "'.");
            return false;
        }
        bool foundValidSpell = false;
        foreach (Action action in this.owner.ActionList)
        {
            if (action.hasLimitedUses && action.uses < action.maxUses && action.name.Contains("Conjure"))
            {
                foundValidSpell = true;
                break;
            }
        }
        if(!foundValidSpell) {
            Console.WriteLine("No valid spell found for Prepare Ritual.");
            return false;
        }
        return base.canUse(target, modifier);        
    }
    
}