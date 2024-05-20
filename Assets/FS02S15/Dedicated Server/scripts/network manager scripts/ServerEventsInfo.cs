using Fusion;
using Fusion.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Game15Server
{
    public class ServerEventsInfo : SimulationBehaviour, INetworkRunnerCallbacks
    {

        #region Private fields
        private const int TIMEOUT = 5;
        private float TIME_COUNTER = TIMEOUT;

        
        #endregion

        #region Monobehaviour callbacks
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            TIME_COUNTER -= Time.deltaTime;

            if (TIME_COUNTER < 0)
            {
                TIME_COUNTER = TIMEOUT;

                if (Runner != null && Runner.IsServer)
                {
                    var msg = $"Total Players: {Runner.ActivePlayers.Count()}";

                    foreach (var player in Runner.ActivePlayers)
                    {
                        msg += $"\n{player}: {Runner.GetPlayerConnectionType(player)}";
                        var ping = Runner.GetPlayerRtt(player);
                        var result=TimeSpan.FromSeconds(ping);
                        msg += $"\n{player}: Ping {result.Milliseconds}";
                        
                    }
                      
                    Debug.Log(msg);
                }
            }
        }
        #endregion


        #region INetwork Runner callbacks
        public void OnConnectedToServer(NetworkRunner runner)
        {
            //throw new NotImplementedException();
        }

        public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
        {
            //throw new NotImplementedException();
        }

        public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
        {
            //throw new NotImplementedException();
        }

        public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
        {
            //throw new NotImplementedException();
        }

        public void OnDisconnectedFromServer(NetworkRunner runner)
        {
            //throw new NotImplementedException();
        }

        public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
        {
            //throw new NotImplementedException();
        }

        public void OnInput(NetworkRunner runner, NetworkInput input)
        {
            //throw new NotImplementedException();
        }

        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
        {
            //throw new NotImplementedException();
        }

        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
            //throw new NotImplementedException();
        }

        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
        {
            //throw new NotImplementedException();
        }

        public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
        {
            //throw new NotImplementedException();
        }

        public void OnSceneLoadDone(NetworkRunner runner)
        {
            //throw new NotImplementedException();
        }

        public void OnSceneLoadStart(NetworkRunner runner)
        {
            //throw new NotImplementedException();
        }

        public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
        {
            //throw new NotImplementedException();
        }

        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
        {
            //throw new NotImplementedException();
        }

        public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
        {
            //throw new NotImplementedException();
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

        /*public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
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
        }*/
        #endregion
    }
}


