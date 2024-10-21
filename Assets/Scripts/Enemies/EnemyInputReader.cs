using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInputReader : MonoBehaviour
{
    public Action onAimActivated;
    public Action onAimDeactivated;

    public Action onCrouchActivated;
    public Action onCrouchDeactivated;

    public Action onJumpPerformed;

    public Action onLockOnToggled;

    public Action onSprintActivated;
    public Action onSprintDeactivated;

    public Action onWalkToggled;

    public Action onUsePerformed;
}
