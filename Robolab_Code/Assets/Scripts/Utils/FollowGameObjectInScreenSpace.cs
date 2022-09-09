namespace Robolab.Utils
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityStandardAssets.Characters.FirstPerson;
    using TMPro;    

    public class FollowGameObjectInScreenSpace : MonoBehaviour
    {
        [SerializeField] private RigidbodyFirstPersonController _rigidbodyFirstPersonController;

        public TextMeshProUGUI TextMesh;

        private Transform _target;

        // To adjust the position of the icon
        public Vector3 offset;

        private void Update()
        {
            // Giving limits to the icon so it sticks on the screen
            // Below calculations witht the assumption that the icon anchor point is in the middle
            // Minimum X position: half of the icon width
            float minX = TextMesh.GetPixelAdjustedRect().width / 2;
            // Maximum X position: screen width - half of the icon width
            float maxX = Screen.width - minX;

            // Minimum Y position: half of the height
            float minY = TextMesh.GetPixelAdjustedRect().height / 2;
            // Maximum Y position: screen height - half of the icon height
            float maxY = Screen.height - minY;

            // Temporary variable to store the converted position from 3D world point to 2D screen point
            Vector2 pos = Camera.main.WorldToScreenPoint(_target.position + offset);

            // Check if the target is behind us, to only show the icon once the target is in front
            if (Vector3.Dot((_target.position - _rigidbodyFirstPersonController.transform.position), _rigidbodyFirstPersonController.transform.forward) < 0)
            {
                // Check if the target is on the left side of the screen
                if (pos.x < Screen.width / 2)
                {
                    // Place it on the right (Since it's behind the player, it's the opposite)
                    pos.x = maxX;
                }
                else
                {
                    // Place it on the left side
                    pos.x = minX;
                }
            }

            // Limit the X and Y positions
            pos.x = Mathf.Clamp(pos.x, minX, maxX);
            pos.y = Mathf.Clamp(pos.y, minY, maxY);

            // Update the marker's position
            TextMesh.transform.position = pos;
        }

        public void UpdateTarget(Transform target)
        {
            _target = target;
        }
    }
}