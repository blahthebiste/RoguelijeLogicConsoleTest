public class SturdyMod : Modifier {
    
    public SturdyMod()
    {
        this.name = "Sturdy";
        this.description = "When played, gain Toughness for a turn.";
    }

    public override void useOnce(Action act)
    {
        if (act.owner == null)
        {
            Console.WriteLine("ERROR: null owner for action passed to modifier " + this.name + "!");
            return;
        }
        Console.WriteLine("Gaining Toughness due to Sturdy modifier!");
        Toughness toughEff = new Toughness(1, act.owner);
        WearsOff wearsOffEff = new WearsOff(1, act.owner, toughEff.name);
        act.owner.AddStatusEffect(toughEff);
        act.owner.AddStatusEffect(wearsOffEff);
    }
}