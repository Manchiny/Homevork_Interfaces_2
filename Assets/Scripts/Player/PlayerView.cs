using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private PlayerControlWindow _controlWindow;
    [Space]
    [SerializeField] private Animator _animator;


    public Player Player { get; private set; }
    public HealthBar HealthBar => _healthBar;
    public PlayerControlWindow ControlWindow => _controlWindow;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        Player = new Player(this);
    }

    public void OnGetDamage()
    {
        _animator.StopPlayback();
        _animator.CrossFade(AnimationsHashBase.GetDamage, 0.025f);
    }

    public void OnDie()
    {
        _animator.CrossFade(AnimationsHashBase.Dying, 0.2f);
    }
}
