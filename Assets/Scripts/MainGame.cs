using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainGame : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement[] CharacterPrebs;

    [SerializeField]
    private CameraMovement _refCamraMovement;

    [SerializeField]
    private BackgroundCreator _refBackgroundCreator;

    private PlayerMovement _refPlayerMovement;

    private void Start()
    {
        Input.backButtonLeavesApp = true;
        SelectPlayerCharacter();
        _refBackgroundCreator.Initialize();
    }

    private void Update()
    {
        _refPlayerMovement.GetPlayerInput();
        _refBackgroundCreator.ValidatePositions(_refPlayerMovement.transform.position);
    }

    private void LateUpdate()
    {
        _refCamraMovement.MoveTo(_refPlayerMovement.transform.position);
    }

    private void FixedUpdate()
    {
        if (!_refPlayerMovement.GetComponent<Renderer>().isVisible)
            SceneManager.LoadScene(0);
    }

    public void SelectPlayerCharacter(int character = 0)
    {
        Vector3 pos = Vector3.zero;
        Quaternion rot = Quaternion.identity;
        if (_refPlayerMovement != null)
        {
            pos = _refPlayerMovement.transform.position;
            rot = _refPlayerMovement.transform.rotation;
            Destroy(_refPlayerMovement.gameObject);
        }

        PlayerMovement playerChar = CharacterPrebs[0];
        if (character <= CharacterPrebs.Length)
            playerChar = CharacterPrebs[character];
        _refPlayerMovement = Instantiate(playerChar);
        _refPlayerMovement.transform.position = pos;
        _refPlayerMovement.transform.rotation = rot;
    }
}