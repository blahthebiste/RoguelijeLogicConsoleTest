public class Pengoon : Enemy {

    public Pengoon() {
        this.name = "Pengoon";
        this.description = "\"What is my purpose?\" \"You are part of the tutorial.\" \"Oh god!\"";
        this.maxHP = 7;
        this.hostile = true;
        this.playerControlled = false;
        this.currentHP = maxHP;
        this.ActionList.Add(new Swipe());
        this.assignActionOwnership();
    }

}