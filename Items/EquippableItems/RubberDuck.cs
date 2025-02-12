public class RubberDuck : EquipmentItem {


    public RubberDuck() {
        this.name = "Rubber Duck";
        this.description = "You have exhausted the item pool. Now there are only ducks.";
        this.slot = ActionType.ANY;
        this.price = 5;
    }

    public override void onEquip() {
        base.onEquip();
        Console.WriteLine("QUACK!");
    }


    public override void onUnequip() {
        base.onUnequip();
        Console.WriteLine("QUACK!");
    }

}