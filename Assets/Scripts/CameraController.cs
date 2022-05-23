using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private PlayerView _playerView;

    private Player _player;
    private float _lookAtSpeed = 2f;

    private void Start()
    {
        _player = _playerView.Player;
        _player.HealthChanged += CheckPlayerHealth;
    }

    private void CheckPlayerHealth()
    {
        if (_player.Health > 0)
            return;

        _player.HealthChanged -= CheckPlayerHealth;
        StartCoroutine(LookAt());
    }

    private IEnumerator LookAt()
    {
        while (Application.isFocused)
        {
            var targetRotation = Quaternion.LookRotation(_playerView.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _lookAtSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnDisable()
    {
        _player.HealthChanged -= CheckPlayerHealth;
    }
}
