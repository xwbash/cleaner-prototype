using AnimationMachine;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Players.PlayerManager
{
    public class PrefabComponentManager : MonoBehaviour
    {
        public void Init(Transform ParentObject) // Neden starta yazildi? cunku awake de spawnlandiginda parent object null oluyor.
        {
            GetComponent<PlayerMovement>().PlayerPositionController = ParentObject;
            Vector3 randomVectorToSpawn = new Vector3(Random.Range(0f, 2f), Random.Range(0f, 2f), Random.Range(0f, 2f));
            var playerMovement = ParentObject.GetComponent<PlayerManager>().PlayerScriptable;
            GetComponent<NavMeshAgent>().Warp(ParentObject.position + randomVectorToSpawn);
            GetComponent<AnimationListeners>().AnimatorManagerScript = ParentObject.GetComponent<AnimatorManager>();
            GetComponent<SkinnedMeshRenderer>().material.color = playerMovement.PlayerColor;
        }
    }
}