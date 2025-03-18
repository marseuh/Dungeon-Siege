using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualJoystickFloating : VirtualJoystick
{
    [SerializeField] private bool hideOnPointerUp = false;
    [SerializeField] private bool centralizeOnPointerUp = true;

    protected override void Awake()
    {
        _hideOnPointerUp = hideOnPointerUp;
        _centralizeOnPointerUp = centralizeOnPointerUp;
     
        base.Awake();
    }

}
