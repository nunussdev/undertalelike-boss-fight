using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    [Header("Intro SFX")]
    public AudioSource IntroSfxSource;
    public AudioClip introSfx;

    [Header("Menu Sounds")]
    public AudioSource menuSource;
    public AudioClip menuMoveSfx;
    public AudioClip menuSelectSfx;

    public AudioClip winSfx;
    public AudioClip loseSfx;
    public AudioSource sfxSource;

    public AudioSource bgm;


    public BulletSpawner[] spawners;


    public GameObject dialogueBox;
    public GameObject battleBox;

    public HeartMovement heartMovement;

    public Transform checkPos;
    public Transform prayPos;


    public EnemyHealth enemy;

    public TextMeshProUGUI dialogueText;
    public static GameStateManager Instance;

    public Transform cursor;
    public Transform fightPos;
    public Transform actPos;
    public Transform itemPos;
    public Transform mercyPos;

    public Player player;

    private IState currentState;

    void Awake()
    {


        Instance = this;
    }

    void Start()
    {

        Time.timeScale = 1f;

        if (sfxSource != null && introSfx != null)
            sfxSource.PlayOneShot(introSfx);

        Transform[] positions = new Transform[]
        {
            fightPos,
            actPos,
            itemPos,
            mercyPos
        };

        ChangeState(new PlayerTurnState(this, cursor, positions));
    }

    void Update()
    {
        if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Time.timeScale = 1f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            return;
        }

        currentState?.Update();
    }


    public void ChangeState(IState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }


    public void GoToActMenu()
    {
        Transform[] actPositions = new Transform[]
        {
        checkPos,
        prayPos
        };

        ChangeState(new ActMenuState(this, cursor, actPositions));
    }

    public void GoToPlayerTurn()
    {
        Transform[] mainPositions = new Transform[]
        {
        fightPos,
        actPos,
        itemPos,
        mercyPos
        };

        ChangeState(new PlayerTurnState(this, cursor, mainPositions));
    }
    public void GoToEnemyTurn()
    {
        ChangeState(new EnemyTurnState(this));
    }

    public void SetDialogue(string text)
    {
        dialogueText.text = text;
    }

    public GameObject gameOverPanel;
    private bool gameOver = false;

    public void ShowGameOver()
    {
        if (gameOver) return;
        gameOver = true;

        if (bgm != null) bgm.Stop();
        if (sfxSource != null && loseSfx != null) sfxSource.PlayOneShot(loseSfx);

        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public GameObject winPanel;
    private bool win = false;

    public void ShowWin()
    {
        if (win) return;
        win = true;

        if (bgm != null) bgm.Stop();
        if (sfxSource != null && winSfx != null) sfxSource.PlayOneShot(winSfx);

        winPanel.SetActive(true);
        Time.timeScale = 0f;
    }


}
