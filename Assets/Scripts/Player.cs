using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //public Rigidbody2D PlayerRigidbody;
    //readonly float MovementSpeed = 2.5f;
    private bool readyForInput = false;
    private Vector2 movement;
    private AudioManager audioManager;

    public Animator PlayerAnimator;
    public SpriteRenderer PlayerSpriteRenderer;
    public GameObject WinPanel;
    public GameObject LevelTextPanel;
    public GameObject PauseMenuPanal;
    public GameObject PauseButton;

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
        audioManager.Play("levelBgMusic");
    }

    private void Update()
    {
        if(!LevelTextPanel.activeSelf && !WinPanel.activeSelf && !PauseMenuPanal.activeSelf)
        {
            MovePlayer();
        }
        else if(PauseMenuPanal.activeSelf && Input.GetKeyUp(KeyCode.Escape))
        {
            PauseMenuPanal.SetActive(false);
            PauseButton.SetActive(true);
        }
    }

    private void MovePlayer()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            PauseMenuPanal.SetActive(true);
            PauseButton.SetActive(false);
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
                    WinPanel.SetActive(true);
                }
            }
        }
        else
        {
            readyForInput = true;
        }
    }

    //private void FixedUpdate()
    //{
    //    PlayerRigidbody.MovePosition(PlayerRigidbody.position + movement * MovementSpeed * Time.fixedDeltaTime);
    //}

    public void Move(Vector2 direction)
    {
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
            if (wall.transform.position.x == newPos.x && wall.transform.position.y == newPos.y)
            {
                return true;
            }
        }
        GameObject[] boxes = GameObject.FindGameObjectsWithTag("Box");
        foreach (GameObject box in boxes)
        {
            if (box.transform.position.x == newPos.x && box.transform.position.y == newPos.y)
            {
                Box bx = box.GetComponent<Box>();
                if (bx && bx.Move(direction))
                {
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
