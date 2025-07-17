public class MistWolf : Enemy {

    public MistWolf() {
        this.name = "Mist Wolf";
        this.description = "This bloodthirsty beast lurks in the dark, waiting to devour anyone caught unawares.";
        this.maxHP = 30;
        this.hostile = true;
        this.playerControlled = false;
        this.currentHP = maxHP;
        this.ActionList.Add(new Tough());
        this.ActionList.Add(new Bite());
        this.ActionList.Add(new Vanish());
        this.ActionList.Add(new Ravage());
        this.ActionList.Add(new Devour());
        this.assignActionOwnership();
    }

}