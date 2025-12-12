using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Transform _healthBarPosition;

    private GameObject _healthBarUI;
    private Slider _slider;

    private WaitForSeconds _waitForReduceSlider = new WaitForSeconds(2f);

    private void Start()
    {
        _healthBarUI = Instantiate(GameResources.Instance.EnemyHPBarPrefab,
            _healthBarPosition.position,
            Quaternion.identity,
            BattleManager.Instance.BattleUI.transform);

        _slider = _healthBarUI.GetComponentInChildren<Slider>();
    }

    public void ReduceHealthGauge(int maxHealth, int currentHealth, int reduceAmount)
    {
        _healthBarUI.SetActive(true);

        _slider.value = (float)currentHealth / (float)maxHealth;

        currentHealth -= reduceAmount;
        if (currentHealth < 0) currentHealth = 0;

        float targetValue = (float)currentHealth / (float)maxHealth;

        _slider.DOValue(targetValue, 1f);

        StartCoroutine(ReduceSliderValueRoutine(targetValue));
    }

    private IEnumerator ReduceSliderValueRoutine(float targetValue)
    {
        yield return _waitForReduceSlider;

        _healthBarUI.SetActive(false);
    }
}
