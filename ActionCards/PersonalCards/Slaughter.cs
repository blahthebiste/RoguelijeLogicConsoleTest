public class Slaughter : ActionCard {

    int bonusStrength;
    public Slaughter() {
        this.name = this.originalName = "Slaughter";
        this.description = this.originalDescription = "Perform an attack action, with bonus damage equal to missing HP";
        this.actionType = ActionType.ATTACK;
        this.owner = null;
        this.modifier = null;
        this.bonusStrength = 0;
    }


    public override bool BeforeUse(Entity entityToUseAction, Entity? target, Action hoveredAction)
    {
        bonusStrength = entityToUseAction.maxHP - entityToUseAction.currentHP;
        if(bonusStrength < 0)
        {
            bonusStrength = 0; // Clamp minimum to 0
        }
        if(bonusStrength > entityToUseAction.maxHP)
        {
            bonusStrength = entityToUseAction.maxHP; // Clamp maximum to entity's max HP
        }
        entityToUseAction.AddStatusEffect(new Strength(bonusStrength, entityToUseAction));
        return true;
    }

    public override bool AfterUse(Entity entityToUseAction, Entity? target, Action hoveredAction)
    {
        entityToUseAction.AddStatusEffect(new Strength(-bonusStrength, entityToUseAction));
        bonusStrength = 0; // Reset bonus
        return true;
    }
}