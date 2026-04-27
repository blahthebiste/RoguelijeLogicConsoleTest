public class LivingFlame : Action {

    public LivingFlame() {
        this.name = "Living Flame";
        this.actionType = ActionType.SPELL;
        this.magicNumber = 3; // Base damage
        this.magicNumber2 = 1; // Spell power gain
        this.hasLimitedUses = true;
        this.uses = 3;
        this.maxUses = this.uses;
        this.targetting = TargetCategory.ALL_ENEMIES;
        this.description = "Deal "+magicNumber+" damage to all enemies. Gain "+magicNumber2+" Spell Power.";
    }

    public override bool useOnTarget(Entity? target, Modifier? modifier) {
        int power = magicNumber;
        if(owner!.HasStatusEffect("Spell Power")) {
            StatusEffect spell_power = owner.GetStatusEffect("Spell Power")!;
            power += spell_power.amount;
        }
        if(owner!.HasStatusEffect("Charged")) {
            StatusEffect charge = owner.GetStatusEffect("Charged")!;
            power += charge.amount;
            owner.EffectList.Remove(charge);
        }
        // Deal damage to all enemies.
        if(modifier != null) power += modifier.damageMod;
        Attack atk = new Attack(power, this.owner!, target!);
        //atk = owner.onAttack(atk); // Don't trigger onAttack for the owner, since it is a spell?
        target!.onReceiveAttack(atk);
        return true;
    }

    public override bool useOnce(Modifier? modifier){
        if(this.owner != null) {
            //Console.WriteLine("Drawing 3 cards");
            this.owner.AddStatusEffect(new SpellPower(magicNumber2, this.owner));
        }
        return true;
    }
}