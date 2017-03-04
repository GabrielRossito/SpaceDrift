using UnityEngine;
using System.Collections;

public class PlayerOutside : MonoBehaviour
{
    private Transform _spaceChibi { get { return GetComponent<Transform>(); } }
    private Renderer _render { get { return GetComponent<Renderer>(); } }

    private Camera _mainCamera;

    public void Initialize()
    {
        _mainCamera = Camera.main;
    }

    public bool ValidatePosition()
    {
        return _render.isVisible;
    }
}