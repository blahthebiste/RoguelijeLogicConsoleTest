public class Pavise : EquipmentItem {

    public static int damage = 2;

    public Pavise() {
        this.name = "Pavise";
        this.description = "When you use your Defend action, deal 2 damage to a random enemy.";
        this.slot = ActionType.DEFEND;
        this.price = 65;
    }

    // On action use:
    public override Action onUseEquippedAction(Action actionBeingUsed) {
        Console.WriteLine("Pavise deals damage");
        Enemy randomTarget = Battlefield.EnemySide[CurrentRun.rng.Next(0, Battlefield.EnemySide.Count)];
        // Deal damage to the target.
        Attack atk = new Attack(damage, this.parentAction!.owner!, randomTarget!);
        randomTarget!.onReceiveAttack(atk);
        return actionBeingUsed;
    }

}