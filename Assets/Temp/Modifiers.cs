using UnityEngine;
using System.Collections;

public static class Modifiers {
	public static float creditsMod = 1.0f;
	public static float expMod = 1.0f;
	public static float supwerWeaponMod = 1.0f;
	public static float powerWeaponMod = 1.0f;
	
	// Use this for initialization
	public static void Init () {
		PlayerPrefs.GetFloat("creditsMod", 1f);
		PlayerPrefs.GetFloat("expMod", 1f);
		PlayerPrefs.GetFloat("supwerWeaponMod", 1f);
		PlayerPrefs.GetFloat("powerWeaponMod", 1f);
	}
}
