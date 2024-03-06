using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoVida : LifeBase
{
    public static Action<float> EventEnemyDestroyed;

    [Header("Vida")]
    [SerializeField] private EnemigoBarraVida barraLifePrefab;
    [SerializeField] private Transform barraLifePosition;

    [Header("Rastros")]
    [SerializeField] private GameObject rastros;

    private EnemigoBarraVida _enemyBarraLifeCreated;
    private EnemigoInteraction _enemyInteraction;
    private EnemigoMovimiento _enemyMove;
    private SpriteRenderer _spriteRender;
    private BoxCollider2D _boxCollider2D;
    private IAController _controller;
    private EnemigoLoot _enemyLoot;

    private void Awake()
    {
        _enemyInteraction= GetComponent<EnemigoInteraction>();
        _enemyMove = GetComponent<EnemigoMovimiento>();
        _spriteRender = GetComponent<SpriteRenderer>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _controller = GetComponent<IAController>();
        _enemyLoot = GetComponent<EnemigoLoot>();

    }

    protected override void Start()
    {
        base.Start();
        CreateBarraLife();
    }

    private void CreateBarraLife()
    {
        _enemyBarraLifeCreated = Instantiate(barraLifePrefab, barraLifePosition);
        UpdateBarLife(Health, healthMax);
    }


    protected override void UpdateBarLife(float lifeActual, float lifeMax)
    {
        _enemyBarraLifeCreated.ModifyHeath(lifeActual, lifeMax);
    }

    protected override void CharacterDefeated()
    {
        DisabledEnemy();
        EventEnemyDestroyed?.Invoke(_enemyLoot.ExpWin);
        QuestManager.Instance.AddProggress("Mata10", 1);
        QuestManager.Instance.AddProggress("Mata25", 1);
        QuestManager.Instance.AddProggress("Mata50", 1);
    }

    private void DisabledEnemy()
    {
        rastros.SetActive(true);
        _spriteRender.enabled = false;
        _controller.enabled = false;
        _boxCollider2D.isTrigger=true;
        _enemyInteraction.DisabledSpritesSelection();
        _enemyMove.enabled = false;
        _enemyBarraLifeCreated?.gameObject.SetActive(false);
    }
}
