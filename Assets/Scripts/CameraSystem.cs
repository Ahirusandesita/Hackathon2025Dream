// --------------------------------------------------------- 
// CameraSystem.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 

using UnityEngine;
using System.Collections;
using DG.Tweening;

public class CameraSystem : MonoBehaviour
{
    [SerializeField]
    private Camera _camera = default;
    [SerializeField]
    private Transform _player1Camera = default;
    [SerializeField]
    private Transform _player2Camera = default;
    [SerializeField]
    private float _rotateCameraDuraction = default;
    private TurnManager _turnManager = default;
    private float _initialFieldOfView = default;
    private const float ZoomOutCoefficient = 1.5f;

    private void Awake()
    {
        _turnManager = FindObjectOfType<TurnManager>();
        _turnManager.OnTurnEnd += RotateCamera;
        _initialFieldOfView = _camera.fieldOfView;
    }

    private void Update()
    {
        // Debug
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            RotateCamera(PlayerNumber.Player1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            RotateCamera(PlayerNumber.Player2);
        }
    }

    private void RotateCamera(PlayerNumber playerNumber)
    {
        Transform nextCameraTransform = default;
        switch (playerNumber)
        {
            case PlayerNumber.Player1:
                nextCameraTransform = _player2Camera;
                break;
            case PlayerNumber.Player2:
                nextCameraTransform = _player1Camera;
                break;
        }

        DOVirtual.Float(_initialFieldOfView, _initialFieldOfView * ZoomOutCoefficient, _rotateCameraDuraction / 2, onVirtualUpdate =>
        {
            _camera.fieldOfView = onVirtualUpdate;
        }).OnComplete(() =>
        {
            DOVirtual.Float(transform.position.z, nextCameraTransform.position.z, _rotateCameraDuraction, onVirtualUpdate =>
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, onVirtualUpdate);
            });

            DOVirtual.Float(transform.eulerAngles.y, nextCameraTransform.eulerAngles.y, _rotateCameraDuraction, onVirtualUpdate =>
            {
                transform.rotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, onVirtualUpdate, transform.eulerAngles.z));
            }).OnComplete(() =>
            {

                DOVirtual.Float(_camera.fieldOfView, _initialFieldOfView, _rotateCameraDuraction / 2, onVirtualUpdate =>
                {
                    _camera.fieldOfView = onVirtualUpdate;
                });
            });
        });
    }
}