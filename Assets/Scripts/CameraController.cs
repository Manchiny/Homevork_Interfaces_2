using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private PlayerView _player;

    private Camera _camera;
    private float _lookAtSpeed = 2f;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Start()
    {
        _player.Player.Died += StartLookAt;
    }

    private void StartLookAt()
    {
        _player.Player.Died -= StartLookAt;
        StartCoroutine(LookAt());
    }

    private IEnumerator LookAt()
    {
        while (Application.isFocused)
        {
            var targetRotation = Quaternion.LookRotation(_player.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _lookAtSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnDisable()
    {
        _player.Player.Died -= StartLookAt;
    }
}
