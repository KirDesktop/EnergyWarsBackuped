using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

public class PlayerReadyController : MonoBehaviourPunCallbacks, IPunObservable
{
    public static PlayerReadyController Singleton;

    #region SerializeField Private Fields

    [SerializeField] private float _timeToStartAfterAllReady;

    [SerializeField] private Text _info;
    [SerializeField] private Text _progress;

    #endregion

    #region Private Fields

    private bool _isReady = false;
    public PhotonView view;

    private bool _isTimer = false;
    private float _timerTimeLeft;

    //private PlayerServerListener[] _playersListeners;
    private int _ind;
    private string _lastText;

    #endregion

    #region Constant Private Fields

    private const string _info1 = "Press E\nto ready";
    private const string _info2 = "Waiting for\nother players...";

    #endregion

    #region Public Fields

    public int readyPlayers = 0;
    public int ready;

    #endregion

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        if(Singleton == null)
        {
            Singleton = this;
        }

        view = this.GetComponent<PhotonView>();
    }

    private void Update()
    {
        ready = readyPlayers;
        if (_isTimer)
        {
            _timerTimeLeft -= Time.deltaTime;
            _info.text = Mathf.Round(_timerTimeLeft).ToString() + " seconds\nleft";

            if(_timerTimeLeft <= 0)
            {
                PhotonNetwork.LoadLevel("GameArea");
                _isTimer = false;
                readyPlayers = -100;
            }
        }

        _UpdateProgressText();
        
    }

    #endregion

    #region Photon Class Reference
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            //stream.SendNext(readyPlayers);
            //stream.SendNext(_isTimer);
        }
        else
        {
            //readyPlayers = (int)stream.ReceiveNext();
            //_isTimer = (bool)stream.ReceiveNext();
            //_UpdateProgressText();
        }
    }

    #endregion

    #region MonoBehaviourPunCallbacks Callbacks

    #endregion

    #region Private Methods

    private void _UpdateProgressText()
    {
        if(_isTimer && readyPlayers != 2)
        {
            _isTimer = false;
            _info.text = _lastText;
        }

        if (_isTimer) return;
        _progress.text = readyPlayers.ToString() + "/2" + " players";
        if (readyPlayers == 2 && !_isTimer)
        {
            _isTimer = true;
            _timerTimeLeft = _timeToStartAfterAllReady;
        }
        else
        {
            _isTimer = false;
        }
    }

    #endregion

    #region Public Methods
    public void readyTextUpdate(bool isReady)
    {
        if (isReady)
        {
            _info.text = _info2;
            _lastText = _info2;
        }
        else
        {
            _info.text = _info1;
            _lastText = _info1;
        }
    }

    #endregion

    [PunRPC]
    public void plusPlayerReady(int count)
    {
        readyPlayers += count;
    }
}
