public class Tomahawk : EquipmentItem {

    public static int damage = 3;
    public bool used;

    public Tomahawk()
    {
        this.name = "Tomahawk";
        this.description = "The first time you use your Skill action, deal 3 damage to a random enemy.";
        this.slot = ActionType.SKILL;
        this.price = 65;
        used = false;
        this.tier = 1;
    }

    // On action use:
    public override Action onUseEquippedAction(Action actionBeingUsed) {
        // If this has not activated yet, use it
        if(!used) {
            used = true;
            Console.WriteLine("Tomahawk deals damage");
            Entity randomTarget = Battlefield.EnemySide[CurrentRun.rng.Next(0, Battlefield.EnemySide.Count)];
            // Deal damage to the target.
            Attack atk = new Attack(damage, this.parentAction!.owner!, randomTarget!);
            randomTarget!.onReceiveAttack(atk);
        }
        return actionBeingUsed;
    }

}