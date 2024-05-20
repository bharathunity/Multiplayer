using Cinemachine;
using System;
using UnityEngine;

public delegate void CameraDelegate();

public class CameraController : MonoBehaviour
{
    public enum CameraEnum
    {
        Tpp,
        ZipLine,
        Helicopter
    }


    public event CameraDelegate     CinemachinePovOnChanged;
    public static CameraController  Instance;


    #region Serialize private fields
    [field: SerializeField] public Transform                MainCamera { get; private set; }
    [field: SerializeField] public CinemachineVirtualCamera VirtualCamera { get; private set; }
    #endregion


    #region Private fields
    private bool            _followerAssigned = false;
    private CinemachinePOV  cinemachinePOV;
    #endregion

    [field: SerializeField] public Vector2 RotationValue { get; private set; }

    private float GetAxis(string axisName)
    {
        return axisName switch
        {
            "X" => Input.GetAxis("Mouse X"),
            "Y" => Input.GetAxis("Mouse Y"), 
            _ => 0f
        };
    }


    #region Monobehaviour callbacks
    void Awake()
    {
        Instance                = this;
        CinemachinePovOnChanged += UpdateAimPovProperties;
        cinemachinePOV          = VirtualCamera.GetCinemachineComponent<CinemachinePOV>();
    }

    void LateUpdate()
    {
        RotationValue = new Vector2((float)Math.Round(this.MainCamera.transform.localEulerAngles.x, 1),
                                    (float)Math.Round(this.MainCamera.transform.localEulerAngles.y, 1));


        if(VirtualCamera.Follow != null && !_followerAssigned)
        {
            CinemachinePovOnChanged?.Invoke();
            CinemachinePovOnChanged -= UpdateAimPovProperties;
            _followerAssigned       = true;
        }
    
    }

    /// <summary>
    /// Updae the POV properties in the cinemachine
    /// </summary>
    public void UpdateAimPovProperties()
    {
        cinemachinePOV.m_VerticalAxis.m_InputAxisName   = "Mouse Y";
        cinemachinePOV.m_HorizontalAxis.m_InputAxisName = "Mouse X";
    }
    #endregion

    

    


}
