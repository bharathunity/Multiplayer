using Fusion;
using Fusion.Photon.Realtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SharedManager : MonoBehaviour
{
    public enum Region
    {
        asia,  
        kr,
        us
    }

    public Region region;

    [field: SerializeField] private PhotonAppSettings   _photonAppSettings;
    [field: SerializeField] private NetworkRunner       _networkRunner;
    [field: SerializeField] private byte                _maxPlayerCount = 4;
    [field: SerializeField] private string              _sessionName = "s1";

    [SerializeField] private Button _sharedModeButton;

    public void Start()
    {
        _sharedModeButton.onClick.AddListener(StartSharedMode);
    }

    /// <summary>
    /// Start the shared mode.
    /// </summary>
    public async void StartSharedMode(){

        
        _networkRunner.name = "Shared Mode Runner";

        FusionAppSettings appSettings   = _photonAppSettings.AppSettings.GetCopy();
        appSettings.FixedRegion         = region.ToString().ToLower();

        StartGameArgs startGameArgs = new StartGameArgs(){
            GameMode = GameMode.Shared,
            CustomPhotonAppSettings = appSettings,
            PlayerCount = (int)_maxPlayerCount,
            SessionName = _sessionName,
        };

        StartGameResult startGame = await _networkRunner.StartGame(startGameArgs);
        if(startGame.Ok == true){
            Debug.Log($"Shared mode started...................................... \n");

        }else{
            Debug.LogError("Failed to start shared mode...............................");
        }
        
    }

}
