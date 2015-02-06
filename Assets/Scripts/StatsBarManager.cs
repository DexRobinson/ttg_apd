using UnityEngine;
using System.Collections;

public class StatsBarManager : MonoBehaviour {

	public StatsInfo[] topBarStats;
	public StatsInfo[] middleBarStats;
	public StatsInfo[] bottomBarStats;

    
	void Update()
	{
		MainMenuManager.instance.statsBars[0].SetStatsBar(topBarStats[Variables.instance.upgradeCurrentCrate].frontMax, topBarStats[Variables.instance.upgradeCurrentCrate].middleMax,
		                                                  topBarStats[Variables.instance.upgradeCurrentCrate].frontCurrent, topBarStats[Variables.instance.upgradeCurrentCrate].middleCurrent);

		MainMenuManager.instance.statsBars[1].SetStatsBar(middleBarStats[Variables.instance.upgradeCurrentCrate].frontMax, middleBarStats[Variables.instance.upgradeCurrentCrate].middleMax,
		                                                  middleBarStats[Variables.instance.upgradeCurrentCrate].frontCurrent, middleBarStats[Variables.instance.upgradeCurrentCrate].middleCurrent);

		MainMenuManager.instance.statsBars[2].SetStatsBar(bottomBarStats[Variables.instance.upgradeCurrentCrate].frontMax, bottomBarStats[Variables.instance.upgradeCurrentCrate].middleMax,
		                                                  bottomBarStats[Variables.instance.upgradeCurrentCrate].frontCurrent, bottomBarStats[Variables.instance.upgradeCurrentCrate].middleCurrent);
	}
}

[System.Serializable]
public class StatsInfo
{
	public float frontMax;
	public float frontCurrent;

	public float middleMax;
	public float middleCurrent;

	public StatsInfo(float fMax, float fCurrent, float mMax, float mCurrent)
	{
		frontMax = fMax;
		frontCurrent = fCurrent;

		middleMax = mMax;
		middleCurrent = mCurrent;
	}
}