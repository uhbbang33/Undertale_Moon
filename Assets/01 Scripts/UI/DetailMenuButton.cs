using UnityEngine;
using UnityEngine.EventSystems;

public class DetailMenuButton : MonoBehaviour, ISelectHandler, ISubmitHandler
{
    public Enemy enemy { get; set; }

    public void OnSelect(BaseEventData eventData)
    {
        BattleManager.Instance.DetailMenuButtonHighlighted(this);
    }
    
    public void OnSubmit(BaseEventData eventData)
    {
        BattleManager.Instance.CurTargetEnemy = enemy;

        BattleManager.Instance.ChangeBattleState(BattleState.PLAYER_ATTACK);
    }
}
