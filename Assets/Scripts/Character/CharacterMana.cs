using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMana : MonoBehaviour
{

    [SerializeField] private float manaInit;
    [SerializeField] private float manaMax;
    [SerializeField] private float regenerationForSecond;

    public float ManaActual { get; private set; }

    public bool CanBeRestart => ManaActual < manaMax;

    private CharacterLife _characterLife;


    private void Awake()
    {
        _characterLife = GetComponent<CharacterLife>();
    }


    // Start is called before the first frame update
    void Start()
    {

        ManaActual = manaInit;
        UpdateBarMana();
        InvokeRepeating(nameof(RegenerateMana), 1, 1);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.G)) {

            UseMana(10);
        }
    }


    public void UseMana(float amount) 
    {

        if (ManaActual >= amount)
        {
            ManaActual -= amount;
            UpdateBarMana();
        }
    
    }

    public void RestartManaAmout(float amount)
    {
        if (ManaActual >= manaMax)
        {
            return;
        }
        ManaActual += amount;
        if (ManaActual > manaMax)
        {
            ManaActual= manaMax;
        }
        UpdateBarMana();
    }

    private void RegenerateMana()
    {
        if(_characterLife.Health>0f && ManaActual < manaMax)
        {
            ManaActual += regenerationForSecond;
            UpdateBarMana();
        }
    }

    public void RestartMana()
    {
        ManaActual = manaInit;
        UpdateBarMana();
    }

    private void UpdateBarMana()
    {
        UIManager.Instance.UpdateManaCharacter(ManaActual, manaMax);
    }

}
