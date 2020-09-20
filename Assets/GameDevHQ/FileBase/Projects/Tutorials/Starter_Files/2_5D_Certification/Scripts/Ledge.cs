using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ledge : MonoBehaviour
{
    #region Private Variables

    [SerializeField] private Vector3 _ledgePosition, _standPosition;

    #endregion

    #region public properties

    /// <summary>
    /// Gets the ledge hand position
    /// </summary>
    public Vector3 LedgePosition
    {
        get { return _ledgePosition; }
    }

    /// <summary>
    /// Gets the ledge stand position
    /// </summary>
    public Vector3 StandPosition
    {
        get { return _standPosition; }
    }

    #endregion

    #region Unity Functions

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ledge_Grab_Checker"))
        {
            var player = other.transform.parent.GetComponent<Player>();

            if (player != null)
            {
                player.GrabLedge(this);
            }
        }
    }

    #endregion
}
