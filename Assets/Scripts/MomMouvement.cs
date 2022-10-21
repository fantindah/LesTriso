using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomMouvement : MonoBehaviour
{
    [SerializeField] private GameObject momDialogue;
    [SerializeField] private GameObject sonDialogue;
    [SerializeField] private GameObject inputBlock;
    [SerializeField] private GameObject LivingRoom;
    private bool dialoguePlaying;
    public int security=0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.GetComponent<EnemyMovement>().activeMovement == 5 && !dialoguePlaying && gameObject.GetComponent<EnemyMovement>().isBlocked && security ==0 && transform.position.x >-4)
        {
            Debug.Log(gameObject.GetComponent<EnemyMovement>().activeMovement);
            StartCoroutine(ActivateDialogue(momDialogue, 2, sonDialogue, 1));
        }
        else if (gameObject.GetComponent<EnemyMovement>().activeMovement == 5 && gameObject.GetComponent<EnemyMovement>().isBlocked && security == 0 && transform.position.x < -4)
        {
            gameObject.GetComponent<EnemyMovement>().goBack = true;
        }
        if (gameObject.GetComponent<EnemyMovement>().activeMovement == 5 && security == 1)
            security++;


        if (dialoguePlaying)
            inputBlock.SetActive(true);
        else
            inputBlock.SetActive(false);

    }

    public void StartMovement(string roomName)
    {

        security = 0;
        if (roomName == "LivingRoom")
        {
            gameObject.GetComponent<EnemyMovement>().unblocked = true;
        }
    }
    private IEnumerator ActivateDialogue(GameObject dialogue1, int time1, GameObject dialogue2, int time2)
    {

        dialoguePlaying = true;
        dialogue1.SetActive(true);
        yield return new WaitForSeconds(time1);
        dialogue1.SetActive(false);
        dialogue2.SetActive(true);
        yield return new WaitForSeconds(time2);
        dialogue2.SetActive(false);
        dialoguePlaying = false;
        gameObject.GetComponent<EnemyMovement>().unblocked = true;
        gameObject.GetComponent<EnemyMovement>().goBack = true;
        LivingRoom.GetComponent<LightSwitch>().LightsOn();
        security++;
    }
}
