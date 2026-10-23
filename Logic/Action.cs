// All actions extend this class.
public class Action : Events {
    
    public string name;
    public string description;
    public ActionType actionType;
	
    public bool hasEquipmentSlot = true;
	public EquipmentItem? equippedItem;
    public bool hasLimitedUses = false;
    public bool ignoresTaunt = false;
    public bool ignoresDodge = false;
    public bool ignoresBlock = false;
    public bool hitsAbove = false;
    public bool hitsBelow = false;
    public bool freeAction = false; // The action does not exhaust the entity using it
    public bool hiddenAction = false; // The action does not appear in action lists.
	public Entity? owner; // The entity that is using the action

	public TargetCategory targetting = TargetCategory.NONE;

	// These lists are cleared after the action is used. They can be modified temporarily by anyone.
	public List<Entity> targetList = new List<Entity>();
	
	// This list tracks which modifiers have already been applied, to prevent applying them more than once.
	// It is reset only after an action resolves.
	public List<Modifier> appliedModifiers = new List<Modifier>();
	


	// Negative 1 means unlimited uses.
	public int uses = -1; // The number of times this action can be used per combat. Usually reserved for spells.
	public int maxUses = -1; // The most uses the action can gain.
	public int damage = -1; // Some actions deal damage. -1 means they do not.
	public double damageMult = 1.0; // Used by modifiers at the end
	public int block = -1; // Some actions gain block. -1 means they do not.
	public double blockMult = 1.0; // Used by modifiers at the end
	public int healing = -1; // Some actions restore HP. -1 means they do not.
	public double healingMult = 1.0; // Used by modifiers at the end
	public int magicNumber = -1; // Can be used for a variety of things. -1 means unused.
	public int magicNumber2 = -1; // Can be used for a variety of things. -1 means unused.
	public int magicNumber3 = -1; // Can be used for a variety of things. -1 means unused.

	
	// Holds the temporary copies of all previous fields.
	// Will be used by any modifiers to modify properties of the action.
	public ActionProperties properties;

	public Action() {
		name = "MISSING NAME";
		description = "MISSING DESCRIPTION";
		properties = new ActionProperties(this);
	}
    
    // Whether this action can be used right now.
	// Most actions should override this, but utilize this base version as well.
	// Checks uses and valid targets. For now, modifiers are not considered.
    public virtual bool canUse(Entity? target) {
		if(properties.hasLimitedUses && uses == 0) {
			Console.WriteLine("No uses left.");
			return false;
		}
		if(owner == null) {
            Console.WriteLine("ERROR: no owner for action!");
            return false;
		}
		if(requiresTarget() && target == null) {
            Console.WriteLine("ERROR: no target for action '"+name+"'!");
            return false;
        }
		// If stunned, cannot use non-rest actions
		if(owner.HasStatusEffect("Stun") && actionType != ActionType.REST) {
			Console.WriteLine("Cannot use non-rest actions while stunned!");
			return false;
		}
		if(target != null) {
			// Check if target is valid:
			if(CanTarget(target!)) {
				return true;
			}
			else {
				Console.WriteLine(owner!.name+" cannot target "+target!.name+" with "+name+"!");
				return false;
			}
		}
		// Passed all checks, action can be used
		return true;
	}
    

	// Uses an action directly, for when an action is used without the player playing a card.
	// Asks the player to select a target if necessary. Returns false if the action was not used.
	// Also returns false if the action was untargetted, and failed.
	public virtual bool promptUse(bool free = false) {
		if(targetting == TargetCategory.SINGLE_ANY || targetting == TargetCategory.SINGLE_ALLY || targetting == TargetCategory.SINGLE_ENEMY) {
			// Prompt the player for a target.
			while(true) {
				Console.WriteLine("Enter a target for "+name+" (or 'skip' to not use the action):");
				Console.Write("\n> ");
                string? feedback = Console.ReadLine();
				Entity? target = null;
				string? actionTarget;
				if (feedback == null) feedback = "";
				actionTarget = feedback.ToLower().Trim().Replace('_',' ');
				if(actionTarget == "skip") {
					Console.WriteLine("Skipping action.");
					return false;
				}
				foreach(Entity hero in Battlefield.PlayerSide){
					// Check if target is here, if we were given one.:
					if(hero.name.ToLower().Trim() == actionTarget) {
						target = hero;
						break;
					}
				}
				foreach(Entity enemy in Battlefield.EnemySide){
					// Check if target is here, if we were given one.:
					if(enemy.name.ToLower().Trim() == actionTarget) {
						target = enemy;
						break;
					}
				}
				// If the target is still null, error:
				if(target == null) {
					Console.WriteLine("No target with the name "+actionTarget+" exists in this battle.");
					continue;
				}
				// Check if the target is valid:
				if(this.CanTarget(target)) {
					if(use(target, null, free)) {
						return true;
					}
					else {
						Console.WriteLine("Failed to use "+name+" on "+target+"!");
						continue;
					}
				}
				else {
					Console.WriteLine("Invalid target!");
					continue;
				}
			}
		}
		else {
			// Action does not require the player to choose a target; automatically select targets and attempt the action.
			return use(null, null, free);
		}
	}

	// Manages automatic targeting, action uses, events, etc
	// Relies on useOnTarget to be implemented by the child class, otherwise the action will fail
    public bool use(Entity? mainTarget, List<Modifier>? modifiers, bool free = false) {
		bool anySuccess = false; // Used to track whether the action was ever used successfully
		targetList = new List<Entity>(); // Reset the target list
		appliedModifiers = new List<Modifier>(); // Reset the modifier list
		properties = new ActionProperties(this); // Reset action properties. Generate new copy using this action
		properties.freeAction = free || properties.freeAction;
		// If this action currently has modifiers, apply them to change the properites of the action:
		if (modifiers != null && modifiers.Count > 0) {
			ApplyModifiersToProperties(modifiers);
		}
		if(mainTarget == null) {
			setTargets(); // Set starting target list if the target passed in was null
		}
		else {
			targetList.Add(mainTarget); // If we were passed a target, just use it
		}
		owner!.onUseAction(this); // Trigger event; this can modify the target and modifier lists
		if (targetList != null)
		{
			foreach (Entity target in targetList)
			{
				if (this.canUse(target))
				{
					if (target == null)
					{
						Console.WriteLine(owner!.name + " attempting to use " + name + "!");
					}
					else
					{
						Console.WriteLine(owner!.name + " attempting to use " + name + " on " + target!.name + "!");
					}
					bool successOnThisTarget = this.useOnTarget(target);
					// Run targeted modifier code:
					if (successOnThisTarget && modifiers != null && modifiers.Count > 0)
					{
						foreach(Modifier modifier in modifiers)
						{
							modifier.useOnTarget(this, target);
						}						
					} 
					anySuccess = successOnThisTarget || anySuccess;
				}
				else
				{
					// Invalid target
				}
			}
		}
		// Run the action code that does not target anyone
		if (anySuccess || (!requiresTarget() && canUse(null)))
		{
			anySuccess = useOnce() || anySuccess;
		}
		if (anySuccess)
		{ // Action succeeded (at least in some capacity)
			owner!.previousAction = this; // Update previous action.
			if (hasLimitedUses)
			{ // Decrement uses if the action has limited uses
				uses--;
				Console.WriteLine(uses + " use(s) remaining.");
			}
			if (!properties.freeAction)
			{
				owner!.exhausted = true; // Exhaust owner
			}
			// Run modifier code:
			if (modifiers != null && modifiers.Count > 0)
			{
				foreach(Modifier modifier in modifiers)
				{
					modifier.useOnce(this);
				}	
			}
			// Run the following event code only after action succeeds
			if(owner!.hostile)
			{
				//Console.WriteLine("DEBUG: Triggering onEnemyUsedAction for every hero");
				// Trigger onEnemyUsedAction for all heroes
				foreach(Entity hero in Battlefield.PlayerSide)
				{
					hero.onEnemyUsedAction(this);
				}
			}
			else
			{
				//Console.WriteLine("DEBUG: Triggering onEnemyUsedAction for every enemy");
				// Trigger onEnemyUsedAction for all enemies
				foreach(Entity enemy in Battlefield.EnemySide)
				{
					enemy.onEnemyUsedAction(this);
				}
			}
		}
		else
		{ // Action never went through; don't exhaust, don't use up uses
			Console.WriteLine("Action could not be used!");
		}
		Battlefield.resolveDeath();
		return anySuccess;
	}

    // The meat and potatoes of the action.
    // Each action should override this, unless it is NONE type targetting.
	// Modifier often null.
	public virtual bool useOnTarget(Entity? target) {
		return false;
	}

    // Some parts of an action only trigger once.
    // Actions with NONE targetting should always override this with their main effect.
	public virtual bool useOnce() {
		return false;
	}


	// Gets a target list based on the current targeting type of the action
	// List will be empty for target categories that cannot get a list automatically.
	public List<Entity> getTargets(TargetCategory category) {
		if(owner == null) {
			Console.WriteLine("ERROR: Action has no owner.");
			return new List<Entity>();
		}
		if(!CurrentRun.InCombat || Battlefield.CurrentEncounter == null) {
			Console.WriteLine("ERROR: not in combat.");
			return new List<Entity>();
		}
		List<Entity> targets = new List<Entity>();
		switch(category) {
			case TargetCategory.NONE:
				Console.WriteLine("Action target category is NONE.");
				return targets;
			case TargetCategory.SELF:
				targets.Add(owner);
				return targets;
			case TargetCategory.ALL_ENEMIES:
				if(owner.hostile) {
					foreach(Entity ent in Battlefield.PlayerSide) {
						targets.Add(ent);
					}
				}
				else {
					foreach(Entity ent in Battlefield.EnemySide) {
						targets.Add(ent);
					}
				}				
				return targets;
			case TargetCategory.ALL_ALLIES:
				if(owner.hostile) {
					foreach(Entity ent in Battlefield.EnemySide) {
						targets.Add(ent);
					}
				}
				else {
					foreach(Entity ent in Battlefield.PlayerSide) {
						targets.Add(ent);
					}
				}				
				return targets;
			case TargetCategory.EVERYONE:
				foreach(Entity ent in Battlefield.EnemySide) {
					targets.Add(ent);
				}
				foreach(Entity ent in Battlefield.PlayerSide) {
					targets.Add(ent);
				}
				return targets;
			case TargetCategory.OPPOSING:
				Entity? entOpp = getOpposingTarget(owner);
				if(entOpp != null)
				{
					Console.WriteLine("DEBUG: adding "+entOpp.name+" as opposing target.");
					targets.Add(entOpp);	
				}
				return targets;
			case TargetCategory.SINGLE_ALLY:
			case TargetCategory.SINGLE_ENEMY:
			case TargetCategory.SINGLE_ANY:
			case TargetCategory.DEAD_ALLY:
			case TargetCategory.DEAD_ENEMY:
			case TargetCategory.DEAD_ANY:
				Console.WriteLine("Action target category requires player to choose target.");
				return new List<Entity>();
			default:
				Console.WriteLine("ERROR: unknown Action target category");
				return new List<Entity>();
		}
	}

	// Default overload uses the action's normal targetting
	public virtual List<Entity> getTargets() {
		return getTargets(targetting);
	}


	// Uses math to approximate which enemy is closest to "across" from the action user.
	public Entity? getOpposingTarget(Entity owner)
	{
		Console.WriteLine("DEBUG: selecting opposing target automatically");
		int ownerIndex; // We will keep this 0-indexed.
		int ownerTeamCount;
		decimal ownerTeamCenterPosition; // We will keep this 0-indexed.
		decimal ownerDistanceFromCenter;
		int targetIndex; // We don't know this one, it is what we are trying to calculate.
		decimal targetTeamCenterPosition; // We will keep this 0-indexed.
		int targetTeamCount;
		if(owner.hostile)
		{ // Code for enemies
			ownerIndex = Battlefield.EnemySide.IndexOf(owner);
			ownerTeamCount = Battlefield.EnemySide.Count;
			ownerTeamCenterPosition = (ownerTeamCount * 0.5m)-0.5m; // Should end in .5 if the team count is even
			targetTeamCount = Battlefield.PlayerSide.Count;
			targetTeamCenterPosition = (targetTeamCount * 0.5m)-0.5m; // Should end in .5 if the team count is even
			ownerDistanceFromCenter = ownerIndex - ownerTeamCenterPosition; // This is allowed to be negative if the owner is above the center.
			//Console.WriteLine("DEBUG: Owner index: "+ownerIndex+"\n Owner team count: "+ownerTeamCount+"\n Owner team center: "+ownerTeamCenterPosition+"\n Owner distance from center: "+ownerDistanceFromCenter);
			// Now calculate the equivalent for the targets team:
			targetIndex = (int)(targetTeamCenterPosition+ownerDistanceFromCenter); // Let the int cast floor it.
			// Console.WriteLine("DEBUG: Target index: "+targetIndex+"\n Target team count: "+targetTeamCount+"\n Target team center: "+targetTeamCenterPosition);
			if(targetIndex < 0)
			{
				targetIndex = 0;
			}
			if(targetIndex >= Battlefield.EnemySide.Count)
			{
				targetIndex = Battlefield.EnemySide.Count-1;
			}
			return Battlefield.PlayerSide[targetIndex];
		}
		else
		{ // Code for heroes
			ownerIndex = Battlefield.PlayerSide.IndexOf(owner);
			ownerTeamCount = Battlefield.PlayerSide.Count;
			ownerTeamCenterPosition = (ownerTeamCount * 0.5m)-0.5m; // Should end in .5 if the team count is even
			targetTeamCount = Battlefield.EnemySide.Count;
			targetTeamCenterPosition = (targetTeamCount * 0.5m)-0.5m; // Should end in .5 if the team count is even
			ownerDistanceFromCenter = ownerIndex - ownerTeamCenterPosition; // This is allowed to be negative if the owner is above the center.
			// Console.WriteLine("DEBUG: Owner index: "+ownerIndex+"\n Owner team count: "+ownerTeamCount+"\n Owner team center: "+ownerTeamCenterPosition+"\n Owner distance from center: "+ownerDistanceFromCenter);
			// Now calculate the equivalent for the targets team:
			targetIndex = (int)(targetTeamCenterPosition+ownerDistanceFromCenter); // Let the int cast floor it.
			// Console.WriteLine("DEBUG: Target index: "+targetIndex+"\n Target team count: "+targetTeamCount+"\n Target team center: "+targetTeamCenterPosition);
			if(targetIndex < 0)
			{
				targetIndex = 0;
			}
			if(targetIndex >= Battlefield.EnemySide.Count)
			{
				targetIndex = Battlefield.EnemySide.Count-1;
			}
			return Battlefield.EnemySide[targetIndex];
		}
	}

	// Uses getTargets as a baseline, and then adds the extra targets if they do not exist in the list.
	public void setTargets(List<Entity> extraTargets) {
		this.targetList = getTargets();
		foreach(Entity extraTarget in extraTargets) {
			if(!this.targetList.Contains(extraTarget)) {
				// Add it
				this.targetList.Add(extraTarget);
			}
		}
	}

	// Uses getTargets as a baseline, substituting the given target category.
	public void setTargets(TargetCategory category) {
		this.targetList = getTargets(category);
	}

	// Uses getTargets as a baseline, with the default targetting for the action.
	public void setTargets() {
		this.targetList = getTargets();
	}

	// Useful for printing what would be shown to the player
	public override string ToString() {
		string actionString = "("+this.actionType+") "+this.name + ": " + this.description;
		if(this.hasLimitedUses) {
			actionString += " " + this.uses + " use";
			if(this.uses != 1) {
				actionString += "s."; 
			}
			else {
				actionString += ".";
			}
		}
		return actionString;
	}

	// Can this action target that entity?
	// Should be overridden, but the base version has useful basic targeting guidelines.
	// TODO: allow modifiers to change targetting here
	public virtual bool CanTarget(Entity target) {
		if(owner == null) {
            Console.WriteLine("ERROR: no owner for action!");
            return false;
        }
		// Use a copy of properties for determining targeting
		ActionProperties hypotheticalProperties = new ActionProperties(this);
		// Trigger event to allow modifying targeting:
		foreach (StatusEffect effect in owner.EffectList.ToList())
        {
            hypotheticalProperties = effect.ModifyCanTarget(hypotheticalProperties);
        }
		foreach(Action act in owner.ActionList)
		{
			hypotheticalProperties = act.ModifyCanTarget(hypotheticalProperties);
			if (act.equippedItem != null)
            {
                hypotheticalProperties = act.equippedItem.ModifyCanTarget(hypotheticalProperties);
            }
		}
		// Anyone who wanted to modify targeting at this point should have done so.

		// If the target is an Inanimate, return false:
        if (target.isInanimate)
        {
            Console.WriteLine(target + " cannot be targeted, it is part of the Inanimate.");
            return false;
        }
		switch (targetting)
		{
			case TargetCategory.NONE:
				return false;
			case TargetCategory.SELF:
				return target == owner;
			case TargetCategory.OPPOSING:
				// Must be the proper target.
				if(getOpposingTarget(owner) != target)
				{
					Console.WriteLine(target.name + " cannot be targeted, because they are not the opposing enemy!");
					return false;
				}
				goto case TargetCategory.SINGLE_ENEMY;
			case TargetCategory.ALL_ENEMIES:
			case TargetCategory.SINGLE_ENEMY:
				// Must be alive
				if (Battlefield.DeadHeroes.Contains(target) || Battlefield.DeadEnemies.Contains(target))
				{
					Console.WriteLine(target.name + " cannot be targeted, because they are dead!");
					return false;
				}
				// If they are on different teams, they can target with this action.
				bool opposingTeams = owner.hostile != target.hostile;
				// Check for Taunt as well:
				if (!hypotheticalProperties.ignoresTaunt && !Battlefield.Taunters.Contains(target))
				{
					// If the target does not have taunt, need to check if their allies do:
					if (Battlefield.Taunters.Count > 0)
					{
						foreach (Entity taunter in Battlefield.Taunters)
						{
							if (target.hostile == taunter.hostile)
							{
								// Taunter is on the same team as the target, and will protect them.
								Console.WriteLine(target.name + " could not be targeted, because they were protected by " + taunter.name);
								return false;
							}
						}
					}
				}
				// Check for invisibility: (currently considered a form of dodging)
				if (!hypotheticalProperties.ignoresDodge && target.HasStatusEffect("Invisibility"))
				{
					// If they are not last on their team, they cannot be targeted
					if (!target.hostile && Battlefield.PlayerSide.Count > 1)
					{
						Console.WriteLine(target.name + " could not be targeted, because they were invisible.");
						return false;
					}
					else if (target.hostile && Battlefield.EnemySide.Count > 1)
					{
						Console.WriteLine(target.name + " could not be targeted, because they were invisible.");
						return false;
					}
				}
				// Check for Locked:
				if (owner.HasStatusEffect("Locked"))
				{
					StatusEffect? eff = owner.GetStatusEffect("Locked");
					if(eff is Locked lockStatus) {
						if (target != lockStatus.lockedTarget)
						{
							Console.WriteLine(target.name + " could not be targeted, because they were not "+owner.name+"'s locked target.");
							return false;
						}
					}
				}
				// Check for Skill Immune:
				if(actionType == ActionType.SKILL && target.HasPassive("Skill-immune"))
				{
					Console.WriteLine(target.name + " could not be targeted, because they are immune to Skills.");
				}
				return opposingTeams;
			case TargetCategory.ALL_ALLIES:
			case TargetCategory.SINGLE_ALLY:
				// Must be alive
				if (Battlefield.DeadHeroes.Contains(target) || Battlefield.DeadEnemies.Contains(target))
				{
					Console.WriteLine(target.name + " cannot be targeted, because they are dead!");
					return false;
				}
				// If they are on the same team, they can target with this action
				return owner.hostile == target.hostile;
			case TargetCategory.DEAD_ALLY:
				// If they are on the same team, but the target is dead, they can target with this action
				return (Battlefield.DeadHeroes.Contains(target) && !owner.hostile)
				|| (Battlefield.DeadEnemies.Contains(target) && owner.hostile);
			case TargetCategory.DEAD_ENEMY:
				// If they are on opposite teams, but the target is dead, they can target with this action
				return (Battlefield.DeadHeroes.Contains(target) && owner.hostile)
				|| (Battlefield.DeadEnemies.Contains(target) && !owner.hostile);
			case TargetCategory.DEAD_ANY:
				// If the target is dead, they can target with this action
				return Battlefield.DeadHeroes.Contains(target) || Battlefield.DeadEnemies.Contains(target);
			case TargetCategory.EVERYONE:
			case TargetCategory.SINGLE_ANY:
				// Must be alive
				if (Battlefield.DeadHeroes.Contains(target) || Battlefield.DeadEnemies.Contains(target))
				{
					Console.WriteLine(target.name + " cannot be targeted, because they are dead!");
					return false;
				}
				return true;
			default:
				Console.WriteLine("Action had no targetting set. This should never happen.");
				return false;

		}
	}

	// Some target categories require a target to operate on.
    public bool requiresTarget() {
        return targetting == TargetCategory.SINGLE_ALLY || targetting == TargetCategory.SINGLE_ENEMY || targetting == TargetCategory.SINGLE_ANY || targetting == TargetCategory.DEAD_ALLY || targetting == TargetCategory.DEAD_ENEMY || targetting == TargetCategory.DEAD_ANY;
    }

	// This should be called just before an action triggers.
	// This modifies the ongoing properties. It does not allow any given modifier
	// to modify the list more than once.
	public void ApplyModifiersToProperties(List<Modifier> modifiers)
	{
		foreach(Modifier mod in modifiers)
		{
			if(!appliedModifiers.Contains(mod))
			{
				mod.modifyProperties(properties, this.owner);
				appliedModifiers.Add(mod);
			}
			else
			{
				Console.WriteLine("Modifier "+mod.name+" has already been applied.");
			}
			
		}
	}
	
	// Same thing but only 1 modifier
	public void ApplyModifierToProperties(Modifier modifier)
	{
		if(!appliedModifiers.Contains(modifier))
		{
			modifier.modifyProperties(properties, this.owner);
			appliedModifiers.Add(modifier);
		}
		else
		{
			Console.WriteLine("Modifier "+modifier.name+" has already been applied.");
		}
	}


	//==========================ITEM OPERATIONS=========================
	

    public void Equip(EquipmentItem item) {
		// Let the item itself do the heavy liftiing.
		if(item.Equip(this)) {
			// If it returns true, then the equipping was successful.
			this.equippedItem = item; 
		}
		else {
			// Otherwise, it failed, so do nothing.
			Console.WriteLine("ERROR: Could not equip item "+item.name+" to action "+this.name);
		}
    }

    public void Unequip() {
		if(this.equippedItem == null) return;
        if(this.equippedItem.Unequip(this)) {
			// If it returns true, then the unequipping was successful.
			this.equippedItem = null; 
		}
		else {
			// Otherwise, it failed, so do nothing.
			Console.WriteLine("ERROR: Could not unequip item "+this.equippedItem.name+" from action "+this.name);
		}
    }

	//==========================END ITEM OPERATIONS=========================

	//=========================ACTION LISTS==================================
	// These just help organize actions into categories, which are referenced by status effects/modifiers.
	// For example, Charged and Surging both affect the same list of spell actions.
	// These will need to be updated as new actions are added.
	public static List<string> SpellsWithDamageBlockOrHealing = 
	[
		"Forcefield",
		"Frost Breath",
		"Ice Spike",
		"Ice Wall",
		"Lightning",
		"Living Flame",
		"Mirror Force",
		"Purify",
		"Repel",
		"Restore",
		"Zap"
	];

	public static List<string> AttacksWithDamage = 
	[
		"Bash",
		"Bite",
		"Cleave",
		"Cut",
		"Decimate",
		"Dive",
		"Gnaw",
		"Infect",
		"Jaws",
		"Loose",
		"Ravage",
		"Shadow Slash",
		"Shadow Strike",
		"Shoot",
		"Snipe",
		"Stab",
		"Strike",
		"Swipe",
		"Twin Slash",
		"Whack"
	];

	public static List<string> DefendsWithBlock = 
	[
		"Block",
		"Cower",
		"Obstruct",
		"Parry"
	];

	public static List<string> AttacksOrDefendsWithDamageOrBlock = AttacksWithDamage.Union(DefendsWithBlock).ToList();
}