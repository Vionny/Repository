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

    // Start is called before the first frame update
    void Start()
    {
        vCam = GetComponentInChildren<CinemachineVirtualCamera>();
        hipFOV = vCam.m_Lens.FieldOfView;
        animate = GetComponentInChildren<Animator>();
        SwitchState(rifle);
    }

    void LateUpdate()
    {
        camPos.localEulerAngles = new Vector3(y.Value, camPos.localEulerAngles.y, camPos.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, x.Value, transform.eulerAngles.z);
    }

    // Update is called once per frame
    void Update()
    {
        x.Update(Time.deltaTime);
        y.Update(Time.deltaTime);
        currentState.Update(this);
        if (Input.GetKey(KeyCode.Mouse1))
        {
            while (vCam.m_Lens.FieldOfView>50)
            {
                vCam.m_Lens.FieldOfView = Mathf.Lerp(vCam.m_Lens.FieldOfView, currentFOV, FOVSmooth * Time.deltaTime);
            }
        }
        else vCam.m_Lens.FieldOfView = 60;
    }

    public void SwitchState(AimBaseState state)
    {
        currentState = state;
        currentState.Enter(this);
    }

}
