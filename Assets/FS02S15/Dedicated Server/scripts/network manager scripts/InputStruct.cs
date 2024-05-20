using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public struct InputStruct : INetworkInput
{
    public Vector2 LeftJoystick;

    public Vector2 MoveDirection;

    public float CameraYrotation;
}
