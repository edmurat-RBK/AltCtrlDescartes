using System.Collections;
using System.Collections.Generic;
using Uduino;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance
    {
        get
        {
            if (_instance != null)
                return _instance;

            GameManager[] managers = FindObjectsOfType(typeof(GameManager)) as GameManager[];
            if (managers.Length == 0)
            {
                Debug.LogWarning("GameManager not present on the scene. Creating a new one.");
                GameManager manager = new GameObject("Game Manager").AddComponent<GameManager>();
                _instance = manager;
                return _instance;
            }
            else
            {
                return managers[0];
            }
        }
        set
        {
            if (_instance == null)
                _instance = value;
            else
            {
                Debug.LogError("You can only use one GameManager. Destroying the new one attached to the GameObject " + value.gameObject.name);
                Destroy(value);
            }
        }
    }
    private static GameManager _instance = null;
    #endregion

    public GameState state;
    public SynchroScore score;
    public Text scoreDisplayCastor;
    public Text scoreDisplayPollux;
    private bool coroutineFeedback;

    #region Son
    public AudioManager audioManager;
    public AudioClip feedbackPositif;
    public float volumePositif;
    public AudioClip feedbackSymbol;
    public float volumeSymbol;
    public AudioClip feedbackSlot;
    public float volumeSlot;
    #endregion

    #region Feedbacks Graphiques
    //positions
    public GameObject Slot1Joueur1;
    public GameObject Slot2Joueur1;
    public GameObject Slot3Joueur1;
    public GameObject Slot1Joueur2;
    public GameObject Slot2Joueur2;
    public GameObject Slot3Joueur2;
    private Vector3 PositionInstanceP1;
    private Vector3 PositionInstanceP2;
            //effets

    //effects rank1
    public GameObject FeedbackRang1ObjectPlayer1;
    public VisualEffect FeedbackRang1Player1;
    public GameObject FeedbackRang1ObjectPlayer2;
    public VisualEffect FeedbackRang1Player2;
    //effects rank2
    public GameObject FeedbackRang2ObjectPlayer1;
    public VisualEffect FeedbackRang2Player1;
    public GameObject FeedbackRang2ObjectPlayer2;
    public VisualEffect FeedbackRang2Player2;
    public GameObject FeedbackRang3ObjectPlayer1;
    public VisualEffect FeedbackRang3Player1;
    public GameObject FeedbackRang3ObjectPlayer2;
    public VisualEffect FeedbackRang3Player2;
    #endregion

    private void Start()
    {
        audioManager = AudioManager.Instance;
        state = GameState.WAIT_BOTH;
        score = new SynchroScore();
        coroutineFeedback = false;
    }

    private void Update()
    {
        switch (state)
        {
            case GameState.WAIT_BOTH:
                if (SlotManager.Instance.cardDown[(int)PlayerName.CASTOR])
                {
                    state = GameState.WAIT_POLLUX;
                }
                else if (SlotManager.Instance.cardDown[(int)PlayerName.POLLUX])
                {
                    state = GameState.WAIT_CASTOR;
                }
                break;

            case GameState.WAIT_CASTOR:
                if (SlotManager.Instance.cardDown[(int)PlayerName.CASTOR] && !coroutineFeedback)
                {
                    StartCoroutine(SwitchStateFeedback());
                }
                break;

            case GameState.WAIT_POLLUX:
                if (SlotManager.Instance.cardDown[(int)PlayerName.POLLUX] && !coroutineFeedback)
                {
                    StartCoroutine(SwitchStateFeedback());
                }
                break;

            case GameState.FEEDBACK:
                // Compare cards
                CardSymbol successSymbol;
                int slotIndex;
                if (SlotManager.Instance.CheckFullSuccess(out successSymbol, out slotIndex))
                {
                    ObjectiveManager.Instance.Success(successSymbol);
                    score.IncreaseScore();
                }
                else
                {
                    score.DecayScore();
                }

                scoreDisplayCastor.text = score.score + "%";
                scoreDisplayPollux.text = score.score + "%";

                // Send feedback


                if (ObjectiveManager.Instance.BothFinish())
                {
                    state = GameState.END;
                }
                else
                {
                    state = GameState.RESET;
                }
                break;

            case GameState.RESET:
                foreach (Slot s in SlotManager.Instance.slots)
                {
                    s.ResetToNextTurn();
                }

                SlotManager.Instance.cardDown[0] = false;
                SlotManager.Instance.cardDown[1] = false;

                state = GameState.WAIT_BOTH;
                break;

            case GameState.END:
                Debug.LogWarning("Game ended !");
                break;
        }
    }

    IEnumerator SwitchStateFeedback()
    {
        coroutineFeedback = true;

        CardSymbol successSymbol;
        CardSymbol symbolCastor;
        CardSymbol symbolPollux;
        int slotPollux;
        int slotIndex;
        int slotCastor;
        if (SlotManager.Instance.CheckFullSuccess(out successSymbol, out slotIndex))
        {
            //feedbacks de type 3: meme symbole, meme emplacement
            Debug.Log("Full Success");
            audioManager.PlaySFX(feedbackPositif, volumePositif);
           
        }

        else
        {

            if (SlotManager.Instance.CheckSlotSuccess(out slotIndex, out symbolCastor, out symbolPollux))
            {
                //feedbacks de rang2 (pas le meme signe mais le meme emplacement)
                Debug.Log("Slot Success");
                audioManager.PlaySFX(feedbackSlot, volumeSlot);


            }
            else
            {
                //feedbacks de rang1 (pas le meme emplacement)
                Debug.Log("Symbol Success");
                audioManager.PlaySFX(feedbackSymbol, volumeSymbol);
                SlotManager.Instance.ReturnAll(out slotCastor, out slotPollux, out symbolCastor, out symbolPollux);
                //ci dessous on donne la position de la ou lancer les feedbacks
                //if (slotCastor == 1)
                //{
                //    PositionInstanceP2 = Slot1Joueur2.transform.position;
                //}
                //else if (slotCastor == 2)
                //{
                //    PositionInstanceP2 = Slot2Joueur2.transform.position;
                //}
                //else if (slotCastor == 3)
                //{
                //    PositionInstanceP2 = Slot3Joueur2.transform.position;
                //}
                //if (slotPollux == 4)
                //{
                //    PositionInstanceP2 = Slot1Joueur2.transform.position;
                //}
                //else if (slotPollux == 5)
                //{
                //    PositionInstanceP2 = Slot2Joueur2.transform.position;
                //}
                //else if (slotPollux == 6)
                //{
                //    PositionInstanceP2 = Slot3Joueur2.transform.position;
                //}
                //FeedbackRang1ObjectPlayer1.transform.position = PositionInstanceP1;
                //FeedbackRang1ObjectPlayer2.transform.position = PositionInstanceP2;
                //on set active les objets dans lesquels les feedbacks sont
                //FeedbackRang1ObjectPlayer1.SetActive(true);
                //FeedbackRang1ObjectPlayer1.SetActive(true);
                ////on change la couleur des feedbacks

                ////on lance les feedbacks
                //FeedbackRang1Player1.Play();
                //FeedbackRang1Player2.Play();


            }
        }

        yield return new WaitForSeconds(3f);
        state = GameState.FEEDBACK;
        coroutineFeedback = false;
    }
}
