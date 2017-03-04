using UnityEngine;
using System.Collections;
using System;

public class RocketGirlMovement : PlayerMovement
{
    [SerializeField]
    private float _strength = 1, _maxVelocity = 10;

    private Rigidbody2D _refRigidBody { get { return GetComponent<Rigidbody2D>(); } }

    public override void GetPlayerInput()
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                PointerClick(Input.touches[i].position);
            }
        }
        //else
        //{
        //    StopPropursors();
        //}
        if (Input.GetMouseButton(0))
            PointerClick(Input.mousePosition);
    }

    private void PointerClick(Vector3 pos)
    {
        pos = Camera.main.ScreenToWorldPoint(pos);
        Vector3 refpos = pos - transform.position;
        //if (refpos.y < 0)
        //if (_refRigidBody.velocity.magnitude < _maxVelocity)
        //    _refRigidBody.AddForce(transform.up * _strength /** powerPercentage*/, ForceMode2D.Force);
        //else
        _refRigidBody.velocity = refpos * _strength;
        //{
        LookAt(refpos);
        //StopPropursors();
        //}
    }

    private void StopPropursors()
    {
        _refRigidBody.AddForce(transform.up * -_strength * 0.1f, ForceMode2D.Impulse);
        if (_refRigidBody.velocity.y < 0)
        {
            Debug.Log("STOPPPP");
            _refRigidBody.velocity = Vector3.zero;
            _refRigidBody.angularVelocity = 0;
        }
    }

    private void LookAt(Vector3 diff)
    {
        //Vector3 diff = pos - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }
}