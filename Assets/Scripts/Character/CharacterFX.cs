using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeCharacter
{
    Player,
    IA
}

public class CharacterFX : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private GameObject canvasTxtAnimPrefab;
    [SerializeField] private Transform canvasTextPosition;

    [Header("Pooler")]
    [SerializeField] private ObjectPooler pooler;

    [Header("Tipo")]
    [SerializeField] private TypeCharacter typeCharacter;

    private EnemigoVida _enemyHealth;

    private void Awake()
    {
        _enemyHealth = GetComponent<EnemigoVida>();
    }

    private void Start()
    {
        pooler.CreatePooler(canvasTxtAnimPrefab);
    }

    private IEnumerator IEShowText(float amount,Color color)
    {
        GameObject newTextGO = pooler.GetInstance();
        TextoAnimation text = newTextGO.GetComponent<TextoAnimation>();
        text.EstablishText(amount,color);
        newTextGO.transform.SetParent(canvasTextPosition);
        newTextGO.transform.position = canvasTextPosition.position;
        newTextGO.SetActive(true);
        yield return new WaitForSeconds(1f);
        newTextGO.SetActive(false);
        newTextGO.transform.SetParent(pooler.listContainer.transform);
    }

    private void ResponseAmangeReceiver(float damage)
    {
        if (typeCharacter == TypeCharacter.Player)
        {
            StartCoroutine(IEShowText(damage,Color.black));
        }
    }

    private void ResponseDamageToWardEnemy(float damage,EnemigoVida enemyHealth)
    {
        if (typeCharacter == TypeCharacter.IA && _enemyHealth == enemyHealth)
        {
            StartCoroutine(IEShowText(damage,Color.red));
        }
    }

    private void OnEnable()
    {
        IAController.EventDamageDone += ResponseAmangeReceiver;
        CharacterAttack.EventEnemyDamage += ResponseDamageToWardEnemy;
    }

    private void OnDisable()
    {
        IAController.EventDamageDone -= ResponseAmangeReceiver;
        CharacterAttack.EventEnemyDamage -= ResponseDamageToWardEnemy;
    }
}
