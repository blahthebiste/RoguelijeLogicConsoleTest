public class Purify : Action {

    public Purify() {
        this.name = "Purify";
        this.actionType = ActionType.SPELL;
        this.healing = 6;
        this.hasLimitedUses = true;
        this.uses = 4;
        this.maxUses = this.uses;
        this.targetting = TargetCategory.SINGLE_ANY;
        this.description = "Restore "+healing+" HP. Cleanse ALL debuffs from the target.";
    }

    public override bool useOnTarget(Entity? target) {
        // Cleanse debuffs
        foreach (StatusEffect eff in target!.EffectList)
        {
            if(eff.isDebuff)
            {
                target.EffectList.Remove(eff);
                Console.WriteLine("Cleansed "+eff.name+" from "+target.name+"!");
            }
        }
        // Apply healing
        target!.ReceiveHealing(properties.healing);
        return true;
    }
}