using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StoreManagerNewUI : MonoBehaviour 
{
    public Text titleText;
    public Text costText;
    public RawImage level;
    public Texture[] levelImages;
    
    public Text statsBarTitleTop;
    public Slider topStatBarCurrent;
    public Slider topStatBarNext;

    public Text statsBarTitleMiddle;
    public Slider middleStatBarCurrent;
    public Slider middleStatBarNext;

    public Text statsBarTitleBottom;
    public Slider bottomStatBarCurrent;
    public Slider bottomStatBarNext;

    public Text description;

    public MainMenuManager mainMenuManager;

    void Start()
    {
        Variables.instance.ReturnCrateDescription();

    }
    void Update()
    {
        titleText.text = Variables.instance.storeNames[Variables.instance.upgradeCurrentCrate];
        costText.text = "Cost: " + Variables.instance.upgradeCost[Variables.instance.upgradeCurrentCrate];
        level.texture = levelImages[Variables.instance.upgradeLevel[Variables.instance.upgradeCurrentCrate]];

        description.text = Variables.instance.upgradeCrateDescription2[Variables.instance.upgradeCurrentCrate];

        //SetStats(0, 10, 50, 20, "My Custom Weapon");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index">0-2 for the bar level</param>
    /// <param name="currentValue"></param>
    /// <param name="maxValue"></param>
    /// <param name="nextValue"></param>
    public void SetStats(int index, float currentValue, float maxValue, float nextValue, string title )
    {
        float current = currentValue / maxValue;
        float next = nextValue / maxValue;

        switch (index)
        {
            case 0:
                statsBarTitleTop.text = title;
                topStatBarCurrent.value = current;
                topStatBarNext.value = next;
                break;
            case 1:
                statsBarTitleMiddle.text = title;
                middleStatBarCurrent.value = current;
                middleStatBarNext.value = next;
                break;
            case 2:
                statsBarTitleBottom.text = title;
                bottomStatBarCurrent.value = current;
                bottomStatBarNext.value = next;
                break;
            default:
                break;
        }

    }

    public void SetStatsOff( int index )
    {
        switch (index)
        {
            case 0:
                statsBarTitleTop.text = "";
                topStatBarCurrent.value = 0;
                topStatBarNext.value = 0;
                break;
            case 1:
                statsBarTitleMiddle.text = "";
                middleStatBarCurrent.value = 0;
                middleStatBarNext.value = 0;
                break;
            case 2:
                statsBarTitleBottom.text = "";
                bottomStatBarCurrent.value = 0;
                bottomStatBarNext.value = 0;
                break;
            default:
                break;
        }
    }

    public void Buy()
    {
        Variables.instance.BuyUpgrade(Variables.instance.upgradeCurrentCrate);
    }

    public void Back()
    {
        mainMenuManager.BackFromStore();
    }
}
