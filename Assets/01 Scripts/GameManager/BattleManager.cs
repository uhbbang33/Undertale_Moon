using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public enum BattleState
{
    SELECT_MENU,
    SELECT_TARGET,
    PLAYER_ATTACK,
    ENEMY_ATTACK,
    END
}

public class BattleManager : SingletonMonoBehaviour<BattleManager>
{
    [SerializeField] private BattleMenuButton _fightButton;
    [SerializeField] private Button _detailButtons_first;
    [SerializeField] private GameObject _menuHeart;

    [Tooltip("max 6")]
    [SerializeField] private List<EnemyDetailsSO> _enemies;
    [SerializeField] private List<Transform> _enemiesPos;
    [SerializeField] private List<TextMeshProUGUI> _nameTexts;

    [SerializeField] private GameObject _text;
    [SerializeField] private GameObject _menuInBattleBox;
    [SerializeField] private GameObject _attackMode;
    [SerializeField] private GameObject _attackBar;

    private InputActions _inputActions;
    private BattleMenuButton _curMenu;
    [SerializeField] private List<DetailMenuButton> _detailButtons;
    private DetailMenuButton _curDetailMenu;
    private BattleState _battleState;
    private BattleState _prevBattleState;
    private Enemy _curTargetEnemy;

    public Enemy CurTargetEnemy
    {
        get { return _curTargetEnemy; }
        set { _curTargetEnemy = value; }
    }


    private readonly Vector3 _menuOffset = new Vector3(-5, 0, 0);
    private readonly Vector3 _detailMenuOffset = new Vector3(-18, 0, 0);

   
    #region MonoBehaviour

    protected override void Awake()
    {
        base.Awake();

        _inputActions = new InputActions();
    }

    private void OnEnable()
    {
        _inputActions.Enable();
        
        _inputActions.UI.Submit.performed += OnSubmitPerformed;
    }

    private void OnDisable()
    {
        _inputActions.UI.Submit.performed -= OnSubmitPerformed;

        _inputActions.Disable();
    }

    void Start()
    {
        _prevBattleState = BattleState.SELECT_MENU;
        _battleState = BattleState.SELECT_MENU;

        EventSystem.current.SetSelectedGameObject(_fightButton.gameObject);

        InitEnemies();
    }

    #endregion MonoBehaviour

    private void InitEnemies()
    {
        for(int i = 0; i< _enemies.Count; ++i)
        {
            GameObject enemyObject = Instantiate(_enemies[i].EnemyPrfab, _enemiesPos[i].position, Quaternion.identity);

            _detailButtons[i].enemy = enemyObject.GetComponent<Enemy>();
        }

        InitEnemiesNameText();
    }

    private void InitEnemiesNameText()
    {
        for (int i = 0; i < _enemies.Count; ++i)
        {
            _nameTexts[i].transform.parent.gameObject.SetActive(true);
            _nameTexts[i].text = "* " + _enemies[i].EnemyName;
        }
    }

    public void MenuButtonHighlighted(BattleMenuButton btn)
    {
        _menuHeart.transform.position = btn.transform.position + _menuOffset;
        _curMenu = btn;
    }

    public void DetailMenuButtonHighlighted(DetailMenuButton btn)
    {
        _menuHeart.transform.position = btn.transform.position + _detailMenuOffset;
        _curDetailMenu = btn;
    }
    
    public void HighlightFirstDetailMenu()
    {
        EventSystem.current.SetSelectedGameObject(_detailButtons_first.gameObject);
    }

    private void ShowTargetEnemies()
    {
        _text.SetActive(false);
        _menuInBattleBox.SetActive(true);
    }

    public void PlayerAttackMode()
    {
        _menuInBattleBox.SetActive(false);
        _menuHeart.SetActive(false);
        _attackMode.SetActive(true);
    }

    public void EnemyHit()
    {
        _curTargetEnemy.EnemyHit();
    }

    #region INPUT_SYSTEM

    private void OnSubmitPerformed(InputAction.CallbackContext context)
    {
        if (_curMenu == _fightButton)
        {
            _battleState = BattleState.SELECT_TARGET;

            ShowTargetEnemies();

            _curMenu = null;
        }
    }

    #endregion INPUT_SYSTEM
}
