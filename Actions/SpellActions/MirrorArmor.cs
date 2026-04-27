public class MirrorArmor : Action
{

    public MirrorArmor()
    {
        this.name = "Mirror Armor";
        this.actionType = ActionType.SPELL;
        this.magicNumber = 2;
        this.magicNumber2 = 99;
        this.hasLimitedUses = true;
        this.uses = 1;
        this.maxUses = this.uses;
        this.targetting = TargetCategory.SELF;
        this.description = "Gain +"+magicNumber+" Toughness. Reflect attacks for "+magicNumber2+" turn.";
    }


    public override bool useOnce(Modifier? modifier) {
        if (this.owner == null) {
            Console.WriteLine("ERROR: null owner for action '"+this+"'.");
            return false;
        }
        // Add Toughness status:
        owner.AddStatusEffect(new Toughness(magicNumber, owner));
        // Add Reflect status:
        owner.AddStatusEffect(new ReflectAttacks(magicNumber2, owner));
        return true;
    }    
}