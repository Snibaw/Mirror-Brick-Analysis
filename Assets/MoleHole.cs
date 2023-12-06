using System;
using UnityEngine;
using Mirror;
public class MoleHole : NetworkBehaviour
{
    [SerializeField] private Animator moleAnimator;
    private bool isMoleVisible = false;

    private void Start()
    {
        moleAnimator = GetComponentInChildren<Animator>();
        if (moleAnimator == null)
        {
            Debug.LogError("Animator component not found on MoleHole");
        }
    }

    public void ClickOnMoleHole()
    {
        if (isServer)
        {
            // Only the server player is allowed to click the mole hole
            Debug.Log("Server player clicked the mole hole");
            ToggleMoleVisibility();
        }
    }
    void ToggleMoleVisibility()
    {
        // Toggle the mole's visibility
        isMoleVisible = !isMoleVisible;

        // Inform clients about the mole visibility change
        RpcToggleMoleVisibility(isMoleVisible);
    }
    [ClientRpc]
    void RpcToggleMoleVisibility(bool isVisible)
    {
        // This function will be called on all clients
        // You can handle client-side logic based on the mole's visibility
        if (moleAnimator != null)
        {
            // Trigger the "ShowMole" animation
            moleAnimator.SetBool("ShowMole", isVisible);
        }
    }
}
