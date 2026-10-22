public class Slaughter : ActionCard {

    public Slaughter() {
        this.name = this.originalName = "Slaughter";
        this.description = this.originalDescription = "Perform an attack action, with bonus damage equal to missing HP";
        this.actionType = ActionType.ATTACK;
        this.hiddenModifiers.Add(new SlaughterMod());
    }

}