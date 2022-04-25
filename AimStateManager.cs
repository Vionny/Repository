using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AimStateManager : MonoBehaviour
{
    AimBaseState currentState;
    public AimState Aim = new AimState();
    RifleFire rifle = new RifleFire();
    public Cinemachine.AxisState x, y;
    [SerializeField] Transform camPos;
    [HideInInspector] public Animator animate;
    [HideInInspector] public CinemachineVirtualCamera vCam;
    public float adsFOV = 40;
    public float hipFOV;
    public float currentFOV;
    public float FOVSmooth= 5;

    public Transform aimPos;
    [HideInInspector] public Vector3 actualAimPos;
    [SerializeField] float aimSmoothSpeed = 30;
    [SerializeField] LayerMask aimMask;
    // Start is called before the first frame update
    void Start()
    {
        vCam = GetComponentInChildren<CinemachineVirtualCamera>();
        hipFOV = vCam.m_Lens.FieldOfView;
        animate = GetComponent<Animator>();
        
    }

    void LateUpdate()
    {
        camPos.localEulerAngles = new Vector3(y.Value, camPos.localEulerAngles.y, camPos.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, x.Value, transform.eulerAngles.z);
    }

    // Update is called once per frame
    void Update()
    {
        SwitchState(rifle);
        x.Update(Time.deltaTime);
        y.Update(Time.deltaTime);
        currentState.UpdateState(this);
        if (Input.GetKey(KeyCode.Mouse1))
        {
            //while (vCam.m_Lens.FieldOfView>50)
            //{
            vCam.m_Lens.FieldOfView = Mathf.Lerp(vCam.m_Lens.FieldOfView, adsFOV, FOVSmooth * Time.deltaTime);
            //}
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1)) {
            vCam.m_Lens.FieldOfView = 60;
        }
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, aimMask))
        {
            aimPos.position = Vector3.Lerp(aimPos.position, hit.point, aimSmoothSpeed * Time.deltaTime);
            actualAimPos = hit.point;
        }
    }

    public void SwitchState(AimBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

}
