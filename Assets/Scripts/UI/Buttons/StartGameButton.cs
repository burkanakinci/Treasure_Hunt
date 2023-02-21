using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameButton : UIBaseButton
{
    protected override void ButtonClick()
    {
        base.ButtonClick();
        this.gameObject.SetActive(false);
    }
}
