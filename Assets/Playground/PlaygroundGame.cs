using TLH.Gameplay.Entities;
using TLH.Input;
using UnityEngine;

namespace TLH.Playground
{
    public class PlaygroundGame : MonoBehaviour
    {
        [SerializeField] private GameObject playerPrefab;

        private void Start()
        {
            Player player = Instantiate(playerPrefab, Vector2.zero, Quaternion.identity).GetComponent<Player>();
            player.gameObject.name = "Player";
            player.Init(new TestInputReader());
        }
    }
}