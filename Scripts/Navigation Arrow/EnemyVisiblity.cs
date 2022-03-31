using UnityEngine;

namespace Navigation_Arrow
{
    public class EnemyVisiblity : MonoBehaviour
    {
        public GameObject EnemyToFollow = null;
        [SerializeField] private float m_cameraRayArea;
        [SerializeField] private GameObject m_gameObjectArrow;
        [SerializeField] private GameObject m_arrowText;
        private Camera _cameraComponent;
        
        private void Awake()
        {
            _cameraComponent = GetComponent<Camera>(); 
        }

        private bool IsVisible()
        {
            var planes = GeometryUtility.CalculateFrustumPlanes(_cameraComponent);
            if (EnemyToFollow)
            {
                var point = EnemyToFollow.transform.position;

                foreach (var plane in planes)
                {
                    if (plane.GetDistanceToPoint(point) < m_cameraRayArea)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        
        private void Update()
        {
            if (IsVisible())
            {
                m_arrowText.SetActive(false);
                m_gameObjectArrow.SetActive(false);

            }
            else
            {
                m_arrowText.SetActive(true);
                m_gameObjectArrow.SetActive(true);

            }
        }
    }
}
