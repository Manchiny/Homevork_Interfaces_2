using UnityEngine;

public static class AnimationsHashBase
{
    public static int Idle { get; private set; } = Animator.StringToHash("Idle");
    public static int GetDamage { get; private set; } = Animator.StringToHash("GetDamage");
    public static int Dying { get; private set; } = Animator.StringToHash("Dying");
}
