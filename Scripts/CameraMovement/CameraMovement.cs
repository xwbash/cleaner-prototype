using UnityEngine;

namespace CameraMovement
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private Transform m_followingObject;
        [SerializeField] private Vector3 m_offsetCameraFromObject;
        [SerializeField, Range(0f, 1f)] private float m_smoothnessCameraPosition;
        private void LateUpdate()//kk
        {
            transform.position = Vector3.Lerp(transform.position,(m_followingObject.position + m_offsetCameraFromObject), m_smoothnessCameraPosition);
        }
    }
}