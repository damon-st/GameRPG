using System;
using UnityEngine;

public class CharacterLife : LifeBase
{
    public static Action EventCharacterDead;
    /// <summary>
    /// Return is character is dead
    /// </summary>
    public bool Defeated { get; private set; }
    public bool CanBeCured => Health < healthMax;

    private BoxCollider2D _boxColider2D;

    private void Awake()
    {
        _boxColider2D = GetComponent<BoxCollider2D>();
    }

    protected override void Start()
    {
        base.Start();
        UpdateBarLife(Health, healthMax);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) {

            // ReceiverDamage(10);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {

          //  ResetHealth(10);
        }
    }

    public void ResetHealth(float amount)
    {
        if (Defeated) return;
        if (CanBeCured)
        {
            Health += amount;
            if(Health> healthMax)
            {
                Health = healthMax;
            }
            UpdateBarLife(Health, healthMax);
        }
    }

    protected override void CharacterDefeated()
    {
        _boxColider2D.enabled = false;
        Defeated = true;
        EventCharacterDead?.Invoke();
        AudioManager.Instance.PlayAudioCharacterDead();
        LevelManager.Instance.PauseGame();
    }

    public void RestartCharacter()
    {
        _boxColider2D.enabled = true;
        Defeated = false;
        Health = healthInit;
        UpdateBarLife(Health, healthInit);
    }

    protected override void UpdateBarLife(float lifeActual, float lifeMax)
    {
        UIManager.Instance.UpdateLifeCharacter(lifeActual, lifeMax);
    }
}
