public class LivingFlame : Action {

    public LivingFlame() {
        this.name = "Living Flame";
        this.actionType = ActionType.SPELL;
        this.damage = 3; // Base damage
        this.magicNumber = 1; // Spell power gain
        this.hasLimitedUses = true;
        this.uses = 3;
        this.maxUses = this.uses;
        this.targetting = TargetCategory.ALL_ENEMIES;
        this.description = "Deal "+damage+" damage to all enemies. Gain "+magicNumber+" Spell Power.";
    }

    public override bool useOnTarget(Entity? target) {
        // Deal damage to all enemies.
        Attack atk = new Attack(properties.damage, this.owner!, target!);
        //atk = owner.onAttack(atk); // Don't trigger onAttack for the owner, since it is a spell?
        target!.onReceiveAttack(atk);
        return true;
    }

    public override bool useOnce(){
        if(this.owner != null) {
            this.owner.AddStatusEffect(new SpellPower(magicNumber, this.owner));
        }
        return true;
    }
}