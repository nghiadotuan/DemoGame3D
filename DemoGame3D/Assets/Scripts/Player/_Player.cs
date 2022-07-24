using UnityEngine;

public class _Player : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Animator _animator;
    
    private float _forwardAmount;
    private float _turnAmount;
    private bool _isCrouching;
    private bool _isGround;
    private Vector3 _vtCamForward;
    private Vector3 _vtMove;

    private Camera _camera;
    
    private static readonly int _forward = Animator.StringToHash("Forward");
    private static readonly int _turn = Animator.StringToHash("Turn");

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Start()
    {
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotationX
                                 | RigidbodyConstraints.FreezeRotationY
                                 | RigidbodyConstraints.FreezeRotationZ;
    }

    private void Update()
    {
        _forwardAmount = _vtMove.z;
        _turnAmount = Mathf.Atan2(_vtMove.x, _vtMove.z);
        _animator.SetFloat(_forward, _forwardAmount, .1f, Time.deltaTime);
        _animator.SetFloat(_turn, _turnAmount, .1f, Time.deltaTime);
    }

    private void FixedUpdate()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        _vtCamForward = Vector3.Scale(_camera.transform.forward, new Vector3(1, 0, 1)).normalized;
        _vtMove = v * _vtCamForward + h * _camera.transform.right;
        if (_vtMove.magnitude > 1) _vtMove.Normalize();
        
    }
}