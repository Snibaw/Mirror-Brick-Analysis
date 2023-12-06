using UnityEngine;
using Mirror;
using UnityEngine.EventSystems;

public class PlayerController : NetworkBehaviour
{
    // Référence à la taupe actuellement ciblée
    private MoleHole currentMole;

    void Update()
    {
        // Vérifiez si le joueur a cliqué sur un objet et si cet objet a le composant MoleHole
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("Click on object");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Raycast hit " + hit.collider.gameObject.name);
                MoleHole moleHole = hit.collider.GetComponent<MoleHole>();

                if (moleHole != null)
                {
                    // Assurez-vous que le joueur a l'autorité sur la taupe
                    if (hasAuthority)
                    {
                        // Changez la visibilité de la taupe
                        moleHole.SetMoleVisibility(!moleHole.isMoleVisible);
                    }
                    else
                    {
                        // Demandez l'autorité au serveur
                        CmdRequestAuthority(moleHole.gameObject);
                    }
                }
            }
        }
    }

    [Command]
    void CmdRequestAuthority(GameObject moleObject)
    {
        // Demandez l'autorité sur la taupe au serveur
        NetworkIdentity networkIdentity = moleObject.GetComponent<NetworkIdentity>();
        if (networkIdentity != null)
        {
            networkIdentity.AssignClientAuthority(connectionToClient);
        }
    }
}