using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class LobbyLauncher : MonoBehaviourPunCallbacks
{
    #region Serizlizable Private Fields

    [SerializeField] private GameObject _connectionPanel;
    [SerializeField] private GameObject _loadingPanel;
    [SerializeField] private InputField _nameInput;

    #endregion

    #region Public Methods

    public void TryToConnect()
    {
        if (string.IsNullOrEmpty(_nameInput.text))
        {
            Debug.LogError("LobbyLauncher: Name Input is empty, please enter the name");
            return;
        }

        _connectionPanel.SetActive(false);
        _loadingPanel.SetActive(true);

        PhotonNetwork.NickName = _nameInput.text;

        Debug.Log("LobbyLauncher: Nickname set: " + _nameInput.text);

        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.ConnectUsingSettings();
    }

    #endregion

    #region Private Methods

    

    #endregion

    #region MonoBehaviourPunCallbacks Callbacks

    public override void OnConnectedToMaster()
    {
        Debug.Log("LobbyLauncher: successfully connected to Master");

        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("LobbyLauncher: Failed to join random room, trying to create own room");

        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 2 });
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError("LobbyLauncher: Failed to create room");
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("LobbyLauncher: Room successfully created");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("LobbyLauncher: Successfully joined to room");

        PhotonNetwork.LoadLevel("Lobby");
    }

    #endregion
}
