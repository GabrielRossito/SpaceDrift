using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private float _distance = 10, _followSpeed = 1;

    private Vector3 _positionTo;
    private bool _started;
    private float _time = 0;

    public void MoveTo(Vector3 pos)
    {
        pos.z -= _distance;
        pos.x = 0;
        //transform.position = pos;
        if (pos.y > transform.position.y)
        {
            _positionTo = pos;
            //transform.position = Vector3.Lerp(transform.position, _positionTo, _time * _followSpeed);
            if (!_started)
                StartCoroutine(MoveToRoutine());
        }
    }

    private IEnumerator MoveToRoutine()
    {
        _started = true;
        while (true)
        {
            transform.position = Vector3.Lerp(transform.position, _positionTo, _time * _followSpeed);
            _time += Time.deltaTime;
            yield return null;
        }
    }
}