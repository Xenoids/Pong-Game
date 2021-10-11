using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideWall : MonoBehaviour
{

    // Pemain akan bertambah skor apabila nyentuh dinding
    public PlayerControl p;
    [SerializeField]
    private GameManager gameManager;

    // Ontrigger method SideWall
    // akan dipanggil ketika objek lain bercollider 
    // (bola) bersentuhan dengan dinding (left n r wall)

    void OnTriggerEnter2D(Collider2D colliderlain)
    {
        // jika objeknya bernama "Ball";
        if(colliderlain.name == "Ball")
        {
            // naikin skor player
            p.naikinskor();

            // jika blm max skornya
            if(p.hasil < gameManager.skormax)
            {
                // restart game setelah kena dinding
                colliderlain.gameObject.SendMessage("gamerestart",2.0f,SendMessageOptions.RequireReceiver);
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
