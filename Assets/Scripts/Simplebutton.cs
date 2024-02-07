using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Simplebutton : Clickable
{
    public UnityEvent OnRightclick;
    public UnityEvent OnLeftClick;
    public UnityEvent OnholdLeft;
    public UnityEvent OnholdRight;

    public override void Leftclick()
    {
        //Debug.Log("leftist");
        OnLeftClick.Invoke();
    }
    public override void Rightclick()
    {
        OnRightclick.Invoke();
    }
    public override void Lefthold()
    {
        OnholdLeft.Invoke();
    }
    public override void Righthold()
    {
        OnholdRight.Invoke();
    }
}
