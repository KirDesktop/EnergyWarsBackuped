using UnityEngine;
using Photon.Pun;

public class LobbyManager : MonoBehaviour
{
    #region SerializeField Private Fields

    [SerializeField] private GameObject _player;
    [SerializeField] private Transform _spawnPoint;

    #endregion

    #region Monobehaviour Callbacks

    private void Awake()
    {
        PhotonNetwork.Instantiate(_player.name, _spawnPoint.position, Quaternion.identity);
    }

    #endregion
}
