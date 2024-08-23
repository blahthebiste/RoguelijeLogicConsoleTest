public class Pengoon : Enemy {

    public Pengoon() {
        this.name = DataRegistry.EnemyData.Pengoon.Name;
        this.maxHP = DataRegistry.EnemyData.Pengoon.HP;
        this.hostile = true;
        this.playerControlled = false;
        this.currentHP = maxHP;
        this.exhausted = false;
        foreach(Action action in DataRegistry.EnemyData.Pengoon.ActionList) {
            this.ActionList.Add(action);
        }
        this.assignActionOwnership();
    }

}