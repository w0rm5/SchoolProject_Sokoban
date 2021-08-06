using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private bool readyForInput = false;
    private Vector2 movement;
    private AudioManager audioManager;

    public Animator PlayerAnimator;
    public SpriteRenderer PlayerSpriteRenderer;
    public GameObject WinPanel;
    public GameObject LevelTextPanel;
    public GameObject PauseMenuPanal;
    public GameObject PauseButton;
    public GameObject UndoButton;

    private readonly Stack<MoveHistory> MoveHistories = new Stack<MoveHistory>();

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.Play("startingSound");
        StartCoroutine(WaitForLevelText());        
    }

    private IEnumerator WaitForLevelText()
    {
        LevelTextPanel.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        LevelTextPanel.SetActive(false);
        PauseButton.SetActive(true);
        UndoButton.SetActive(true);
        audioManager.Play("levelBgMusic");
    }

    private void Update()
    {
        if (!LevelTextPanel.activeSelf && !WinPanel.activeSelf && !PauseMenuPanal.activeSelf)
        {
            MovePlayer();
        }
        else if(PauseMenuPanal.activeSelf && Input.GetKeyUp(KeyCode.Escape))
        {
            PauseGame(false);
        }
    }

    public void PauseGame(bool pause)
    {
        PauseMenuPanal.SetActive(pause);
        PauseButton.SetActive(!pause);
        UndoButton.SetActive(!pause);
    }

    private void MovePlayer()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            PauseGame(!PauseMenuPanal.activeSelf);
        }

        if (Input.GetKeyUp(KeyCode.F2))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
        if (Input.GetKeyUp(KeyCode.Z))
        {
            UndoMove();
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        PlayerAnimator.SetFloat("Horizontal", movement.x);
        PlayerAnimator.SetFloat("Vertical", movement.y);
        PlayerAnimator.SetFloat("Speed", movement.sqrMagnitude);

        if (movement.x > 0)
        {
            PlayerSpriteRenderer.flipX = true;
        }
        else
        {
            PlayerSpriteRenderer.flipX = false;
        }

        Vector2 input = new Vector2(movement.x, movement.y);
        input.Normalize();

        if (input.sqrMagnitude > 0.5)
        {
            if (readyForInput)
            {
                readyForInput = false;
                Move(input);
                if (IsLevelComplete())
                {
                    PauseButton.SetActive(false);
                    UndoButton.SetActive(false);
                    WinPanel.SetActive(true);
                }
            }
        }
        else
        {
            readyForInput = true;
        }
    }

    public void Move(Vector2 direction)
    {
        audioManager.Play("walkingSound");
        if (Mathf.Abs(direction.x) < 0.5)
        {
            direction.x = 0;
        }
        else
        {
            direction.y = 0;
        }
        direction.Normalize();
        if (!Blocked(transform.position, direction))
        {
            transform.Translate(direction);
        }
    }

    bool Blocked(Vector3 position, Vector2 direction)
    {
        Vector2 newPos = new Vector2(position.x, position.y) + direction;
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        foreach (GameObject wall in walls)
        {
            if (Mathf.Approximately(wall.transform.position.x, newPos.x)
                && Mathf.Approximately(wall.transform.position.y , newPos.y))
            {
                return true;
            }
        }
        GameObject[] boxes = GameObject.FindGameObjectsWithTag("Box");
        foreach (GameObject box in boxes)
        {
            if (Mathf.Approximately(box.transform.position.x, newPos.x)
                && Mathf.Approximately(box.transform.position.y, newPos.y))
            {
                Box bx = box.GetComponent<Box>();
                if (bx && bx.Move(direction))
                {
                    MoveHistories.Push(new MoveHistory(transform.position, (Vector2)box.transform.position - direction, box));
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        return false;
    }


    public void UndoMove()
    {
        if(MoveHistories.Count > 0)
        {
            MoveHistory previousMove = MoveHistories.Pop();
            transform.position = previousMove.PlayerPosition;
            previousMove.BoxReference.transform.position = previousMove.BoxPosition;
            previousMove.BoxReference.GetComponent<Box>().TransformOnTarget();
        }
    }

    private bool IsLevelComplete()
    {
        Box[] boxes = FindObjectsOfType<Box>();
        foreach(Box box in boxes)
        {
            if (!box.onTarget) return false;
        }
        return true;
    }
}
