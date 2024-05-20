using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : NetworkBehaviour
{
    /// <summary>
    /// Locomotion
    /// </summary>
    public virtual void Locomotion() { }

    /// <summary>
    /// Rotation
    /// </summary>
    public virtual void Rotation() { }
}
 