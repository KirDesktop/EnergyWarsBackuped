using UnityEngine;
using Photon.Pun;

public class PlayerServerInit : MonoBehaviour
{
    #region SerializeField Private Fields

    [SerializeField] PhotonView _view;
    [SerializeField] GameObject[] _toDelete;
    [SerializeField] GameObject _toFree;

    #endregion

    #region Monobehaviour Callbacks

    private void Start()
    {
        if (_view.IsMine)
        {
            Destroy(this.gameObject);
            return;
        }

        _toFree.transform.parent = _view.gameObject.transform;

        for (int i = 0; i < _toDelete.Length; i++)
        {
            Destroy(_toDelete[i]);
        }

        Destroy(this.gameObject);
    }

    #endregion
}
