using UnityEngine;
using System.Collections;

public class ActMenuState : IState
{
    private bool canSelect = false;


    private bool locked = false;

    private GameStateManager manager;
    private Transform cursor;
    private Transform[] positions;

    private int currentIndex = 0;

    private bool isWaiting = false;


    public ActMenuState(GameStateManager manager, Transform cursor, Transform[] positions)
    {
        this.manager = manager;
        this.cursor = cursor;
        this.positions = positions;
    }

    public void Enter()
    {
        manager.heartMovement.enabled = false;
        manager.dialogueText.text = " CHECK\n\n PRAY";


        currentIndex = 0;
        cursor.position = positions[currentIndex].position;

        Debug.Log("ActMenuState ENTER");
    }




    public void Update()
    {
        if (locked) return;

        


        // SAĞ
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            int prev = currentIndex;

            currentIndex++;
            currentIndex = Mathf.Clamp(currentIndex, 0, positions.Length - 1);

            if (currentIndex != prev)
            {
                cursor.position = positions[currentIndex].position;
                manager.menuSource.PlayOneShot(manager.menuMoveSfx);
            }
        }


        // SOL
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            int prev = currentIndex;

            currentIndex--;
            currentIndex = Mathf.Clamp(currentIndex, 0, positions.Length - 1);

            if (currentIndex != prev)
            {
                cursor.position = positions[currentIndex].position;
                manager.menuSource.PlayOneShot(manager.menuMoveSfx);
            }
        }

        
        
        if (!canSelect && Input.GetKeyUp(KeyCode.Z))
        {
            
            canSelect = true;
        }

        
        if (Input.GetKeyDown(KeyCode.Z) && !isWaiting)
        {
            manager.menuSource.PlayOneShot(manager.menuSelectSfx);

            if (currentIndex == 0)
            {
                manager.StartCoroutine(CheckRoutine());

            }
            else if (currentIndex == 1)
            {
                manager.StartCoroutine(PrayRoutine());

            }
        }




        if (Input.GetKeyDown(KeyCode.X) && !locked)
        {
            manager.GoToPlayerTurn();
        }

    }

    private IEnumerator CheckRoutine()
    {
        isWaiting = true;

        manager.dialogueText.text = "* KELGUMBAL  -  ATK 10 DEF 100\n\n* It seems your end has come.";

        yield return new WaitForSeconds(2f);

        isWaiting = false;
        manager.GoToEnemyTurn();
    }

    private IEnumerator PrayRoutine()
    {
        isWaiting = true;

        manager.dialogueText.text = "* Nothing happened.";

        yield return new WaitForSeconds(2f);

        isWaiting = false;
        manager.GoToEnemyTurn();
    }

    public void Exit()
    {
        Debug.Log("ActMenuState EXIT");

    }
}
