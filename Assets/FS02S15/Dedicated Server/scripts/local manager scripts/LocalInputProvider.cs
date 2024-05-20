using System;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using UnityEngine;

public class LocalInputProvider : SimulationBehaviour, INetworkRunnerCallbacks
{

    InputProvider inputProvider;
    private Vector2 _leftJoystick;

    private float _cameraYrotation;

    private Camera _camera;


   #region  Monobehaviour callbacks
    void Awake(){
        inputProvider = new InputProvider();
    }

    void OnEnable(){
        inputProvider.Enable();
    }

    void Start()
    {
        
    }

    public void Update(){
       
        _leftJoystick    = inputProvider.Player.Move.ReadValue<Vector2>();
        // _cameraYrotation = Input.GetAxis("Mouse X");
       
        
    }

    void OnDisable(){
        inputProvider.Disable();
    }

    #endregion

    #region Private methods
    void GetLocalComponents(){
        
        _camera = GameObject.FindObjectOfType<Camera>();
        // Debug.Log($"Camera {_camera}");
    }
    #endregion

    #region INetwork runner callbacks (InUSe)

    public override void Render()
    {
        GetLocalComponents();
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        InputStruct inputStruct     = new InputStruct();
        inputStruct.LeftJoystick    = this._leftJoystick;
        if(_camera != null){
            
            inputStruct.CameraYrotation =  _camera.transform.localEulerAngles.y; //Input.GetAxis("Mouse X");
            // Debug.Log(inputStruct.CameraYrotation);
        }
        input.Set(inputStruct);
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {

    }
    #endregion


    public void OnConnectedToServer(NetworkRunner runner)
    {
       
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
    
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
        
    }

    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
    {
       
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {

    }


    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
       
    }

    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
     
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
    
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        
    }

    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
    {
     
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
    {
       
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {

    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {

    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
   
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {

    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {

    }


}
