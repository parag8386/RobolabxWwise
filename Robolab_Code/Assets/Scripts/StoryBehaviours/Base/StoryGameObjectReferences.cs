namespace Robolab.Story
{
    using System;
    using UnityStandardAssets.Characters.FirstPerson;
    using UnityEngine;

    public class StoryGameObjectReferences : MonoBehaviour
    {
        public const string PLAYER_TAG = "Player";

        [Header("Ambience Objects")]
        public GameObject GenericAmbience = default;
        public GameObject StaticRobotLab = default;
        public GameObject RetroTelevision = default;
        public GameObject Fan = default;
        public GameObject HoverPad = default;

        [Header("NPC")]
        public GameObject NPC = default;

        [Header("Player")]
        public RigidbodyFirstPersonController RigidbodyFirstPersonController;

        [Header("Terminals")]
        public Terminal[] Terminals;

        [Header("Lights")]
        public Light[] LightObjects = default;
        public GameObject LightAudioEmitter1 = default;
        public GameObject LightAudioEmitter2 = default;
        public GameObject LightAudioEmitter3 = default;

        [Header("Animators")]
        public Animator RobotArmAnimator = default;
        public Animator BatteringRamAnimator = default;
        public Animation LegacyFanAnimation = default;

        [Header("Particles")]
        public ParticleSystem[] FanParticles = default;

        private Color[] _defaultLightColors = default;

        private void Start()
        {
            _defaultLightColors = new Color[LightObjects.Length];
            for (int i = 0; i < _defaultLightColors.Length; i++)
            {
                _defaultLightColors[i] = LightObjects[i].color;
            }
        }

        public void RestoreLightColorsToDefault()
        {
            for (int i = 0; i < _defaultLightColors.Length; i++)
            {
                LightObjects[i].color = _defaultLightColors[i];
            }
        }
    }
}