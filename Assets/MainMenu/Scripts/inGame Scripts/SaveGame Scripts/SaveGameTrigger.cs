using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SaveGameTrigger : MonoBehaviour {

    #region variables

    Animator anim;

    [Header("All other UI")]
    [Tooltip("No Need to Assign")]
    public Canvas[] allUI;

    [Header("References to UI")]
    [Tooltip("No Need to Assign")]
    public GameObject saveUI;
    [Tooltip("No Need to Assign")]
    public Text saveName_Txt;
    [Tooltip("No Need to Assign")]
    public Text savePercentage_Txt;

    [Header("Edit these in Inspector According to your level")]
    [Tooltip("Save Name which will be displayed in respective Save Slots")]
    public string saveName;
    [Tooltip("Game Completion percentage displayed in respective Save Slots")]
    public float savePercentage;
    [Tooltip("Scene to Load")]
    public string sceneName;
    [Tooltip("Player will spawn from this point when level loads")]
    public Transform spawnPoint;
    [Tooltip("Unique trigger ID to see is this the last save point")]
    public int saveTriggerId;
    [Tooltip("Debug to spawn player at this Trigger's spawnPoint")]
    public bool debugSpawn;

    #endregion

    // Use this for initialization
    void Start () {
       

        //checking is this the trigger where the player saved the game

    }

  
 
    private void Update()
    {
      
    }

}
