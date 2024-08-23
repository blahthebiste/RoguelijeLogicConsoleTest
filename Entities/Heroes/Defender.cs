public class Defender : PlayerCharacter {

    public Defender() {
        this.name = DataRegistry.CharacterData.Defender.Name;
        this.maxHP = DataRegistry.CharacterData.Defender.HP;
        this.hostile = false;
        this.playerControlled = true;
        this.currentHP = maxHP;
        this.exhausted = false;
        this.personalCard = DataRegistry.CharacterData.Defender.PersonalCard;
        foreach(Action action in DataRegistry.CharacterData.Defender.ActionList) {
            this.ActionList.Add(action);
        }
        this.assignActionOwnership();
    }

}