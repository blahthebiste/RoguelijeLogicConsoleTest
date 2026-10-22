public class PureMod : Modifier {
    
    public PureMod()
    {
        this.name = "Pure";
        this.description = "When played, reduce a debuff on the user.";
    }

    public override void useOnce(Action act)
    {
        if (act.owner == null)
        {
            Console.WriteLine("ERROR: null owner for action passed to modifier " + this.name + "!");
            return;
        }
        List<StatusEffect> debuffList = new List<StatusEffect>();
        foreach (StatusEffect effect in act.owner.EffectList.ToList())
        {
            if (effect.isDebuff)
            {
                debuffList.Add(effect);
            }
        }
        if (debuffList.Count > 0)
        {
            int debuffIndex = CurrentRun.rng.Next(0, debuffList.Count);
            Console.WriteLine("Pure modifier reduced debuff " + debuffList[debuffIndex].name);
            debuffList[debuffIndex].Decrease(1);
        }
        else
        {
            Console.WriteLine("No debuffs found.");
        }
    }
}