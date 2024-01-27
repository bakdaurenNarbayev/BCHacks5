using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction.Locomotion;
using System;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public GameObject player, environment;
    public TeleportInteractor interactor;
    public VirtualActiveState activeState;
    public bool isSelected = false, isActive = false, isDataSent = false, isSessionStarted = false;
    private List<GameObject> platforms;
    private int current = 0, next = 1, previous = -1;
    private Vector3 playerPosition, platformPosition;
    private Color failColor, successColor, tailFailColor, tailSuccessColor, tailInvisible, currentColor, tailCurrentColor, bubbleColor;

    public enum StateType
    {
        BASE,
        PAUSE_SELECT,
        PAUSE_ACTIVE,
        COMET_VISIBLE,
        COMET_INVISIBLE,
        BUBBLE_VISIBLE,
        BUBBLE_INVISIBLE
    };

    public StateType state;

    private void DisplayNextPlatform() {
        if(interactor.Interactable != null && interactor.Interactable.AllowTeleport) {
            currentColor = successColor;
            tailCurrentColor = tailSuccessColor;
            bubbleColor = tailSuccessColor;
        } else {
            currentColor = failColor;
            tailCurrentColor = tailFailColor;
            bubbleColor = tailFailColor;
        }

        platforms[current].transform.Find("Ring").gameObject.GetComponent<Renderer>().material.color = currentColor;
        platforms[current].transform.Find("Hanger").gameObject.GetComponent<Renderer>().material.color = currentColor;

        if(state == StateType.COMET_VISIBLE) {
            platforms[current].transform.Find("Tail").gameObject.GetComponent<Renderer>().material.color = tailCurrentColor;
        } else if(state == StateType.COMET_INVISIBLE) {
            platforms[current].transform.Find("Tail").gameObject.GetComponent<Renderer>().material.color = tailInvisible;
        } else if(state == StateType.BUBBLE_VISIBLE) {
            platforms[current].transform.Find("Bubble").gameObject.GetComponent<Renderer>().material.color = bubbleColor;
        } else {
            platforms[current].transform.Find("Bubble").gameObject.GetComponent<Renderer>().material.color = tailInvisible;
        }
        
        playerPosition = player.transform.position;

        if(previous >= 0) {
            platformPosition = platforms[previous].gameObject.transform.position;
        } else {
            platformPosition = new Vector3(0, DatabaseManager.Instance.minPlatformHeight, 0);
            platforms[0].gameObject.SetActive(true);
        }
        
        if(playerPosition.z - platformPosition.z > 5) {
            if(previous >= 0) {
                platforms[previous].gameObject.SetActive(false);
            }
            platforms[current].gameObject.SetActive(true);
            if(next < platforms.Count) {
                platforms[next].gameObject.SetActive(true);
            }

            platforms[current].transform.Find("Ring").gameObject.SetActive(false);
            platforms[current].transform.Find("Collider").gameObject.SetActive(false);
            platforms[current].transform.Find("Hanger").gameObject.SetActive(false);

            if(state == StateType.COMET_VISIBLE) {
                platforms[current].transform.Find("Tail").gameObject.SetActive(false);
            }

            current++;
            previous++;
            next++;
        }
    }

    private void IdentifyState() {
        switch(state) {
            case StateType.PAUSE_SELECT:
                if(interactor.Interactable != null) {
                    isSelected = true;
                } else {
                    isSelected = false;
                }
                break;
            case StateType.PAUSE_ACTIVE:
                if(activeState.Active) {
                    isActive = true;
                } else {
                    isActive = false;
                }
                break;
            default:
                break;
        }
    }

    private void Start()
    {
        PlatformManager.Instance.GeneratePlatforms();
        PlatformManager.Instance.HidePlatforms();

        ColorUtility.TryParseHtmlString("#FF1F6233", out tailFailColor);
        ColorUtility.TryParseHtmlString("#00FF0033", out tailSuccessColor);
        ColorUtility.TryParseHtmlString("#FFFFFF00", out tailInvisible);
        ColorUtility.TryParseHtmlString("#FF1F62", out failColor);
        ColorUtility.TryParseHtmlString("#00FF00", out successColor);
    }

    private void Update()
    {
        DatabaseManager.Instance.GetSessionInfo();

        environment.SetActive(DatabaseManager.Instance.environment);
        Camera.main.clearFlags = DatabaseManager.Instance.skybox ? CameraClearFlags.Skybox : CameraClearFlags.SolidColor;

        PlatformManager.Instance.IdentifyTechnique();
        PlatformManager.Instance.UpdatePlatforms();
        platforms = PlatformManager.Instance.platforms;

        if (current >= platforms.Count)
        {
            if (previous >= 0)
            {
                player.transform.position = platforms[previous].transform.position;
                if (!isDataSent)
                {
                    //DatabaseManager.Instance.FinishSession();
                    isDataSent = true;
                }
            }
            return;
        };

        IdentifyState();
        DisplayNextPlatform();

        if (previous >= 0)
        {
            player.transform.position = platforms[previous].transform.position;
        } else
        {
            player.transform.position = new Vector3(0, DatabaseManager.Instance.minPlatformHeight, 0);
        }

        if (isSessionStarted)
        {

        }
        else
        {
            DatabaseManager.Instance.GetSessionInfo();
        }
    }
}