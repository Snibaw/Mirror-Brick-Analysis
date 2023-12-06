using UnityEngine;
using Mirror;

public class Mole : NetworkBehaviour
{
    // ...

    public void ClickOnMole()
    {
        if (isServer)
        {
            // Only the server player is allowed to click the mole
            Debug.Log("Server player clicked the mole");

            // Add your mole interaction logic here

            // Destroy the mole for everyone
            NetworkServer.Destroy(gameObject);

            // Inform clients that the mole was clicked and should be destroyed
            RpcMoleClicked();
        }
    }

    [ClientRpc]
    void RpcMoleClicked()
    {
        // This function will be called on all clients
        // Add any client-side logic you want to perform when the mole is clicked
    }
}