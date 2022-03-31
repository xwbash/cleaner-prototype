using UnityEngine;

namespace Navigation_Arrow
{
    public class FollowPlayer : MonoBehaviour
    {
        [SerializeField] private GameObject m_playerObject;

        [SerializeField] private Vector3 m_offset;
        void LateUpdate()
        {
            gameObject.transform.position = m_playerObject.transform.position + m_offset;
        }
    }
}
