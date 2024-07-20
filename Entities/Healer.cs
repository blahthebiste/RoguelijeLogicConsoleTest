public class Healer : PlayerCharacter {

    public Healer() {
        this.name = DataRegistry.CharacterData.Healer.Name;
        this.maxHP = DataRegistry.CharacterData.Healer.HP;
        this.hostile = false;
        this.playerControlled = true;
        this.currentHP = maxHP;
        this.exhausted = false;
        this.personalCard = DataRegistry.CharacterData.Healer.PersonalCard;
        foreach(Action action in DataRegistry.CharacterData.Healer.ActionList) {
            this.ActionList.Add(action);
        }
    }

}