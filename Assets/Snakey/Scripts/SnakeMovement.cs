using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SnakeBody))]
[RequireComponent(typeof(SnakeDeathHandler))]
public class SnakeMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 7f;
    public float steerSpeed = 180f;
    public float bodySpacing = 0.95f;

    [Header("Body Settings")]
    public Transform segmentPrefab;

    public IReadOnlyList<Transform> Segments => _snakeBody?.Segments;

    private Vector3 _direction = Vector3.forward;
    private bool _isStopped = false;
    private SnakeBody _snakeBody;
    private SnakeDeathHandler _deathHandler;
    private IInputReader _inputReader;

    void Awake()
    {
        _snakeBody = GetComponent<SnakeBody>();
        _deathHandler = GetComponent<SnakeDeathHandler>();

        #if UNITY_ANDROID || UNITY_IOS
            _inputReader = GetComponentInChildren<MobileInputReader>();
            if (_inputReader == null)
            {
                Debug.LogError("MobileInputReader missing from the scene!");
            }
        #else
            _inputReader = GetComponentInChildren<SnakeInput>();
            if (_inputReader == null)
            {
                Debug.LogError("SnakeInput missing from the scene!");
            }
        #endif
    }

    void Start()
    {
        _snakeBody?.Initialize(transform.position, _direction, segmentPrefab, bodySpacing, new List<Transform> { transform });
    }

    private void OnEnable()
    {
        if (_inputReader != null)
        {
            _inputReader.OnDirectionChanged += SetDirection;
        }
    }

    private void OnDisable()
    {
        if (_inputReader != null)
        {
            _inputReader.OnDirectionChanged -= SetDirection;
        }
    }

    public void SetDirection(Vector3 newDirection)
    {
        if (newDirection == Vector3.forward && _direction == Vector3.back) return;
        if (newDirection == Vector3.back && _direction == Vector3.forward) return;
        if (newDirection == Vector3.left && _direction == Vector3.right) return;
        if (newDirection == Vector3.right && _direction == Vector3.left) return;

        _direction = newDirection;
    }

    void Update()
    {
        if (_isStopped)
        {
            return;
        }

        Vector3 previousPosition = transform.position;
        transform.Translate(_direction * moveSpeed * Time.deltaTime, Space.World);

        if (_direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, steerSpeed * Time.deltaTime);
        }

        if (transform.position != previousPosition)
        {
            _snakeBody?.HandleHeadPosition(transform.position);
        }
    }

    public void Grow()
    {
        _snakeBody?.Grow();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SnakeBody") || other.CompareTag("Wall"))
        {
            int segmentIndex = _snakeBody?.GetSegmentIndex(other.transform) ?? -1;
            if (segmentIndex > 0 && segmentIndex <= 3)
            {
                return;
            }

            StopSnake();
            _deathHandler?.TriggerGameOver();
        }
    }

    void StopSnake()
    {
        _isStopped = true;
        _direction = Vector3.zero;
        moveSpeed = 0f;
    }
}