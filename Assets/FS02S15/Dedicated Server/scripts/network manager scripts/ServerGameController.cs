using Fusion;
using Fusion.Sockets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Game15Server
{
    /// <summary>
    /// Server game controller for instantiating the player by the server.
    /// </summary>
    public class ServerGameController : SimulationBehaviour, INetworkRunnerCallbacks
    {
        
        /// <summary>
        /// Player 
        /// </summary>
        [SerializeField] private NetworkObject _player;
        /// <summary>
        /// Spawner scriptable object
        /// </summary>
        // [SerializeField] private SpawnerScriptable _spawnerScriptable;
        

        // Player map dictionary.
        private readonly Dictionary<PlayerRef, NetworkObject> _playerMap = new Dictionary<PlayerRef, NetworkObject>();

        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {

            var x = 0;//-10.47f; // -280f; // 115f;
            var y = 0;//20f; // 30f; // 350f;
            var z = 0;//-15f; // 1102f; // 1541f;
            

            Quaternion rotation = Quaternion.Euler(0, 180f, 0); // -90f

            NetworkObject Player = runner.Spawn(_player, new Vector3(x, y, z), rotation, inputAuthority: player);
            runner.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            
            _playerMap[player] = Player;
            Debug.Log($"{nameof(OnPlayerJoined)} Player {Player}");

        }

        #region Monobehaviour callbacks
        // Start is called before the first frame update
        void Start()
        {

        }

        #endregion

        #region INetwork Runner callbacks
        public void OnConnectedToServer(NetworkRunner runner)
        {
            Debug.Log($"");
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

        public void OnDisconnectedFromServer(NetworkRunner runner)
        {
            
        }

        public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
        {
            
        }

        public void OnInput(NetworkRunner runner, NetworkInput input)
        {
            
        }

        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
        {
         
        }



        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
        {
            if (_playerMap.TryGetValue(player, out var character))
            {
                // Despawn Player
                runner.Despawn(character);

                // Remove player from mapping
                _playerMap.Remove(player);

                Log.Info($"Despawn for Player: {player}");
            }

            if (_playerMap.Count == 0)
            {
                Log.Info("Last player left, shutdown...");
                // Shutdown Server after the last player leaves
                // runner.Shutdown();
            }
          
        }

        public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
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

        public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
        {
            throw new NotImplementedException();
        }

        public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
        {
            throw new NotImplementedException();
        }

        public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
        {
            throw new NotImplementedException();
        }

        public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
        {
            throw new NotImplementedException();
        }

        public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}


