using UnityEngine;

namespace Navigation_Arrow
{
    public class StaticRotation : MonoBehaviour
    {
        [SerializeField] private Vector3 m_vector3Rotation = new Vector3(18, 0, 0);
        private Quaternion _rotation;
        private void Start()
        {
            _rotation = Quaternion.Euler(m_vector3Rotation);
        }

        void Update()
        {
            transform.rotation = _rotation;
        }
    }
}
