// Loads data from data files on startup.
public static class DataRegistry {

    public static List<Entity> TroupeData = new List<Entity>();
    public static void fillCharacterData(PlayerCharacter pc, string characterID) {
        // TODO: get info from data
        pc.name = "data.name";
        pc.maxHP = 1;
        pc.personalCard = new BasicAttack();
        // foreach(Action action in data.ActionList) {
        //     pc.ActionList.Add(action);
        // }
    }

    public static void fillEnemyData(Enemy enemy, string enemyID) {
        // TODO: get info from data
        enemy.name = "data.name";
        enemy.maxHP = 1;
        // foreach(Action action in data.ActionList) {
        //     enemy.ActionList.Add(action);
        // }
    }

    public static void loadTroupeData(string combatID) {
        // TODO
    }
}
