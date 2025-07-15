using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyDetailsSO _enemyDetails;
    private Health _health;

    private void Initialize(PlayerDetailsSO playerDetails)
    {
        _health.SetStartHealth(_enemyDetails.MaxHealth);
    }
}
