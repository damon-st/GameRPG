using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float velocity;

    public CharacterAttack CharacterAttack { get; private set; }

    private Rigidbody2D _rigidbody2D;
    private Vector2 direction;
    private EnemigoInteraction enemyObjetivo;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (enemyObjetivo == null) return;
        MoveProyectil();
    }

    private void MoveProyectil()
    {
        direction = enemyObjetivo.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x)* Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        _rigidbody2D.MovePosition(_rigidbody2D.position+direction.normalized*velocity*Time.fixedDeltaTime);
    }

    public void InitializedProyectil(CharacterAttack attack)
    {
        CharacterAttack= attack;
        enemyObjetivo = attack.EnemyObjetivo;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            float damage = CharacterAttack.GetDamage();
            EnemigoVida enemyHealt = enemyObjetivo?.GetComponent<EnemigoVida>();
            enemyHealt?.ReceiverDamage(damage);
            CharacterAttack.EventEnemyDamage?.Invoke(damage, enemyHealt);
            gameObject.SetActive(false);
        }
    }
}
