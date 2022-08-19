namespace Robolab.Story
{
    using UnityStandardAssets.Characters.FirstPerson;
    using UnityEngine;

    public class StoryGameObjectReferences : MonoBehaviour
    {
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

        [Header("Lights")]
        public Light[] LightObjects = default;

        [Header("Aniamtors")]
        public Animator RobotArmAnimator = default;
        public Animator BatteringRamAnimator = default;
    }
}