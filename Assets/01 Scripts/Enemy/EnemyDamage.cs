using System.Collections;
using UnityEngine;
using DG.Tweening;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private Transform _damagePosition;

    private readonly WaitForSeconds _waitForDamageShow = new WaitForSeconds(2f);
    private readonly Vector3 _damageInterval = new Vector3(4f, 0f, 0f);

    public void ShowDamage(int damageAmount)
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
        damageObj.transform.DOJump(damageObj.transform.position, 2f, 1, 0.6f);

        yield return _waitForDamageShow;

        PoolManager.Instance.ReturnObject(damageObj);
    }
}
