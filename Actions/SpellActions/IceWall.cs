public class IceWall : Action {

    public IceWall() {
        this.name = "Ice Wall";
        this.description = "Generate 18 Block.";
        this.actionType = ActionType.SPELL;
        this.magicNumber = 18;
        this.hasLimitedUses = true;
        this.uses = 2;
        this.maxUses = this.uses;
        this.targetting = TargetCategory.NONE;
    }

    public override bool use(Entity? target, Modifier? modifier) {
        if(base.use(target, modifier)) {           
            if(owner!.HasStatusEffect("Charged")) {
                StatusEffect charge = owner.GetStatusEffect("Charged")!;
                magicNumber += charge.amount;
                owner.EffectList.Remove(charge);
            }
            // Generate Block. (Don't trigger onGainBlock since this is a spell?)
            Console.WriteLine("Generated "+this.magicNumber+" Block.");
            Battlefield.addBlock(this.magicNumber, true);
            return true;
        }
        return false;
    }
}