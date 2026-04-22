public class MirrorArmor : Action
{

    public MirrorArmor()
    {
        this.name = "Mirror Armor";
        this.description = "Gain +2 Toughness. Reflect attacks for 99 turn.";
        this.actionType = ActionType.SPELL;
        this.magicNumber = 2;
        this.magicNumber2 = 99;
        this.hasLimitedUses = true;
        this.uses = 1;
        this.maxUses = this.uses;
        this.targetting = TargetCategory.SELF;
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