using System;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(EnemyDamage))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Animator _hitAnim;
    [SerializeField] private FrogHeadMove _headMove;
    [SerializeField] private EnemyDetailsSO _enemyDetails;
    
    private Health _health;
    private EnemyDamage _damage;
    
    public int DefensePower => _enemyDetails.DefencePower;

    public event Action OnHit;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _health.SetStartHealth(_enemyDetails.MaxHealth);

        _damage = GetComponent<EnemyDamage>();
    }

    public void EnemyHit(int damageAmount)
    {
        _hitAnim.SetTrigger("Hit");

        _health.TakeDamage(damageAmount);

        _damage.ShowDamage(damageAmount);
    }

    public void AfterHitAnim()
    {
        OnHit.Invoke();

        transform.DOShakePosition(1f, new Vector3(1.5f, 0f, 0f), 6, 0f, true, true, ShakeRandomnessMode.Full);
    }
}
