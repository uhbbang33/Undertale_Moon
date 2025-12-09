using System;
using UnityEngine;
using DG.Tweening;
using System.Collections;

[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Animator _hitAnim;
    [SerializeField] private FrogHeadMove _headMove;
    [SerializeField] private EnemyDetailsSO _enemyDetails;
    [SerializeField] private Transform _damagePosition;
    
    private Health _health;

    private readonly WaitForSeconds _waitForDamageShow = new WaitForSeconds(2f);
    private readonly Vector3 _damageInterval = new Vector3(4f, 0f, 0f);

    public int DefensePower => _enemyDetails.DefencePower;

    public event Action OnHit;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _health.SetStartHealth(_enemyDetails.MaxHealth);
    }

    public void EnemyHit(int damageAmount)
    {
        _hitAnim.SetTrigger("Hit");

        _health.TakeDamage(damageAmount);
        ShowDamage(damageAmount);
    }

    public void AfterHitAnim()
    {
        OnHit.Invoke();

        transform.DOShakePosition(1f, new Vector3(1.5f, 0f, 0f), 6, 0f, true, true, ShakeRandomnessMode.Full);
    }

    private void ShowDamage(int damageAmount)
    {
        string damageStr = damageAmount.ToString();

        Vector3 pos = _damagePosition.position - (damageStr.Length / 2) * _damageInterval;

        if (damageStr.Length % 2 == 0)
            pos += _damageInterval / 2;

        foreach (char c in damageStr)
        {
            int curNum = c - '0';

            GameObject obj = PoolManager.Instance.GetObject("Number" + curNum);
            obj.transform.position = pos;

            pos += _damageInterval;

            StartCoroutine(DamageRemoveRoutine(obj));
        }
    }

    IEnumerator DamageRemoveRoutine(GameObject damageObj)
    {
        yield return _waitForDamageShow;

        PoolManager.Instance.ReturnObject(damageObj);
    }
}
