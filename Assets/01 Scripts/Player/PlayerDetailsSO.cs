using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDetails_", menuName = "Scriptable Objects/Player/Player Details")]
public class PlayerDetailsSO : ScriptableObject
{
    [SerializeField] private string _playerName;
    [SerializeField] private int _level;
    [SerializeField] private int _attackPower;
    //[SerializeField] private int _defencePower;
    [SerializeField] private int _maxHealth;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _afterHitInvincibleTime;
    [SerializeField] private Vector3 _lobbyPosition;
    [SerializeField] private Vector3 _battlePosition;

    public string PlayerName => _playerName;
    public int Level => _level;
    public int AttackPower => _attackPower;
    //public int DefencePower => _defencePower;
    public int MaxHealth => _maxHealth;
    public float MoveSpeed => _moveSpeed;
    public float AfterHitInvincibleTime => _afterHitInvincibleTime;
    public Vector3 LobbyPosition
    {
        get { return _lobbyPosition; }
        set { _lobbyPosition = value; }
    }
    public Vector3 BattlePosition => _battlePosition;

    //[Header("ITEM")]
    //[SerializeField] private List<ItemSO> _initialItemList;
}
