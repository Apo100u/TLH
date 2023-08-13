using TLH.Gameplay.Entities;
using TLH.Gameplay.ObjectPools;
using TLH.Input;
using UnityEngine;

namespace TLH.Playground
{
    public class PlaygroundGame : MonoBehaviour
    {
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private Pools pools;

        private void Start()
        {
            Player player = Instantiate(playerPrefab, Vector2.zero, Quaternion.identity).GetComponent<Player>();
            player.gameObject.name = "Player";
            player.Init(new TestInputReader(), mainCamera, pools);
        }
    }
}