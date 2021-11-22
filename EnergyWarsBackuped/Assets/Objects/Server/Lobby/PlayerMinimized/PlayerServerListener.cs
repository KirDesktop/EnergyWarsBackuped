using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerServerListener : MonoBehaviourPunCallbacks, IPunObservable
{
    #region SerializeField Private Fields

    [SerializeField] private Transform _playerTop;

    #endregion

    #region Private Fields

    private PhotonView _view;

    private Vector3 _playerPos;
    private Vector3 _playerRot;

    #endregion

    #region Public Fields

    public bool isReady = false;

    #endregion

    #region Monobehaviour Callbacks

    private void Awake()
    {
        _view = this.GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (!_view.IsMine) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            isReady = !isReady;

            if (isReady)
            {
                PlayerReadyController.Singleton.view.RPC("plusPlayerReady", RpcTarget.All, 1);
            }
            else
            {
                PlayerReadyController.Singleton.view.RPC("plusPlayerReady", RpcTarget.All, -1);
            }

            PlayerReadyController.Singleton.readyTextUpdate(isReady);
        }
    }

    #endregion

    #region IPunObservable Methods
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting && _view.IsMine)
        {
            _playerPos = _playerTop.transform.position;
            _playerRot = _playerTop.transform.eulerAngles;
            stream.SendNext(_playerPos);
            stream.SendNext(_playerRot);

            stream.SendNext(isReady);
        }
        else if(!_view.IsMine)
        {
            _playerPos = (Vector3)stream.ReceiveNext();
            _playerRot = (Vector3)stream.ReceiveNext();

            _playerTop.transform.position = _playerPos;
            _playerTop.transform.rotation = Quaternion.Euler(_playerRot);

            isReady = (bool)stream.ReceiveNext();
        }
    }

    #endregion

    #region MonoBehaviourPunCallbacks Callbacks

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("PlayerReadyController: Player " + otherPlayer.NickName + " left the room, Master Client: " + PhotonNetwork.IsMasterClient);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("PlayerReadyController: Player " + newPlayer.NickName + " entered in room");
    }

    #endregion
}
