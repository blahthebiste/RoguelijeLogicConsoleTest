public class Bat : Enemy {

    public Bat() {
        this.name = "Bat";
        this.description = "This bat seems bewitched to attack anything its master deems a threat.";
        this.maxHP = 3;
        this.hostile = true;
        this.playerControlled = false;
        this.currentHP = maxHP;
        this.ActionList.Add(new Swipe());
        this.assignActionOwnership();
    }

}