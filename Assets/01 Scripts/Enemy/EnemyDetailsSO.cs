using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDetails_", menuName = "Scriptable Objects/Enemy/Enemy Details")]
public class EnemyDetailsSO : MonoBehaviour
{
    [SerializeField] private string _enemyName;
    [SerializeField] private int _attackPower;
    [SerializeField] private int _defencePower;
    [SerializeField] private int _maxHp;

    public string EnemyName => _enemyName;
    public int AttackPower => _attackPower;
    public int DefencePower => _defencePower;
    public int MaxHp => _maxHp;

    //[Header("SKILL")]
    //[SerializeField] private List<GameObject> _skillList;
}
