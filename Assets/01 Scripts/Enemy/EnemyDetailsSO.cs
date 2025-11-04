using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDetails_", menuName = "Scriptable Objects/Enemy/Enemy Details")]
public class EnemyDetailsSO : ScriptableObject
{
    [SerializeField] private GameObject _enemyPrefab;

    [SerializeField] private string _enemyName;
    [SerializeField] private int _attackPower;
    [SerializeField] private int _defencePower;
    [SerializeField] private int _maxHp;

    [SerializeField] private string _startText;
    [SerializeField] private string _signatureText;
    [SerializeField] private string[] _acts;
    [SerializeField] private string[] _actText;

    public GameObject EnemyPrfab => _enemyPrefab;
    public string EnemyName => _enemyName;
    public int AttackPower => _attackPower;
    public int DefencePower => _defencePower;
    public int MaxHealth => _maxHp;

    public string StartText => _startText;
    public string SignatureText => _signatureText;
    public string[] Acts => _acts;
    public string[] ActText => _actText;
}
