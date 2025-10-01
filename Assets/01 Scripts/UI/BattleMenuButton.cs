using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class BattleMenuButton : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public void OnSelect(BaseEventData eventData)
    {
        BattleManager.Instance.MenuButtonHighlighted(this);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        
    }
  
}
