public class Daze : Action {

    public Daze() {
        this.name = "Daze";
        this.description = "Stun an enemy that has not been Dazed.";
        this.actionType = ActionType.SKILL;
        this.targetting = TargetCategory.SINGLE_ENEMY;
        this.magicNumber = 1;
    }

    // For now, nothing special.
    public override bool canUse() {
        return base.canUse();
    }

    public override bool CanTarget(Entity target) {
        if(Battlefield.BeenDazed.Contains(target)) {
            Console.WriteLine("That target has already been Dazed!");
            return false;
        }
        return base.CanTarget(target);
    }

    public override void use(Entity? target, Modifier? modifier) {
        // Apply the Stun status effect
        target.recieveStatusEffect(new Stun(magicNumber));
        Battlefield.BeenDazed.Add(target);
    }
}