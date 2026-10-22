public class SlaughterMod : Modifier {

    public SlaughterMod()
    {
        this.name = "SlaughterMod";
        this.description = "Used by the Slaughter card.";
    }


    public override void modifyProperties(ActionProperties prop, Entity? actionOwner)
    {
        if(actionOwner == null)
        {
            return;
        }
        int bonusStrength = actionOwner.maxHP - actionOwner.currentHP;
        if(bonusStrength < 0)
        {
            bonusStrength = 0; // Clamp minimum to 0
        }
        if(bonusStrength > actionOwner.maxHP)
        {
            bonusStrength = actionOwner.maxHP; // Clamp maximum to entity's max HP
        }
        prop.damage += bonusStrength;
    }
}