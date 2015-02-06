using UnityEngine;
using System.Collections;

public class WeaponSlot : MonoBehaviour {
	public GameObject slotTab;
	public static WeaponSlot instance;
	public GameObject[] slots;
	public Variables.CrateType[] crateType;
	public bool slot1Full;
	public bool slot2Full;
	
	void Awake()
	{
		instance = this;
	}
	
	public void ChangeSlot()
	{
		if(!slotTab.activeSelf)
			slotTab.SetActive(true);
		else
			slotTab.SetActive(false);
	}
	
	public void ActivateSlot1()
	{
		if(!Variables.instance.isPowerMode)
		{
			ActivateCrate(crateType[0]);
			slot1Full = false;
			slots[0].renderer.material.mainTexture = Resources.Load("Crate_Bomb") as Texture2D;
		}
	}
	
	public void ActivateSlot2()
	{
		if(!Variables.instance.isPowerMode)
		{
			ActivateCrate(crateType[1]);
			slot2Full = false;
			slots[1].renderer.material.mainTexture = Resources.Load("Crate_Bomb") as Texture2D;
		}
	}
	
	public void FillInSlots(Variables.CrateType ct)
	{
		if(!slot1Full)
		{
			slots[0].renderer.material.mainTexture = Resources.Load("Crate_" + ct.ToString()) as Texture2D;
			crateType[0] = ct;
			slot1Full = true;
		}
		else if(!slot2Full)
		{
			slots[1].renderer.material.mainTexture = Resources.Load("Crate_" + ct.ToString()) as Texture2D;
			crateType[1] = ct;
			slot2Full = true;
		}
	}
		
	void ActivateCrate(Variables.CrateType crateType)
	{
		switch (crateType) 
        {
		    case Variables.CrateType.Bomb:
			    Variables.instance.playerBombAmount += 3;
			    break;
		    case Variables.CrateType.Energy:
				float maxEnergy = Variables.instance.playerMaxEnergy;
				maxEnergy *= (Variables.instance.upgradeLevel[8] * 0.2f);
			    Variables.instance.ChangePlayerEnergyAmount (maxEnergy);
			    break;
		    case Variables.CrateType.Flak:
				if(!Variables.instance.isPowerMode)
			    	Variables.instance.ChangeWeapon (Variables.Weapon.Flak);
			    break;
		    case Variables.CrateType.Health:
				float maxHealth = Variables.instance.playerMaxHealth;
				maxHealth *= (Variables.instance.upgradeLevel[7] * 0.1f);
			    Player.instance.AdjustPlanetHealth (maxHealth);
			    break;
		    case Variables.CrateType.Laser:
				if(!Variables.instance.isPowerMode)
			    	Variables.instance.ChangeWeapon (Variables.Weapon.Laser);
			    break;
		    case Variables.CrateType.Mines:
			    Variables.instance.ItemDropMines();
			    break;
		    case Variables.CrateType.Money:
				//QualitySettings.SetQualityLevel(2);
			    Variables.instance.playerMoney += Variables.instance.itemMoneyPickupAmount + (Variables.instance.upgradeLevel[11] * 5);
                Variables.instance.playerMoneyRound += Variables.instance.itemMoneyPickupAmount + (Variables.instance.upgradeLevel[11] * 5);
                Variables.instance.playerMoneyTotal += Variables.instance.itemMoneyPickupAmount + (Variables.instance.upgradeLevel[11] * 5);
			    break;
		    case Variables.CrateType.Multi:
				if(!Variables.instance.isPowerMode)
			    	Variables.instance.ChangeWeapon (Variables.Weapon.Multi);
			    break;
		    case Variables.CrateType.Rapid:
				if(!Variables.instance.isPowerMode)
			    	Variables.instance.ChangeWeapon (Variables.Weapon.Rapid);
			    break;
		    case Variables.CrateType.Satalite:
                Variables.instance.ItemDropSatalite();
                break;
            case Variables.CrateType.Seeker:
				if(!Variables.instance.isPowerMode)
                	Variables.instance.ChangeWeapon(Variables.Weapon.Seeker);
                break;
            case Variables.CrateType.Shield:
                Variables.instance.ChangePlayerShields(Variables.instance.itemShieldMaxAmount);
                break;
            case Variables.CrateType.Slow:
                Variables.instance.ItemSlowOn();
                break;
        }
		
		crateType = Variables.CrateType.None;
	}
}
