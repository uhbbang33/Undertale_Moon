using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDetails_", menuName = "Scriptable Objects/Player/Player Details")]
public class PlayerDetailsSO : ScriptableObject
{
    [SerializeField] private string _playerName;
    [SerializeField] private int _level;
    //[SerializeField] private int _attackPower;
    //[SerializeField] private int _defencePower;
    [SerializeField] private int _maxHp;
    [SerializeField] private float _moveSpeed;

    public string PlayerName => _playerName;
    public int Level => _level;
    //public int AttackPower => _attackPower;
    //public int DefencePower => _defencePower;
    public int MaxHp => _maxHp;
    public float MoveSpeed => _moveSpeed;

    //[Header("ITEM")]
    //[SerializeField] private List<ItemSO> _initialItemList;
}
