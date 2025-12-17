using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BattleMenuButton : MonoBehaviour, ISelectHandler, ISubmitHandler
{
    private Button _btn;
    private MenuButton _menu;

    public MenuButton Menu { set {  _menu = value; } }

    private void Start()
    {
        _btn = GetComponent<Button>();
    }

    public void OnSelect(BaseEventData eventData)
    {
        BattleManager.Instance.MenuButtonHighlighted(this);
    }

    public void OnSubmit(BaseEventData eventData)
    {
        _btn.image.sprite = _btn.spriteState.highlightedSprite;
        
        BattleState nextState = BattleState.SELECT_MENU;

        if (_menu == MenuButton.FIGHT)
            nextState = BattleState.SELECT_ENEMY;
        else if (_menu == MenuButton.ACT)
            nextState = BattleState.SELECT_ACT;
        else if (_menu == MenuButton.ITEM)
            nextState = BattleState.SELECT_ITEM;
        else if (_menu == MenuButton.MERCY)
            nextState = BattleState.SELECT_MERCY;

        BattleManager.Instance.ChangeBattleState(nextState);
    }
}
