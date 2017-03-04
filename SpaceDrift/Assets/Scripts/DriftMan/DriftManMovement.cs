using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DriftManMovement : PlayerMovement
{
    [SerializeField]
    private float _strength = .1f, _maxAlphaDistance = 5;

    [SerializeField]
    private Rigidbody2D _refRocketRight, _refRocketLeft;

    [SerializeField]
    private ParticleSystem _leftparticles, _rightParticles;

    private Transform _refTransform { get { return GetComponent<Transform>(); } }

    public override void GetPlayerInput()
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                PointerClick(Input.touches[i].position);
            }
        }
        else
        {
            StopPropursors();
        }
        if (Input.GetMouseButton(0))
            PointerClick(Input.mousePosition);
    }

    private void PointerClick(Vector3 pos)
    {
        pos = Camera.main.ScreenToWorldPoint(pos);
        if (pos.x > 0)
            ActivateRightPropursor();
        else
            ActivateLeftPropursor();

        //pos.z = transform.position.z;
        //Vector3 forceDirection = transform.position - pos;
        //forceDirection = forceDirection.normalized;

        ////float distance = Mathf.Clamp(Vector2.Distance(pos, _refTransform.position), 0, _maxAlphaDistance);
        ////float secondPropursorPercentage = 1 - (distance / _maxAlphaDistance);
        ////Debug.Log(secondPropursorPercentage);

        //Vector2 direction = pos - _refTransform.localPosition;

        ////if (pos.y - _refTransform.up.y < 0)
        ////{
        ////    ActivateRightPropusor();
        ////    ActivateLeftPropursor();
        ////}
        ////else
        ////{
        //if (pos.x - _refTransform.localPosition.x > 0)
        //{
        //    ActivateRightPropusor();
        //    //if (pos.y < _refTransform.localPosition.y)
        //    //    ActivateLeftPropursor(secondPropursorPercentage);
        //}
        //if (pos.x - _refTransform.localPosition.x < 0)
        //{
        //    ActivateLeftPropursor();
        //    //if (pos.y < _refTransform.localPosition.y)
        //    //    ActivateLeftPropursor(secondPropursorPercentage);
        //}
        ////}
    }

    private void StopPropursors()
    {
        _leftparticles.Stop(true);
        _rightParticles.Stop(true);
    }

    private void ActivateRightPropursor(float powerPercentage = 1)
    {
        _refRocketRight.AddForce(_refTransform.up * _strength * powerPercentage, ForceMode2D.Impulse);
        _rightParticles.Emit((int)(10 * powerPercentage));
        _rightParticles.Play(true);
    }

    private void ActivateLeftPropursor(float powerPercentage = 1)
    {
        _refRocketLeft.AddForce(_refTransform.up * _strength * powerPercentage, ForceMode2D.Impulse);
        _leftparticles.Emit((int)(10 * powerPercentage));
        _leftparticles.Play(true);
    }
}