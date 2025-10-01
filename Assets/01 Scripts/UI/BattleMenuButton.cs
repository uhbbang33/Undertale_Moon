using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class BattleMenuButton : MonoBehaviour, ISelectHandler
{
    public void OnSelect(BaseEventData eventData)
    {
        BattleManager.Instance.MenuButtonHighlighted(this);
    }
}
