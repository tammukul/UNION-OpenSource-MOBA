using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using PlayFab.ClientModels;

public class GameplayUI : MonoBehaviour
{
    public static GameplayUI instance;


    /// <summary>
    /// Contains the prefab of the store item
    /// </summary>
    [SerializeField]
    GameObject storeItemPrefab;

    /// <summary>
    /// Animator of panel
    /// </summary>
    [SerializeField]
    Animator panelAnimator;


    /// <summary>
    /// Team tower lifebar
    /// </summary>
    [SerializeField]
    Image myTeamTowerLifebar;

    /// <summary>
    /// Enemy Team tower lifebar
    /// </summary>
    [SerializeField]
    Image enemyTeamTowerLifebar;

    /// <summary>
    /// Team kill count text
    /// </summary>
    [SerializeField]
    Text myTeamKillcount;

    /// <summary>
    /// Enemy Team kill count text
    /// </summary>
    [SerializeField]
    Text enemyTeamKillcount;

    /// <summary>
    /// Player level text
    /// </summary>
    [SerializeField]
    Text[] myPlayerLevel;

    /// <summary>
    /// Player expericence slider
    /// </summary>
    [SerializeField]
    Slider[] myExpBarFill;

    /// <summary>
    /// Lifebar fill image
    /// </summary>
    [SerializeField]
    Image myLifebarFill;

    /// <summary>
    /// Lifebar background
    /// </summary>
    [SerializeField]
    Image myLifebarBackground;

    /// <summary>
    /// Game time text
    /// </summary>
    [SerializeField]
    Text gameTime;

    /// <summary>
    /// Currency text
    /// </summary>
    [SerializeField]
    Text currencyText;


    /// <summary>
    /// Parent gameobject to the store scroll
    /// </summary>
    [SerializeField]
    GameObject storeScrollParent;


    /// <summary>
    /// Animator to the level up popup
    /// </summary>
    [SerializeField]
    Animator levelUpPopupAnimator;


    /// <summary>
    /// Array with the level up buttons gameObjects
    /// </summary>
    [SerializeField]
    GameObject[] levelUpButtons;
    
    /// <summary>
    /// text of lifebar slider
    /// </summary>
    [SerializeField]
    Text sidebarLifeText;

    /// <summary>
    /// text of lifebar slider
    /// </summary>
    [SerializeField]
    Text sidebarDefenseText;
    /// <summary>
    /// text of the defense sidebar
    /// </summary>
    [SerializeField]
    Text sidebarDamageText;
    /// <summary>
    /// text of the damage sidebar
    /// </summary>
    [SerializeField]
    Text sidebarMovSpeedText;
    /// <summary>
    /// text of movement speed sidebar
    /// </summary>
    [SerializeField]
    Text sidebarClipSizeText;

    TeamTypes playerTeam;

    PlayerManager playerToUpdate;

    void Awake()
    {
        instance = this;
    }

    public void Initialize(TeamTypes myTeam, PlayerManager playerToUpdate)
    {
        this.playerToUpdate = playerToUpdate;

        this.playerTeam = myTeam;
    }

    public void UpdateTowerLife(TeamTypes towerTeam, float actualHP, float totalHP)
    {
        if (towerTeam == playerTeam)
        {
            myTeamTowerLifebar.fillAmount = (float)actualHP / (float)totalHP;
        }
        else
        {
            enemyTeamTowerLifebar.fillAmount = (float)actualHP / (float)totalHP;
        }
    }

    public void UpdatePlayerLife(float actualHP, float totalHP)
    {
        myLifebarFill.fillAmount = (actualHP / totalHP) * 0.74f;
        myLifebarBackground.fillAmount = (actualHP / totalHP) * 0.74f;
    }

    public void UpdateExpBar(float percentage)
    {
        for (int i = 0; i < myExpBarFill.Length; i++)
        {
            myExpBarFill[i].value = percentage;
        }
    }

    public void UpdateKillcount(TeamTypes team, int amount)
    {
        if (team == playerTeam)
        {
            myTeamKillcount.text = amount.ToString();
        }
        else
        {
            enemyTeamKillcount.text = amount.ToString();
        }
    }

    public void UpdatePlayerLevel(int level)
    {
        for (int i = 0; i < myPlayerLevel.Length; i++)
        {
            myPlayerLevel[i].text = "Level " + level.ToString();
        }
    }

    public void UpdateGameTime(int seconds)
    {
        string newText = seconds / 60 + ":" + (seconds % 60).ToString("00");
        if (gameTime.text != newText)
        {
            gameTime.text = newText;
        }
    }

    public void UpdateCurrencyText(int currencyAmount)
    {
        currencyText.text = currencyAmount.ToString();
    }

    public void CreateStoreButtons(List<CatalogItem> catalogItems)
    {
        for (int i = 0; i < catalogItems.Count; i++)
        {
            if (catalogItems[i].ItemId == "buyshe") return;

            GameObject _obj = (GameObject)GameObject.Instantiate(storeItemPrefab);
            _obj.transform.SetParent(storeScrollParent.transform, false);

            float height = (_obj.transform as RectTransform).sizeDelta.y;

            (_obj.transform as RectTransform).anchoredPosition = new Vector2(0, -height * i);

            _obj.GetComponent<InGameStoreItem>().Initialize(catalogItems[i]);
        }
    }

    public void DismissPanel()
    {
        panelAnimator.Play("reverse");
    }

    public void OnLeaveGameClick()
    {
        PhotonNetwork.Disconnect();
        SceneLoader.sceneToLoad = Scenes.MainMenu;
        Application.LoadLevel(Scenes.Loading.ToString());
    }

    public void ShowLevelUpPopup(PlayerStats[] options)
    {
        levelUpPopupAnimator.Play("Foward");

        for (int i = 0; i < levelUpButtons.Length; i++)
        {
            levelUpButtons[i].SetActive(false);
        }

        for (int i = 0; i < options.Length; i++)
        {
            levelUpButtons[options[i].GetHashCode()].SetActive(true);
        }
    }

    public void OnLevelUpClick(int statIndex)
    {
        levelUpPopupAnimator.Play("Reverse");

        playerToUpdate.GiveStatusBonus((PlayerStats)statIndex);
    }

    public void UpdateSidebarStats(PlayerProperties properties)
    {
        sidebarLifeText.text = properties.baseHP + " + " + properties.hpModifier;
        sidebarDefenseText.text = properties.baseDef + " + " + properties.defModifier;
        sidebarDamageText.text = properties.baseAtk + " + " + properties.atkModifier;
        sidebarMovSpeedText.text = properties.baseMovSpd + " + " + properties.movSpdModifier;
        sidebarClipSizeText.text = properties.baseFireRate + " + " + properties.fireRateModifier;
    }
}