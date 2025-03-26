using UnityEngine;

public class UIPage : MonoBehaviour
{
    private RectTransform rectTrans;
    public RectTransform RectTrans { get { if (rectTrans == null) { rectTrans = GetComponent<RectTransform>(); } return rectTrans; } }
    public virtual void Enter(){}
    public virtual void Exit(){}
    public void SetActivate(bool activate)
    {
        gameObject.SetActive(activate);
    }
    public bool IsActivate()
    {
        return gameObject.activeSelf;
    }
    public virtual bool OnKeyInput()
    {
        return false;
    }
}
