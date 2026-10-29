using UnityEngine;
using System.Collections;

public class EnemyTurnState : IState
{
    private static AttackPattern lastChosen;

    private GameStateManager manager;
    private AttackPattern[] patterns;

    public EnemyTurnState(GameStateManager manager)
    {
        this.manager = manager;

        patterns = Object.FindObjectsByType<AttackPattern>(FindObjectsSortMode.None);
    }


    public void Enter()
    {
        manager.dialogueBox.SetActive(false);
        manager.battleBox.SetActive(true);

        manager.heartMovement.transform.position = new Vector3(0f, -2f, 0f);
        manager.heartMovement.enabled = true;

        Debug.Log("EnemyTurnState ENTER");

        manager.StartCoroutine(EnemyTurnRoutine());
    }
    public void Update()
    {
        
    }

    private IEnumerator EnemyTurnRoutine()
    {
        AttackPattern chosen;

        if (patterns.Length == 1)
        {
            chosen = patterns[0];
        }
        else
        {
            do
            {
                chosen = patterns[Random.Range(0, patterns.Length)];
            }
            while (chosen == lastChosen);
        }

        lastChosen = chosen;

        Debug.Log("Seçilen Pattern: " + chosen.name);
        yield return manager.StartCoroutine(chosen.Execute());
        manager.GoToPlayerTurn();

    }

    public void Exit()
    {
        manager.heartMovement.HardStop(); 
        manager.heartMovement.enabled = false;
        manager.battleBox.SetActive(false);

        Debug.Log("EnemyTurnState EXIT");
    }


}
