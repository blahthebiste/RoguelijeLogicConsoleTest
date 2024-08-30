public class Pengoon : Enemy {

    public Pengoon() {
        this.name = DataRegistry.EnemyData.PengoonData.Name;
        this.maxHP = DataRegistry.EnemyData.PengoonData.HP;
        this.hostile = true;
        this.playerControlled = false;
        this.currentHP = maxHP;
        this.exhausted = false;
        foreach(Action action in DataRegistry.EnemyData.PengoonData.ActionList) {
            this.ActionList.Add(action);
        }
        this.assignActionOwnership();
    }

}