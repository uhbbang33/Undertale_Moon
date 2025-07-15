using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerDetailsSO _playerDetails;
    private Health _health;

    private void Initialize(PlayerDetailsSO playerDetails)
    {
        _health.SetStartHealth(_playerDetails.MaxHealth);
    }
}
