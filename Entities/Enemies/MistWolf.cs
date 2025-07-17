public class MistWolf : Enemy {

    public MistWolf() {
        this.name = "Mist Wolf";
        this.description = "The .";
        this.maxHP = 30;
        this.hostile = true;
        this.playerControlled = false;
        this.currentHP = maxHP;
        this.exhausted = false;
        this.ActionList.Add(new Swipe());
        this.assignActionOwnership();
    }

}