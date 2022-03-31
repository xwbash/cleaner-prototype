using System.Collections;
using System.Collections.Generic;
using AnimationMachine;
using DG.Tweening;
using Game_Manager;
using Market;
using EnemySpawnManager;
using SavingSystem;
using ScriptablePlayerObject;
using States.Enemy;
using States.Player;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Players.PlayerManager
{
    public class PlayerManager : MonoBehaviour
    {
        
        public bool IsFighting;
        public PlayerScriptable PlayerScriptable;
        public bool IsInSpawnPoint;
        public int SpawnSize=0;
        public int MyIndex = 0;
        public List<GameObject> TotalPlayer;

        [SerializeField] private Transform m_spawnObjectUnderThis;
        [SerializeField] private Image m_loadingBarChild;
        [SerializeField] private GameObject m_loadingBarParent;
        [SerializeField] private TMP_Text m_textScore;
        [SerializeField] private SaveManager m_saveManager;
        [SerializeField] private GameObject m_enemyFollowArrowObject;
        [SerializeField] private EnemyManagement m_enemyManagement;
        [SerializeField] private GameManager m_gameManager;
        [SerializeField] private RaidManager m_raidManager;
        [SerializeField] private EnemySpawner m_enemySpawner;
        
        private Sequence _sequence;
        private float _timerFight = 1f;
        private List<GameObject> _totalPlayer;
        private AnimatorManager _animatorManager;
        private Image _loadingBarChild;
        private float _minimumFightTimer = 0.1f;    

        private void Awake()
        {
            _animatorManager = GetComponent<AnimatorManager>();
            _loadingBarChild = m_loadingBarChild.GetComponent<Image>();
        }
        private void Start()
        {
            if (SpawnSize == 0)
            {
                gameObject.SetActive(false);
                m_enemySpawner.SpawnTimer(this);
            }
            if (PlayerScriptable.PlayerTypes == PlayerScriptable.PlayerType.Enemy)
            {
                m_enemyManagement.EnemyList.Add(transform);
            }
            _totalPlayer = TotalPlayer;
            _totalPlayer.Clear();
            SpawnPlayer(SpawnSize, PlayerScriptable.PlayerColor, PlayerScriptable.PlayerPrefab);
            
        }
        public void SpawnPlayer(int spawnNumber, Color playerColor, GameObject playerObj)
        {

            for (int i = 0; i < spawnNumber; i++)
            {
                GameObject newGameObj = Instantiate(playerObj, m_spawnObjectUnderThis, true);
                newGameObj.GetComponent<PrefabComponentManager>().Init(transform);
                _totalPlayer.Add(newGameObj);
            }
            TextUpdate();
            m_saveManager.CollectPlayerToData();
        }
        public void StartLoadingBar()
        {
            if (!IsInSpawnPoint)
            {
                ResetBar();
                return;
            }
            _sequence = DOTween.Sequence();
            _sequence.Append(m_loadingBarChild.DOFillAmount(1, PlayerMarketData.Instance.ReloadSpawnSpeed)
                .SetEase(Ease.Linear));
            _sequence.AppendCallback(() =>
            {
                SpawnPlayer(1, PlayerScriptable.PlayerColor, PlayerScriptable.PlayerPrefab);
                SpawnSize += 1;
                ResetBar();
                StartLoadingBar();
            });
            m_loadingBarParent.SetActive(true);
        }
        public void ResetBar()
        {
            m_loadingBarParent.SetActive(false);
            _loadingBarChild.fillAmount = 0;
            _sequence.Kill();
        }
        public void StartFighting()
        {
            _timerFight = 1f;
            if (StateMachine.Instance.CurrentState != StateMachine.Instance.DyingState)
            {
                IsFighting = true;
                StartCoroutine(_animatorManager.SetFightingState(IsFighting));
                DelayedInvokeDecrease();
            }
        }
        public void StopFighting()
        {
            StartCoroutine(_animatorManager.SetFightingState(false));
        }
        

        public void KillPlayer()
        {
            m_enemyFollowArrowObject.SetActive(true);
            SpawnSize = 0;
            m_enemyManagement.EnemyList.Remove(transform);
            StateMachine.Instance.SetStates(StateMachine.Instance.IdleState);
            Destroy(_totalPlayer[0]);
            m_raidManager.IsCurrentlyRaiding = false;
            m_raidManager.RaidPlayer();
            GetComponent<PlayerRaider>().IsRaiding = false;
            m_saveManager.ReIndexObjects();
            m_saveManager.CollectPlayerToData();
            var stateMachineEnemy = GetComponent<StateMachineEnemy>();
            stateMachineEnemy.SetCurrentStateEnemy(stateMachineEnemy.IdleStateEnemy);
            gameObject.SetActive(false);
            _totalPlayer.Clear();
            m_enemySpawner.SpawnTimer(this);
        }
        private void DelayedInvokeDecrease()
        {
            StartCoroutine(WaitAndAttack(_timerFight));
        }
        
        private void DecreasePlayer()
        {
            if (!IsFighting || m_gameManager.IsDied)
            {
                return;
            }
            if (_totalPlayer.Count <= 1)
            {
                if (PlayerScriptable.PlayerTypes == PlayerScriptable.PlayerType.Enemy)
                {
                    var stateMachineEnemy = GetComponent<StateMachineEnemy>();
                    if (stateMachineEnemy)
                    {
                        m_saveManager.CollectPlayerToData();
                        stateMachineEnemy.SetCurrentStateEnemy(stateMachineEnemy.DyingStateEnemy);
                        PlayerMarketData.Instance.AddGold(100);
                        return;    
                    }
                }
                else
                {
                    SpawnSize = 0;
                    m_saveManager.CollectPlayerToData();
                    StateMachine.Instance.SetStates(StateMachine.Instance.DyingState);
                    return;
                }
            }
            SpawnSize -= 1;
            Destroy(_totalPlayer[_totalPlayer.Count-1]);
            _totalPlayer.RemoveAt(_totalPlayer.Count-1);
            TextUpdate();
            _timerFight -= 0.1f;
            Mathf.Clamp(_timerFight, _minimumFightTimer, _timerFight);
            SpawnSize = _totalPlayer.Count;
            m_saveManager.CollectPlayerToData();
            DelayedInvokeDecrease();
        }
        private void TextUpdate()
        {
            m_textScore.text = _totalPlayer.Count.ToString();
        }
        
        private IEnumerator WaitAndAttack(float second)
        {
            yield return new WaitForSeconds(second);
            DecreasePlayer();
        }

    }
}