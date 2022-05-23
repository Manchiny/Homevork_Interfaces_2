using UnityEngine;

public class PlayerControlWindow : MonoBehaviour
{
    [SerializeField] private BasicButton _atackButton;
    [SerializeField] private BasicButton _healButton;

    private Player _player;

    private void OnDisable()
    {
        _atackButton.RemoveAllListeners();
        _healButton.RemoveAllListeners();
    }

    public void Init(Player player)
    {
        _player = player;
        _atackButton.AddListener(_player.GetDamage);
        _healButton.AddListener(_player.Heal);
    }
}
