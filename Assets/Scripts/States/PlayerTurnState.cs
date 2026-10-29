using UnityEngine;
using System.Collections;


public class PlayerTurnState : IState
{
    private GameStateManager manager;
    private Transform cursor;
    private Transform[] positions;

    private int currentIndex = 0;

    private bool isWaiting = false;

    public PlayerTurnState(GameStateManager manager, Transform cursor, Transform[] positions)
    {
        this.manager = manager;
        this.cursor = cursor;
        this.positions = positions;
    }

    public void Enter()
    {
        manager.heartMovement.HardStop();

        manager.dialogueBox.SetActive(true);
        manager.battleBox.SetActive(false);

        manager.dialogueText.text = "* A mysterious man appears";

        manager.heartMovement.enabled = false; 

        currentIndex = 0;
        cursor.position = positions[currentIndex].position;

        Debug.Log("PlayerTurnState ENTER");
    }

    public void Update()
    {
       
        if (isWaiting) return;

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            int prev = currentIndex;

            currentIndex = Mathf.Clamp(currentIndex + 1, 0, positions.Length - 1);

            if (currentIndex != prev)
            {
                cursor.position = positions[currentIndex].position;
                manager.menuSource.PlayOneShot(manager.menuMoveSfx);
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            int prev = currentIndex;

            currentIndex = Mathf.Clamp(currentIndex - 1, 0, positions.Length - 1);

            if (currentIndex != prev)
            {
                cursor.position = positions[currentIndex].position;
                manager.menuSource.PlayOneShot(manager.menuMoveSfx);
            }
        }


        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("Seçilen: " + currentIndex);

            manager.menuSource.PlayOneShot(manager.menuSelectSfx);

            if (currentIndex == 0) // FIGHT
            {
                manager.StartCoroutine(FightRoutine());
                manager.enemy.TakeDamage(10);
                return;
            }

            if (currentIndex == 1) // ACT
            {
                manager.GoToActMenu();
                return;
            }

            if (currentIndex == 2) // ITEM
            {
                manager.StartCoroutine(ItemHealRoutine());
                return;
            }

            if (currentIndex == 3 && !isWaiting)
            {
                manager.StartCoroutine(MercyRoutine());
            }

        }
    }

    private IEnumerator ItemHealRoutine()
    {
        isWaiting = true;

        manager.player.Heal(10);
        manager.dialogueText.text = "* You recovered 10 HP.";

        yield return new WaitForSeconds(2f);

        isWaiting = false;

        manager.GoToEnemyTurn();

    }
    private IEnumerator MercyRoutine()
    {
        isWaiting = true;

        manager.dialogueText.text = "* You showed mercy... But he won't.";

        yield return new WaitForSeconds(2f);

        isWaiting = false;
        manager.GoToEnemyTurn(); 
    }

    private IEnumerator FightRoutine()
    {
        isWaiting = true;

        manager.dialogueText.text = "* You dealt 10 damage.";

        yield return new WaitForSeconds(2f);

        isWaiting = false;
        manager.GoToEnemyTurn(); 
    }



    public void Exit()
    {
        Debug.Log("PlayerTurnState EXIT");
    }
}
