using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float _verticleAmplitude;
    [SerializeField] private float _verticleFrequency;
    [SerializeField] private float _horizontalAmplitude;
    [SerializeField] private float _horizontalFrequency;

    private Vector3 _startPosition;
    void Start()
    {
        _startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float newYPosition = _startPosition.y + _verticleAmplitude * Mathf.Sin(Time.time * _verticleFrequency);
        float newXPosition = _startPosition.x + _horizontalAmplitude * Mathf.Sin(Time.time * _horizontalFrequency);

        transform.position = new Vector3(newXPosition, newYPosition, _startPosition.z);
    }
}
