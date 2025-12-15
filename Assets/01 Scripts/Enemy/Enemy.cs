using System;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(EnemyDamage))]
[RequireComponent(typeof(EnemyHealthBar))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Animator _hitAnim;
    [SerializeField] private FroggitHeadMove _headMove;
    [SerializeField] private EnemyDetailsSO _enemyDetails;
    [SerializeField] private List<GameObject> _skillObjects;
    
    private Health _health;
    private EnemyHealthBar _healthBar;
    private EnemyDamage _damage;
    
    public int DefensePower => _enemyDetails.DefencePower;

    public event Action OnHit;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _healthBar = GetComponent<EnemyHealthBar>();
        _damage = GetComponent<EnemyDamage>();
    }

    private void Start()
    {
        _health.SetStartHealth(_enemyDetails.MaxHealth);
    }

    public void EnemyHit(int damageAmount)
    {
        _hitAnim.SetTrigger("Hit");

        _healthBar.ReduceHealthGauge(_health.MaxHealth, _health.CurrentHealth, damageAmount);

        _health.TakeDamage(damageAmount);

        _damage.ShowDamage(damageAmount);
    }

    public void AfterHitAnim()
    {
        OnHit.Invoke();

        transform.DOShakePosition(1f, new Vector3(1.5f, 0f, 0f), 6, 0f, true, true, ShakeRandomnessMode.Full);
    }

    public void EnableSkill()
    {
        int skillIdx= UnityEngine.Random.Range(0, _skillObjects.Count);

        Debug.Log(_skillObjects[skillIdx].name);
        _skillObjects[skillIdx].SetActive(true);
    }
}
