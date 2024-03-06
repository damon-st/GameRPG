using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterExperience : MonoBehaviour
{

    [Header("Stas")]
    [SerializeField] private CharacterStats stats;


    [Header("Config")]
    [SerializeField] private int levelMax;
    [SerializeField] private int expBase;
    [SerializeField] private int valueIncrement;


    private float expActual;
    private float expRequiredNextLevel;

    // Start is called before the first frame update
    void Start()
    {
        stats.Level = 1;
        expRequiredNextLevel = expBase;
        stats.ExpRequiredNextLevel = expRequiredNextLevel;
        UpdateBarExp();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)) {
            AddExperience(2f);
        }
    }

    public void AddExperience(float expObtenid)
    {
        if (expObtenid <= 0) return;
        expActual += expObtenid;
        stats.ExpActual= expActual;
        if(expActual== expRequiredNextLevel)
        {
            UpdateLevel();
        }else if (expActual > expRequiredNextLevel)
        {
            float difference = expActual - expRequiredNextLevel;
            UpdateLevel();
            AddExperience(difference);
        }

        stats.ExpTotal += expObtenid;
        UpdateBarExp();
    }

    private void UpdateLevel()
    {
        if (stats.Level < levelMax)
        {
            stats.Level++;
            stats.ExpActual = 0;
            expActual = 0;
            expRequiredNextLevel *= valueIncrement;
            stats.ExpRequiredNextLevel = expRequiredNextLevel;
            stats.PointsAvalibles += 3;
        }
    }

    private void UpdateBarExp()
    {
        UIManager.Instance.UpdateExpCharacter(expActual, expRequiredNextLevel);
    }

    private void ResponseEnemyDestroyed(float expWin)
    {
        AddExperience(expWin);
    }

    private void OnEnable()
    {
        EnemigoVida.EventEnemyDestroyed += ResponseEnemyDestroyed;
    }

    private void OnDisable()
    {
        EnemigoVida.EventEnemyDestroyed -= ResponseEnemyDestroyed;
    }
}
