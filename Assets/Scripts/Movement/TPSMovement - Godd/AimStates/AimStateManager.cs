using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimStateManager : MonoBehaviour
{
    //public Cinemachine.AxisState xAxis, yAxis;

    public AimBaseState CurrentState;
    public HipFireState Hip = new HipFireState();
    public AimState Aim = new AimState();

    public float MouseSense = 1f;
    public float xAxis, yAxis;
    public Transform CamFollowPosition;

    public Animator CharacterAnimator;
    [Header("Parameters for changing fov when aiming")]
    public CinemachineFreeLook VirtualCamera;
    public float ADSFov = 40f;
    public float CurrentFov;
    public float HipFov;
    public float FovSmoothSpeed = 10;

    [Header("Parameters for Aiming")]
    //public Transform AimPosition;
    public float AimSmoothSpeed = 20f;
    public LayerMask AimLayerMask;

    [Header("")]
    public GameObject Crosshair;


    void Start()
    {
        SwitchState(Hip);
        HipFov = VirtualCamera.m_Lens.FieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        xAxis += Input.GetAxisRaw("Mouse X") * MouseSense;
        yAxis -= Input.GetAxisRaw("Mouse Y") * MouseSense;
        Mathf.Clamp(yAxis, -80, 80);

        VirtualCamera.m_Lens.FieldOfView = Mathf.Lerp(VirtualCamera.m_Lens.FieldOfView, CurrentFov, FovSmoothSpeed * Time.deltaTime);

        Vector2 screenCentre = new Vector2 (Screen.width/2, Screen.height/2);
        Ray ray = Camera.main.ScreenPointToRay(screenCentre);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, AimLayerMask)) 
        {
            //AimPosition.position = Vector3.Lerp(AimPosition.position, hit.point, AimSmoothSpeed * Time.deltaTime);
        }

        CurrentState.UpdateState(this);
    }

    private void FixedUpdate()
    {
        CamFollowPosition.localEulerAngles = new Vector3(yAxis, CamFollowPosition.localEulerAngles.y, CamFollowPosition.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis, transform.eulerAngles.z); 
    }
    public void SwitchState(AimBaseState state)
    {
        CurrentState = state;
        CurrentState.EnterState(this);
    }
}
