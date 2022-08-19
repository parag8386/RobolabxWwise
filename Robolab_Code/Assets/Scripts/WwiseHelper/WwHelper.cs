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

        // SFX
        public const string LIGHT_FLICKER = "Play_Lights_Flicker";
        public const string STOP_ALL = "Stop_All";
        public const string PLAY_POWERDOWN_01 = "Play_powerdown_01";
        public const string PLAY_POWERDOWN_02 = "Play_powerdown_02";
        public const string PLAY_ROBOT_BATTERING_RAM = "Play_robot_battering_ram";

        // Radio
        public const string RADIO_ON = "Play_radio_on";
        public const string RADIO_STATIC = "Play_static";
        public const string RADIO_OFF = "Play_radio_off";

        // Alarm
        public const string PLAY_ALARM_01_LOOP = "Play_alarm_01_loop";
        public const string PLAY_ALARM_02_LOOP = "Play_alarm_02_loop";
        public const string PLAY_ALARM_03_LOOP = "Play_alarm_03_loop";
        public const string STOP_ALARM_01_LOOP = "Stop_alarm_01_loop";
        public const string STOP_ALARM_02_LOOP = "Stop_alarm_02_loop";
        public const string STOP_ALARM_03_LOOP = "Stop_alarm_03_loop";
    }

    public struct WwiseVO
    {
        public string EventID { get; private set; }
        public GameObject GameObject { get; private set; }

        public WwiseVO(string eventID, GameObject gameObject)
        {
            EventID = eventID;
            GameObject = gameObject;
        }
    }

    public static class WwiseEventHelper
    {
        private static Dictionary<string, uint> s_playingIDs = new Dictionary<string, uint>();

        public static bool PostEventID(string eventID, GameObject gameObject, bool overrideIfAlreadyPlaying = false, AkCallbackManager.EventCallback eventCallback = null, object callbackObject = null)
        {
            if (eventCallback == null)
            {
                eventCallback = PostEventCallback;
            }

            if (callbackObject == null)
            {
                callbackObject = eventID;
            }

            uint playingId = s_playingIDs.ContainsKey(eventID) ? s_playingIDs[eventID] : 0;
            if (overrideIfAlreadyPlaying || playingId <= 0)
            {
                playingId = AkSoundEngine.PostEvent(eventID, gameObject, (uint)AkCallbackType.AK_EndOfEvent, eventCallback, callbackObject);
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

        public static void PlayVO(string vo_eventID, GameObject gameObject)
        {
            // Play radio on
            WwiseVO voObject = new WwiseVO(vo_eventID, gameObject);
            PostEventID(WwiseEventIDs.RADIO_ON, gameObject, eventCallback: VOStartEventCallback, callbackObject: voObject);
        }

        private static void PostEventCallback(object in_cookie, AkCallbackType in_type, AkCallbackInfo in_info)
        {
            string eventID = in_cookie.ToString();
            s_playingIDs[eventID] = 0;
        }

        private static void VOStartEventCallback(object in_cookie, AkCallbackType in_type, AkCallbackInfo in_info)
        {
            WwiseVO sentVOObject = (WwiseVO)in_cookie;
            string vo_eventID = sentVOObject.EventID;
            GameObject gameObject = sentVOObject.GameObject;

            // Stop radio on
            StopEventID(WwiseEventIDs.RADIO_ON);

            // Play VO with static
            PostEventID(WwiseEventIDs.RADIO_STATIC, gameObject);
            PostEventID(vo_eventID, gameObject, eventCallback: VOEndEventCallback, callbackObject: sentVOObject);
        }

        private static void VOEndEventCallback(object in_cookie, AkCallbackType in_type, AkCallbackInfo in_info)
        {
            WwiseVO sentVOObject = (WwiseVO)in_cookie;
            string vo_eventID = sentVOObject.EventID;
            GameObject gameObject = sentVOObject.GameObject;

            // Clear radio static and VO
            StopEventID(WwiseEventIDs.RADIO_STATIC);
            StopEventID(vo_eventID);

            // Play radio off
            PostEventID(WwiseEventIDs.RADIO_OFF, gameObject);
        }
    }
}