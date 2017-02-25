// Player/SyncedData/LocalPlayerDataStore.cs

using UnityEngine;

namespace Player.SyncedData {
    public class LocalPlayerDataStore {

        private static LocalPlayerDataStore instance;

        public Color playerColour;
        public bool isServer = false;

        private LocalPlayerDataStore () { }

        public static LocalPlayerDataStore GetInstance ()
        {
            if (instance == null) {
                instance = new LocalPlayerDataStore();
            }

            return instance;
        }
    }
}