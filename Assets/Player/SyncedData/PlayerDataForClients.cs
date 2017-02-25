// Player/SyncedData/PlayerDataForClients.cs

using UnityEngine;
using UnityEngine.Networking;

namespace Player.SyncedData {
    public class PlayerDataForClients : NetworkBehaviour {

        public delegate void ColourUpdated (GameObject player, Color newColour);
        public event ColourUpdated OnColourUpdated;
        public delegate void IsServerFlagUpdated (GameObject player, bool isServer);
        public event IsServerFlagUpdated OnIsServerFlagUpdated;

        [SyncVar(hook = "UpdateColour")]
        private Color colour;
        [SyncVar(hook = "UpdateIsServerFlag")]
        private bool isServerFlag;

        public override void OnStartClient()
        {
            // don't update for local player as handled by LocalPlayerOptionsManager
            // don't update for server as the server will know on Command call from local player
            if (!isLocalPlayer && !isServer) {
                UpdateColour(colour);
                UpdateIsServerFlag(isServerFlag);
            }
        }

        public Color GetColour ()
        {
            return colour;
        }
        
        [Client]
        public void SetColour (Color newColour)
        {
            CmdSetColour(newColour);
        }

        [Command]
        public void CmdSetColour (Color newColour)
        {
            colour = newColour;
        }

        [Client]
        public void UpdateColour (Color newColour)
        {
            colour = newColour;
            if (this.OnColourUpdated != null) {
                this.OnColourUpdated(gameObject, newColour);
            }
        }

        public bool GetIsServerFlag ()
        {
            return isServer;
        }

        [Client]
        public void SetIsServerFlag (bool newIsServer)
        {
            CmdSetIsServerFlag(newIsServer);
        }

        [Command]
        public void CmdSetIsServerFlag (bool newIsServer)
        {
            isServerFlag = newIsServer;
        }

        [Client]
        public void UpdateIsServerFlag (bool newIsServer)
        {
            isServerFlag = newIsServer;

            if (this.OnIsServerFlagUpdated != null) {
                this.OnIsServerFlagUpdated(gameObject, newIsServer);
            }
        }
    }
}