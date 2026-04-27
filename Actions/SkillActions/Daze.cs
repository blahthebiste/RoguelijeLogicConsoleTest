public class Daze : Action {

    public Daze() {
        this.name = "Daze";
        this.actionType = ActionType.SKILL;
        this.targetting = TargetCategory.SINGLE_ENEMY;
        this.magicNumber = 1;
        this.description = "Stun an enemy that has not been Dazed.";
    }

    public override bool CanTarget(Entity target) {
        if(Battlefield.BeenDazed.Contains(target)) {
            Console.WriteLine("That target has already been Dazed!");
            return false;
        }
        return base.CanTarget(target);
    }

    public override bool useOnTarget(Entity? target, Modifier? modifier) {
        // Apply the Stun status effect
        target!.AddStatusEffect(new Stun(magicNumber, target));
        Battlefield.BeenDazed.Add(target);
        return true;
    }
}