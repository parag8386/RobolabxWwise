namespace Robolab.Wwise.Events
{
    using System.Collections.Generic;
    using UnityEngine;

    public static class WwiseEventIDs
    {
        // Music
        public const string PLAY_MUSIC_LOOP = "Play_Music_Loop";
        public const string STOP_MUSIC_LOOP = "Stop_Music_Loop";

        // Ambience
        public const string PLAY_AMBIENCE_LOOP = "Play_Ambience_loop";
        public const string PLAY_INDOOR_LAB_LOOP = "Play_Indoorlab_loop";
        public const string PLAY_FAN_BLADE_LOOP = "Play_fan_blade_loop";
        public const string PLAY_FAN_LOOP = "Play_fan_loop";
        public const string PLAY_GLASSPAD_LOOP = "Play_glasspad_loop";
    }

    public static class WwiseEventHelper
    {
        private static Dictionary<string, uint> s_playingIDs = new Dictionary<string, uint>();

        public static bool PostEventID(string eventID, GameObject gameObject, bool overrideIfAlreadyPlaying = false)
        {
            uint playingId = s_playingIDs.ContainsKey(eventID) ? s_playingIDs[eventID] : 0;
            if (overrideIfAlreadyPlaying || playingId <= 0)
            {
                playingId = AkSoundEngine.PostEvent(eventID, gameObject);
                s_playingIDs[eventID] = playingId;
                return true;
            }
            return false;
        }

        public static void StopEventID(string eventID)
        {
            uint playingId = s_playingIDs.ContainsKey(eventID) ? s_playingIDs[eventID] : 0;
            if (playingId > 0)
            {
                AkSoundEngine.StopPlayingID(playingId);
                s_playingIDs[eventID] = 0;
            }
        }
    }
}