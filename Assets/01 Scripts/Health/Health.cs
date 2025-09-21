using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    private int _maxHealth;
    private int _currentHealth;

    private bool _isInvincible;

    private Player _player;
    private Enemy _enemy;

    private HealthEvent _healthEvent;
    private WaitForSeconds _waitForSecondsInvincibleTime;

    Coroutine _routine;

    private void Awake()
    {
        _healthEvent = GetComponent<HealthEvent>();

        _player = GetComponent<Player>();
        _enemy = GetComponent<Enemy>();
    }

    public void SetStartHealth(int maxHealth)
    {
        if (_player != null)
        {
            _maxHealth = maxHealth;
            _waitForSecondsInvincibleTime = new WaitForSeconds(GameResources.Instance.PlayerDetails.AfterHitInvincibleTime);
        }
        else if (_enemy != null)
        {
            _maxHealth = maxHealth;
        }

        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        if (_isInvincible)
            return;

        _currentHealth -= damageAmount;

        if(_routine != null)
            StopCoroutine(_routine);

        _routine = StartCoroutine(InvincibleRoutine());
    }

    public void IncreaseHealth(int increaseAmount)
    {
        _currentHealth += increaseAmount;

        if (_currentHealth > _maxHealth)
            _currentHealth = _maxHealth;
    }


    // player, enemy, UI에서 구독
    private void CallHealthEvent(bool isIncreaseHealth, int healthAmount)
    {
        _healthEvent.CallHealthChangedEvent(isIncreaseHealth, healthAmount);
    }

    private IEnumerator InvincibleRoutine()
    {
        _isInvincible = true;
        yield return _waitForSecondsInvincibleTime;
        _isInvincible = false;
    }
}
