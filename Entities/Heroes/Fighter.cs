public class Fighter : PlayerCharacter {

    public Fighter() {
        this.name = DataRegistry.CharacterData.Fighter.Name;
        this.maxHP = DataRegistry.CharacterData.Fighter.HP;
        this.hostile = false;
        this.playerControlled = true;
        this.currentHP = maxHP;
        this.exhausted = false;
        this.personalCard = DataRegistry.CharacterData.Fighter.PersonalCard;
        foreach(Action action in DataRegistry.CharacterData.Fighter.ActionList) {
            this.ActionList.Add(action);
        }
        this.assignActionOwnership();
    }

}