using UnityEngine;
using UnityEngine.EventSystems;

public enum BattleState
{
    START,
    SELECTMENU,
    PLAYER_ATTACK,
    ENEMY_ATTACK,
    END
}

public class BattleManager : SingletonMonoBehaviour<BattleManager>
{
    [SerializeField] private BattleMenuButton _fightButton;
    [SerializeField] private BattleMenuButton _actButton;
    [SerializeField] private BattleMenuButton _itemButton;
    [SerializeField] private BattleMenuButton _mercyButton;

    [SerializeField] private GameObject _menuHeart;

    private BattleState _state;
    private Vector3 _menuOffset = new Vector3(-38, 0, 0);

    void Start()
    {
        _state = BattleState.START;
        
        EventSystem.current.SetSelectedGameObject(_fightButton.gameObject);

        // TODO: Button.OnClick.Invoke() 로 메뉴 넘어가기
    }

    public void MenuButtonHighlighted(BattleMenuButton btn)
    {
        _menuHeart.transform.position = btn.transform.position + _menuOffset;
    }

}
