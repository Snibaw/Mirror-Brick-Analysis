using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class MoleHole : NetworkBehaviour
{
    [SerializeField] private Animator moleAnimator;

    [SyncVar(hook = nameof(OnMoleVisibilityChanged))]
    public bool isMoleVisible = false;

    private void Start()
    {
        moleAnimator = GetComponentInChildren<Animator>();
        if (moleAnimator == null)
        {
            Debug.LogError("Animator component not found on MoleHole");
        }

        // Ensure initial state is synced to clients, only on the server
        if (isServer)
        {
            SetMoleVisibility(isMoleVisible);
        }
    }

    public void SetMoleVisibility(bool isVisible)
    {
        isMoleVisible = isVisible;

        if (moleAnimator != null)
        {
            moleAnimator.SetBool("ShowMole", isVisible);
        }
    }

    // This method is called whenever the SyncVar changes
    void OnMoleVisibilityChanged(bool oldVisibility, bool newVisibility)
    {
        SetMoleVisibility(newVisibility);
    }
}