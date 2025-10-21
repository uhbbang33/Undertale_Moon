using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BattleMenuButton : MonoBehaviour, ISelectHandler, ISubmitHandler
{
    private Button _btn;

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
        BattleManager.Instance.HighlightFirstDetailMenu();
        _btn.image.sprite = _btn.spriteState.highlightedSprite;
    }
}
