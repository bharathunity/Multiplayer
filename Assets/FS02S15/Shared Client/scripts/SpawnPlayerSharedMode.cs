using Fusion;
using System;
using UnityEngine;

public class SpawnPlayerSharedMode : SimulationBehaviour, IPlayerJoined, IPlayerLeft
{
    #region Serialize private fields
    

    [SerializeField] private NetworkRunner _networkRunner;

    [field: SerializeField] public NetworkObject    Player { get; private set; }
    #endregion

    void Start(){
        _networkRunner.SetVisible(true) ;
    }

    private void Update()
    {
        
    }

    #region Fusion callbacks
    /// <summary>
    /// OnClick() event on the SHARED button.
    /// </summary>
    /// <param name="player"></param>
    public void PlayerJoined(PlayerRef player)  
    {
        if (player == Runner.LocalPlayer)
        {
            NetworkObject networkObject = Runner.Spawn(Player, Vector3.zero, Quaternion.identity, inputAuthority: player);
            
            UiManager.Instance.TogglePlayerCanvas(true);

            UiManager.Instance.ToggleSharedModeUi(false);
        }
    }

    /// <summary>
    /// OnClick() event on the shut down button.
    /// </summary>
    /// <param name="player"></param>
    public void PlayerLeft(PlayerRef player)
    {
        Runner.Despawn(Player);
    }
    #endregion

    #region Public methods
    public async void ShutDown()
    {
        // await Runner.Shutdown(destroyGameObject: false, ShutdownReason.Ok);
#if UNITY_EDITOR

#elif UNITY_STANDALONE
        Application.Quit();
#endif
        // Debug.Log($"{nameof(ShutDown)} \t Runner is shut down {Runner.IsShutdown}");
    }
#endregion
}
 