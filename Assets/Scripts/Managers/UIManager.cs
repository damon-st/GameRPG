using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{

    [Header("Stats")]
    [SerializeField] private CharacterStats stats;

    [Header("Paneles")]
    [SerializeField] private GameObject panelStats;
    [SerializeField] private GameObject panelMarketPlace;
    [SerializeField] private GameObject panelCrafting;
    [SerializeField] private GameObject panelCraftingInfo;
    [SerializeField] private GameObject panelInventario;
    [SerializeField] private GameObject panelInspectorQuests;
    [SerializeField] private GameObject panelCharacterQuests;

    [Header("Barra")]
    [SerializeField] private Image lifePlayer;
    [SerializeField] private Image manaPlayer;
    [SerializeField] private Image expPlayer;


    [Header("Text")]
    [SerializeField] private TextMeshProUGUI lifeTMP;
    [SerializeField] private TextMeshProUGUI manaTMP;
    [SerializeField] private TextMeshProUGUI expTMP;
    [SerializeField] private TextMeshProUGUI levelTMP;
    [SerializeField] private TextMeshProUGUI monedasTMP;

    [Header("Stats")]
    [SerializeField] private TextMeshProUGUI statDamangeTMP;
    [SerializeField] private TextMeshProUGUI statDefendingTMP;
    [SerializeField] private TextMeshProUGUI statCriticalTMP;
    [SerializeField] private TextMeshProUGUI statBlockingTMP;
    [SerializeField] private TextMeshProUGUI statVelocityTMP;
    [SerializeField] private TextMeshProUGUI statLevelTMP;
    [SerializeField] private TextMeshProUGUI statExpTMP;
    [SerializeField] private TextMeshProUGUI statExpReqTMP;
    [SerializeField] private TextMeshProUGUI statExpTotalTMP;
    [SerializeField] private TextMeshProUGUI atributteForceTMP;
    [SerializeField] private TextMeshProUGUI atributteIntelligenceTMP;
    [SerializeField] private TextMeshProUGUI atributteSkillTMP;
    [SerializeField] private TextMeshProUGUI atributteAvalibledTMP;




    private float lifeActual;
    private float lifeMax;

    private float manaActual;
    private float manaMax;

    private float expActual;
    private float expRequiredNewLevel;

    // Update is called once per frame
    void Update()
    {
        UpdateUICharacter();
        UpdatePanelStats();
    }

    private void UpdateUICharacter()
    {
        lifePlayer.fillAmount = Mathf.Lerp(lifePlayer.fillAmount, lifeActual / lifeMax, 10f * Time.deltaTime);
        manaPlayer.fillAmount = Mathf.Lerp(manaPlayer.fillAmount, manaActual / manaMax, 10f * Time.deltaTime);
        expPlayer.fillAmount = Mathf.Lerp(expPlayer.fillAmount, expActual / expRequiredNewLevel, 10f * Time.deltaTime);

        lifeTMP.text = $"{lifeActual}/{lifeMax}";
        manaTMP.text = $"{manaActual}/{manaMax}";
        expTMP.text = $"{((expActual / expRequiredNewLevel) * 100):F2}%";
        levelTMP.text = $"Nivel {stats.Level}";
        monedasTMP.text = MonedasManager.Instance.CointsTotals.ToString();

    }

    private void UpdatePanelStats()
    {
        if (panelStats.activeSelf == false)
        {
            return;
        }

        statDamangeTMP.text = stats.Damage.ToString();
        statDefendingTMP.text = stats.Defending.ToString();
        statCriticalTMP.text = $"{stats.PorcentageCritic}%";
        statBlockingTMP.text = $"{stats.PorcentageBlocking}%";
        statVelocityTMP.text = stats.Velocity.ToString();
        statLevelTMP.text = stats.Level.ToString();
        statExpTMP.text = stats.ExpActual.ToString();
        statExpReqTMP.text = stats.ExpRequiredNextLevel.ToString();
        statExpTotalTMP.text = stats.ExpTotal.ToString();

        atributteForceTMP.text = stats.Force.ToString();
        atributteIntelligenceTMP.text = stats.Intelligence.ToString();
        atributteSkillTMP.text = stats.Skill.ToString();
        atributteAvalibledTMP.text = $"Puntos: {stats.PointsAvalibles}";
    }


    public void UpdateLifeCharacter(float pLifeActual, float pLifeMax)
    {
        lifeActual = pLifeActual;
        lifeMax = pLifeMax;
    }

    public void UpdateManaCharacter(float pManaActual, float pManaMax)
    {
        manaActual = pManaActual;
        manaMax = pManaMax;
    }

    public void UpdateExpCharacter(float pExpActual, float pExpMax)
    {
        expActual = pExpActual;
        expRequiredNewLevel = pExpMax;
    }

    #region Paneles


    public void OpenClosePanelStats()
    {
        panelStats.SetActive(!panelStats.activeSelf);
    }

    public void OpenClosePanelMarketPlace()
    {
        panelMarketPlace.SetActive(!panelMarketPlace.activeSelf);
    }

    public void OpenPanelCrafting()
    {
        panelCrafting.SetActive(true);
    }

    public void ClosePanelCrafting()
    {
        panelCrafting.SetActive(false);
        OpenClosePanelCraftingInfo(false);
    }

    public void OpenClosePanelCraftingInfo(bool state)
    {
        panelCraftingInfo.SetActive(state);

    }

    public void OpenClosePanelInventario()
    {
        panelInventario.SetActive(!panelInventario.activeSelf);
    }

    public void OpenClosePanelInspectorQuests()
    {
        panelInspectorQuests.SetActive(!panelInspectorQuests.activeSelf);
    }

    public void OpenClosePanelCharacterQuests()
    {
        panelCharacterQuests.SetActive(!panelCharacterQuests.activeSelf);
    }

    public void OpenPanelInteraction(InterationExtraNPC typeInteraction)
    {
        switch (typeInteraction)
        {
            case InterationExtraNPC.Quets:
                OpenClosePanelInspectorQuests();
                break;

            case InterationExtraNPC.Marketplace:
                OpenClosePanelMarketPlace();
                break;

            case InterationExtraNPC.Crafting:
                OpenPanelCrafting();
                break;
        }
    }


    #endregion

}
