using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonMove : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 6.0f;
    public float RotationSmoothTime = 0.1f;

    [Header("Slope & Step Settings")]
    [Tooltip("Maximum slope angle the character can climb (in degrees)")]
    [Range(0f, 90f)] public float slopeLimit = 45.0f;

    [Tooltip("Maximum height the character can step up")]
    [Range(0f, 1f)] public float stepOffset = 0.3f;

    [Tooltip("Small gap around the character to prevent clipping")]
    [Range(0f, 0.2f)] public float skinWidth = 0.08f;

    [Header("Jump Settings")]
    [Tooltip("Jump height in meters")]
    public float jumpHeight = 1.5f;

    [Tooltip("Gravity applied to the character")]
    public float gravity = -15f;

    [Tooltip("Time required to pass before being able to jump again")]
    public float jumpTimeout = 0.1f;

    private CharacterController _controller;
    private GameObject _mainCamera;
    private float _targetRot = 0.0f;
    private float _rotationVelocity;
    private Vector2 _move;
    private float _verticalVelocity;
    private float _jumpTimeoutDelta;
    private bool _jumpInput;

    // 新增的公共属性
    public bool IsMoving { get; private set; } // 是否在移动
    public Vector3 MoveDirection { get; private set; } // 移动方向
    public float CurrentSpeed { get; private set; } // 当前速度

    void Start()
    {
        if (_mainCamera == null)
        {
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }

        _controller = GetComponent<CharacterController>();
        ApplyCharacterSettings();
        _jumpTimeoutDelta = jumpTimeout;
    }

    void Update()
    {
        HandleJump();
        HandleMovement();
        ApplyGravity();
    }

    private void ApplyCharacterSettings()
    {
        _controller.slopeLimit = slopeLimit;
        _controller.stepOffset = stepOffset;
        _controller.skinWidth = skinWidth;
    }

    private void HandleMovement()
    {
        Vector3 velocity = Vector3.zero;
        IsMoving = _move != Vector2.zero; // 更新移动状态

        if (IsMoving)
        {
            Vector3 inputDir = new Vector3(_move.x, 0.0f, _move.y).normalized;
            _targetRot = Mathf.Atan2(inputDir.x, inputDir.z) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;

            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRot, ref _rotationVelocity, RotationSmoothTime);
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);

            Vector3 targetDir = Quaternion.Euler(0.0f, _targetRot, 0.0f) * Vector3.forward;
            velocity = targetDir.normalized * (speed * Time.deltaTime);

            // 更新移动方向和速度
            MoveDirection = targetDir.normalized;
            CurrentSpeed = speed;
        }
        else
        {
            // 不在移动时重置方向和速度
            MoveDirection = Vector3.zero;
            CurrentSpeed = 0f;
        }

        velocity.y = _verticalVelocity * Time.deltaTime;
        _controller.Move(velocity);
    }

    private void HandleJump()
    {
        if (_controller.isGrounded)
        {
            _jumpTimeoutDelta = jumpTimeout;
        }
        else
        {
            _jumpTimeoutDelta -= Time.deltaTime;
        }

        if (_jumpInput && _jumpTimeoutDelta <= 0.0f && _controller.isGrounded)
        {
            _verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            _jumpInput = false;
        }
    }

    private void ApplyGravity()
    {
        if (_controller.isGrounded)
        {
            if (_verticalVelocity < 0.0f)
            {
                _verticalVelocity = -1f;
            }
        }
        else
        {
            _verticalVelocity += gravity * Time.deltaTime;
        }
    }

    void OnMove(InputValue value)
    {
        _move = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        _jumpInput = value.isPressed;
    }

    private void OnValidate()
    {
        if (_controller != null)
        {
            ApplyCharacterSettings();
        }
    }
}