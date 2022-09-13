using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    [SerializeField]
    private GameObject _plane;
    [SerializeField]
    private float _threshold;

    private Rigidbody _ballRb;
    private Vector2 edges;
    private bool firstForce = true;

    private void Awake()
    {
        _ballRb = this.GetComponent<Rigidbody>();
        edges = GetXEdges();
    }

    private void FixedUpdate()
    {
        if (firstForce)
        {
            _ballRb.AddForce(Vector3.right * _speed, ForceMode.Impulse);
            firstForce = false;
        }
        if(Mathf.Abs(_ballRb.transform.position.x - edges.x) <= _threshold)
        {
            _ballRb.AddForce(Vector3.right * _speed, ForceMode.Impulse);
        } else if(Mathf.Abs(_ballRb.transform.position.x - edges.y) <= _threshold)
        {
            _ballRb.AddForce(Vector3.right * -_speed, ForceMode.Impulse);
        }
    }

    private Vector2 GetXEdges()
    {
        float minX = Mathf.Infinity;
        float maxX = Mathf.NegativeInfinity;
        foreach (Vector3 vert in _plane.GetComponent<MeshFilter>().sharedMesh.vertices)
        {
            if (vert.x > maxX)
                maxX = vert.x;
            if (vert.x < minX)
                minX = vert.x;
        }

        return new Vector2(minX, maxX);
    }
}
