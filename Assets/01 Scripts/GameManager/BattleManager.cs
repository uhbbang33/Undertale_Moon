using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using System;

public enum BattleState
{
    SELECT_MENU,
    SELECT_ENEMY,
    SELECT_ACT,
    SELECT_ITEM,
    SELECT_MERCY,
    PLAYER_ATTACK,
    ENEMY_ATTACK,
    SHOW_ITEM_INFO,
    END
}

public enum MenuButton { FIGHT, ACT, ITEM, MERCY }

public class BattleManager : SingletonMonoBehaviour<BattleManager>
{
    [SerializeField] private List<EnemyDetailsSO> _enemies;
    [SerializeField] private List<Transform> _enemiesPos;
    [SerializeField] private List<TextMeshProUGUI> _nameTexts;
    [SerializeField] private List<BattleMenuButton> _menuButtons;
    [SerializeField] private List<DetailMenuButton> _detailButtons;
    
    [Space(10)]
    [Header("GameObject")]
    [SerializeField] private GameObject _text;
    [SerializeField] private GameObject _menuInBattleBox;
    [SerializeField] private GameObject _playerAttackMode;
    [SerializeField] private GameObject _battleUI;
    [SerializeField] private GameObject _menuHeart;

    [Space(10)]
    [Header("etc")]
    [SerializeField] private AttackBar _attackBar;
    [SerializeField] private BattleBox _battleBox;

    [SerializeField] private float _battleSize_Height;
    [SerializeField] private float _battleSize_Width;

    private InputActions _inputActions;
    private BattleMenuButton _curMenu;
    private DetailMenuButton _curDetailMenu;
    private Enemy _curTargetEnemy;
    private Stack<BattleState> _menuStack;

    // TODO : Init에서 생성된 Player Heart 넣기 ( SerializeField 삭제 )
    [SerializeField] private GameObject _playerHeart;

    // TODO : GameManager에서 받기
    [SerializeField] private Player _player;


    #region Property

    public Enemy CurTargetEnemy
    {
        get { return _curTargetEnemy; }
        set { _curTargetEnemy = value; }
    }

    public GameObject BattleUI { get { return _battleUI; } }

    public Vector3 PlayerPosition { get { return _playerHeart.transform.position; } }

    #endregion Property

    private readonly Vector3 _menuOffset = new Vector3(-5, 0, 0);
    private readonly Vector3 _detailMenuOffset = new Vector3(-18, 0, 0);

    #region MonoBehaviour

    protected override void Awake()
    {
        base.Awake();

        _inputActions = new InputActions();
        _menuStack = new Stack<BattleState>();
    }

    private void OnEnable()
    {
        _inputActions.Enable();
        
        _inputActions.UI.Cancel.performed += OnCancelPerformed;

        _attackBar.OnAttack += PlayerAttack;
    }

    private void OnDisable()
    {
        _inputActions.UI.Cancel.performed -= OnCancelPerformed;

        _attackBar.OnAttack -= PlayerAttack;

        _inputActions.Disable();
    }

    void Start()
    {
        //_player = GameManager.Instance.player;

        _menuStack.Push(BattleState.SELECT_MENU);
        ChangeBattleState(BattleState.SELECT_MENU);
        
        for(int i =0; i < _menuButtons.Count; ++i)
            _menuButtons[i].Menu = (MenuButton)i;

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

    public void ChangeBattleState(BattleState current)
    {
        if (current != BattleState.SELECT_MENU)
        {
            _menuStack.Push(current);
        }

        switch (current)
        {
            case BattleState.SELECT_MENU:
                _menuInBattleBox.SetActive(false);
                _text.SetActive(false);

                EventSystem.current.SetSelectedGameObject(_menuButtons[0].gameObject);
                break;

            case BattleState.SELECT_ENEMY:
                ShowEnemiesName();

                EventSystem.current.SetSelectedGameObject(_detailButtons[0].gameObject);
                break;

            case BattleState.SELECT_ACT:
                break;

            case BattleState.SELECT_ITEM:
                break;

            case BattleState.SELECT_MERCY:
                break;

            case BattleState.PLAYER_ATTACK:
                PlayerAttackMode();
                break;

            case BattleState.ENEMY_ATTACK:
                while (_menuStack.Peek() != BattleState.SELECT_MENU)
                {
                    _menuStack.Pop();
                }

                EnemyAttackMode();
                break;

            case BattleState.SHOW_ITEM_INFO:

                break;
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
    
    private void ShowEnemiesName()
    {
        _text.SetActive(false);
        _menuInBattleBox.SetActive(true);
    }

    private void PlayerAttackMode()
    {
        _menuInBattleBox.SetActive(false);
        _menuHeart.SetActive(false);
        _playerAttackMode.SetActive(true);
    }

    private void EnemyAttackMode()
    {
        _playerAttackMode.SetActive(false);

        _battleBox.ChangeWidth(_battleSize_Width);
        _battleBox.ChangeHeight(_battleSize_Height);

        _playerHeart.SetActive(true);

        _curTargetEnemy.EnableSkill();
    }

    public void PlayerAttack(float bonus)
    {
        int r = UnityEngine.Random.Range(0, 3);

        // Round((PlayerATK - EnemyDEF + r) * bonus)
        float damageAmount = Mathf.Round(((float)_player.PlayerAttackPower -
        (float)_curTargetEnemy.DefensePower + r) * bonus);

        if(damageAmount < 0)
            Debug.LogError("Player Attack Power is lower than Enemy Defense Power");

        _curTargetEnemy.EnemyHit((int)damageAmount);
    }

    #region INPUT_SYSTEM

    private void OnCancelPerformed(InputAction.CallbackContext context)
    {
        if (_menuStack.Peek() != BattleState.SELECT_MENU)
        {
            _menuStack.Pop();
            ChangeBattleState(_menuStack.Peek());
        }
    }

    #endregion INPUT_SYSTEM
}
