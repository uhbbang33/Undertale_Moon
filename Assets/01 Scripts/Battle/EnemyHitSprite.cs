using UnityEngine;

public class EnemyHitSprite : MonoBehaviour
{
    private Enemy _enemy;

    private void Start()
    {
        _enemy = GetComponentInParent<Enemy>();
    }

    // Anim event
    public void AfterHitEvent()
    {
        _enemy.AfterHitAnim();
    }
}
