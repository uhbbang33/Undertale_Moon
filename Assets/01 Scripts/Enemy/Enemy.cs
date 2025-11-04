using System;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Animator _hitAnim;
    [SerializeField] private FrogHeadMove _headMove;

    public EnemyDetailsSO _enemyDetails;
    private Health _health;

    public event Action OnHit;

    private void Initialize(PlayerDetailsSO playerDetails)
    {
        _health.SetStartHealth(_enemyDetails.MaxHealth);
    }

    public void EnemyHit()
    {
        _hitAnim.SetTrigger("Hit");
    }

    public void AfterHitAnim()
    {
        OnHit.Invoke();

        transform.DOShakePosition(1f, new Vector3(1.5f, 0f, 0f), 6, 0f, true, true, ShakeRandomnessMode.Full);
    }
}
