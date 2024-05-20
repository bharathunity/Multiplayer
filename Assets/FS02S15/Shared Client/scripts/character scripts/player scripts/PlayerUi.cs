using Fusion;
using System;
using TMPro;
using UnityEngine;

public class PlayerUi : NetworkBehaviour
{

    public delegate void PlayerUiDelegate(string message, bool self);

    public static PlayerUiDelegate OnPlayerMessage;
    public static PlayerUiDelegate OnPlayerPing;


    [SerializeField] TMP_Text _playerId;

    [SerializeField] NetworkObject networkObject;

    private float _startTime, _endTime;

    #region Monobehaviour callbacks
    private void OnEnable()
    {
        OnPlayerMessage     += NotifyOnMessageReceived;
        OnPlayerPing        += UpdatePingFromClientToUi;
        _startTime = 0;

        UiManager.Instance.ToggleTimeSpendText(true);

    }

    private void OnDisable()
    {
        OnPlayerMessage     -= NotifyOnMessageReceived;
        OnPlayerPing        -= UpdatePingFromClientToUi;
        _endTime = Time.time;
        UiManager.Instance.ToggleTimeSpendText(false);

    }

    void Start()
    {

    }

    private void Update()
    {
        _startTime += Time.deltaTime;
        UiManager.Instance.UpdateTimeSpend(MathF.Round(_startTime, 0));
    }
    #endregion

    #region Network callbacks
    public override void Spawned()
    {
        _playerId.text = networkObject.Id.ToString();
    }

    public override void Render()
    {
        if (!Object.HasInputAuthority && !Object.HasStateAuthority)
            return;
        
        _playerId.transform.LookAt(CameraController.Instance.MainCamera);

        

    }
    #endregion

    #region Private methods
    
    void NotifyOnMessageReceived(string message, bool self)
    {
        if (self)
        {
            UiManager.Instance.UpdatePlayerState(message);
            Debug.Log(message);
        }
    }

    void UpdatePingFromClientToUi(string message, bool self)
    {
        if(self)
        {
            UiManager.Instance.UpdatePing(message);
        }
    }
    

    #endregion


}
