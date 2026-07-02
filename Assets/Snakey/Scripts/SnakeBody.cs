using System.Collections.Generic;
using UnityEngine;

public class SnakeBody : MonoBehaviour
{
    private Transform _segmentPrefab;
    private float _bodySpacing;
    private List<Transform> _segments;
    private readonly List<Vector3> _waypoints = new();
    public IReadOnlyList<Transform> Segments => _segments ?? new List<Transform>();

    public void Initialize(Vector3 startPosition, Vector3 direction, Transform segmentPrefab, float bodySpacing, List<Transform> segments)
    {
        _segmentPrefab = segmentPrefab;
        _bodySpacing = bodySpacing;
        _segments = segments ?? new List<Transform>();

        if (_segments.Count == 0 || !_segments.Contains(transform))
        {
            _segments.Add(transform);
        }

        _waypoints.Clear();
        for (int i = 0; i < 500; i++)
        {
            _waypoints.Add(startPosition - (direction * i * 0.05f));
        }
    }

    public void HandleHeadPosition(Vector3 headPosition)
    {
        if (_segments == null || _segmentPrefab == null) return;

        if (_waypoints.Count == 0 || _waypoints[0] != headPosition)
        {
            _waypoints.Insert(0, headPosition);
        }

        MoveTailWithWaypoints();
    }

    public void Grow()
    {
        if (_segmentPrefab == null || _segments == null) return;

        Transform newSegment = Instantiate(_segmentPrefab);
        newSegment.SetParent(transform.parent);

        float targetDistanceForNewSegment = _segments.Count * _bodySpacing;
        newSegment.position = GetPositionOnPath(targetDistanceForNewSegment);

        _segments.Add(newSegment);
    }

    public int GetSegmentIndex(Transform segment)
    {
        if (_segments == null) return -1;
        return _segments.IndexOf(segment);
    }

    void MoveTailWithWaypoints()
    {
        if (_segments == null || _segments.Count <= 1) return;

        float targetDistance = _bodySpacing;

        for (int i = 1; i < _segments.Count; i++)
        {
            Transform currentSegment = _segments[i];
            Vector3 targetPosition = GetPositionOnPath(targetDistance);

            currentSegment.position = targetPosition;
            currentSegment.LookAt(_segments[i - 1]);

            targetDistance += _bodySpacing;
        }

        float maxDistanceNeeded = _segments.Count * _bodySpacing;
        int maxWaypointsCount = Mathf.RoundToInt((maxDistanceNeeded / 0.05f) * 2f);
        if (_waypoints.Count > maxWaypointsCount && _waypoints.Count > 500)
        {
            _waypoints.RemoveRange(maxWaypointsCount, _waypoints.Count - maxWaypointsCount);
        }
    }

    Vector3 GetPositionOnPath(float targetDistance)
    {
        if (_waypoints.Count == 0)
        {
            return transform.position;
        }

        if (_waypoints.Count == 1)
        {
            return _waypoints[0];
        }

        float accumulatedDistance = 0f;

        for (int i = 0; i < _waypoints.Count - 1; i++)
        {
            float distBetweenPoints = Vector3.Distance(_waypoints[i], _waypoints[i + 1]);

            if (accumulatedDistance + distBetweenPoints >= targetDistance)
            {
                float remainingDistance = targetDistance - accumulatedDistance;
                float t = remainingDistance / distBetweenPoints;
                return Vector3.Lerp(_waypoints[i], _waypoints[i + 1], t);
            }

            accumulatedDistance += distBetweenPoints;
        }

        Vector3 lastPoint = _waypoints[_waypoints.Count - 1];
        Vector3 secondLastPoint = _waypoints[_waypoints.Count - 2];
        Vector3 extendDirection = (lastPoint - secondLastPoint).normalized;
        float missingDistance = targetDistance - accumulatedDistance;

        return lastPoint + (extendDirection * missingDistance);
    }
}
