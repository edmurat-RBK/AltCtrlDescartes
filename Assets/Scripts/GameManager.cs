using System.Collections;
using System.Collections.Generic;
using Uduino;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;
using UnityEngine.SceneManagement;

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


    public bool isStartRound;
    private bool guideFeedbackFlag;
    public enum MenuButton { Start, Credits, Quit };
    public MenuButton[] menuButtons = new MenuButton[3];
    public float timeBeforeExecuteButton;

    #region transitions 
    public Animator transitionj1;
    public Animator transitionj2;
    #endregion

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

    //couleurs
    [SerializeField]
    private Vector4 bleu = new Vector4(0, 108, 220, 255);
    [SerializeField]
    private Vector4 jaune = new Vector4(253, 214, 1, 255);
    [SerializeField]
    private Vector4 orange = new Vector4(191, 27, 4, 255);

    //effects rank1
    public GameObject FeedbackRang1ObjectPlayer1;
    public VisualEffect FeedbackRang1Player1;
    public GameObject FeedbackRang1ObjectPlayer2;
    public VisualEffect FeedbackRang1Player2;
    //effects rank2
    //player 1
    public GameObject FeedbackRang2ObjectPlayer1Blue;
    public VisualEffect FeedbackRang2Player1Blue;
    public GameObject FeedbackRang2ObjectPlayer1Yellow;
    public VisualEffect FeedbackRang2Player1Yellow;
    public GameObject FeedbackRang2ObjectPlayer1Orange;
    public VisualEffect FeedbackRang2Player1Orange;
    //player 2
    public GameObject FeedbackRang2ObjectPlayer2Blue;
    public VisualEffect FeedbackRang2Player2Blue;
    public GameObject FeedbackRang2ObjectPlayer2Yellow;
    public VisualEffect FeedbackRang2Player2Yellow;
    public GameObject FeedbackRang2ObjectPlayer2Orange;
    public VisualEffect FeedbackRang2Player2Orange;
    //effects rank3
    //Player1
    public GameObject FeedbackRang3ObjectPlayer1Blue;
    public VisualEffect FeedbackRang3Player1Blue;
    public GameObject FeedbackRang3ObjectPlayer1Yellow;
    public VisualEffect FeedbackRang3Player1Yellow;
    public GameObject FeedbackRang3ObjectPlayer1Orange;
    public VisualEffect FeedbackRang3Player1Orange;
    //Player2
    public GameObject FeedbackRang3ObjectPlayer2Blue;
    public VisualEffect FeedbackRang3Player2Blue;
    public GameObject FeedbackRang3ObjectPlayer2Yellow;
    public VisualEffect FeedbackRang3Player2Yellow;
    public GameObject FeedbackRang3ObjectPlayer2Orange;
    public VisualEffect FeedbackRang3Player2Orange;

    #endregion

    private void Start()
    {
        audioManager = AudioManager.Instance;
        state = GameState.WAIT_BOTH;
        score = FindObjectOfType<SynchroScore>();//new SynchroScore();
        coroutineFeedback = false;
        DisableAllFeedbacks();
    }

    private void Update()
    {
        switch (state)
        {
            case GameState.WAIT_BOTH:
                guideFeedbackFlag = true;
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
                if (!coroutineFeedback)
                {
                    if (SlotManager.Instance.cardDown[(int)PlayerName.CASTOR])
                    {
                        StartCoroutine(SwitchStateFeedback());
                    }
                    else if (isStartRound && guideFeedbackFlag)
                    {
                        guideFeedbackFlag = false;
                        CardSymbol symbolPollux;
                        int slotPollux;
                        SlotManager.Instance.ReturnPollux(out slotPollux, out symbolPollux);
                        if (slotPollux == 3)
                        {
                            Debug.Log(slotPollux);
                            PositionInstanceP1 = Slot1Joueur1.transform.position;
                        }
                        if (slotPollux == 4)
                        {
                            PositionInstanceP1 = Slot2Joueur1.transform.position;
                        }
                        if (slotPollux == 5)
                        {
                            PositionInstanceP1 = Slot3Joueur1.transform.position;
                        }
                        FeedbackRang1ObjectPlayer1.transform.position = PositionInstanceP1;
                        FeedbackRang1ObjectPlayer1.SetActive(true);
                        switch (symbolPollux)
                        {

                            case CardSymbol.PINK:
                                Debug.Log(symbolPollux);
                                FeedbackRang1Player1.SetVector4("Couleur rectangles", jaune);
                                Debug.Log("colorchange");
                                break;

                            case CardSymbol.BLUE:
                                Debug.Log("blue");
                                FeedbackRang1Player1.SetVector4("Couleur rectangles", bleu);
                                Debug.Log("colorchangeblue");
                                break;

                            case CardSymbol.ORANGE:
                                Debug.Log(symbolPollux);
                                FeedbackRang1Player1.SetVector4("Couleur rectangles", orange);
                                Debug.Log("colorchange");
                                break;

                            default:
                                throw new System.Exception();
                        }
                        FeedbackRang1Player1.Play();
                    }
                }
                break;

            case GameState.WAIT_POLLUX:
                if (!coroutineFeedback)
                {
                    if (SlotManager.Instance.cardDown[(int)PlayerName.POLLUX])
                    {
                        StartCoroutine(SwitchStateFeedback());
                    }
                    else if (isStartRound && guideFeedbackFlag)
                    {
                        guideFeedbackFlag = false;
                        CardSymbol symbolCastor;
                        int slotCastor;
                        SlotManager.Instance.ReturnCastor(out slotCastor, out symbolCastor);
                        if (slotCastor == 0)
                        {
                            PositionInstanceP2 = Slot1Joueur2.transform.position;
                        }
                        if (slotCastor == 1)
                        {
                            PositionInstanceP2 = Slot2Joueur2.transform.position;
                        }
                        if (slotCastor == 2)
                        {
                            PositionInstanceP2 = Slot3Joueur2.transform.position;
                        }
                        FeedbackRang1ObjectPlayer2.transform.position = PositionInstanceP2;
                        FeedbackRang1ObjectPlayer2.SetActive(true);
                        switch (symbolCastor)
                        {
                            case CardSymbol.PINK:
                                FeedbackRang1Player2.SetVector4("Couleur rectangles", jaune);
                                break;

                            case CardSymbol.BLUE:
                                FeedbackRang1Player2.SetVector4("Couleur rectangles", bleu);
                                break;

                            case CardSymbol.ORANGE:
                                FeedbackRang1Player2.SetVector4("Couleur rectangles", orange);
                                break;

                            default:
                                throw new System.Exception();
                        }
                        FeedbackRang1Player2.Play();
                    }
                }
                break;

            case GameState.FEEDBACK:
                // Compare cards
                CardSymbol successSymbol;
                int slotIndex;
                if (!isStartRound)
                {
                    if (SlotManager.Instance.CheckFullSuccess(out successSymbol, out slotIndex))
                    {
                        ObjectiveManager.Instance.Success(successSymbol);
                        score.IncreaseScore();
                    }
                    else
                    {
                        score.DecayScore();
                    }
                }



                // Send feedback

                if (!isStartRound)
                {
                    if (ObjectiveManager.Instance.BothFinish())
                    {
                        state = GameState.END;
                    }
                    else
                    {
                        state = GameState.RESET;
                    }
                }
                else
                {
                    state = GameState.RESET;
                }
                break;

            case GameState.RESET:
                foreach (Slot s in SlotManager.Instance.slots)
                {
                    s.ResetToNextTurn(isStartRound);
                }
                //eteindre les feedbacks
                DisableAllFeedbacks();

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

        if (isStartRound)
        {
            FeedbackRang1Player1.Stop();
            FeedbackRang1Player2.Stop();
            FeedbackRang1ObjectPlayer1.SetActive(false);
            FeedbackRang1ObjectPlayer2.SetActive(false);
        }

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
            if (slotIndex == 0)
            {
                PositionInstanceP1 = Slot1Joueur1.transform.position;
                PositionInstanceP2 = Slot1Joueur2.transform.position;
            }
            if (slotIndex == 1)
            {
                PositionInstanceP1 = Slot2Joueur1.transform.position;
                PositionInstanceP2 = Slot2Joueur2.transform.position;
            }
            if (slotIndex == 2)
            {
                PositionInstanceP1 = Slot3Joueur1.transform.position;
                PositionInstanceP2 = Slot3Joueur2.transform.position;
            }
            //on set la posiion des feedbacks
            FeedbackRang3ObjectPlayer1Blue.transform.position = PositionInstanceP1;
            FeedbackRang3ObjectPlayer2Blue.transform.position = PositionInstanceP2;
            FeedbackRang3ObjectPlayer1Yellow.transform.position = PositionInstanceP1;
            FeedbackRang3ObjectPlayer2Yellow.transform.position = PositionInstanceP2;
            FeedbackRang3ObjectPlayer1Orange.transform.position = PositionInstanceP1;
            FeedbackRang3ObjectPlayer2Orange.transform.position = PositionInstanceP2;

            //on joue les feedbacks selon la couleur
            switch (successSymbol)
            {
                case CardSymbol.PINK:
                    FeedbackRang3ObjectPlayer1Yellow.SetActive(true);
                    FeedbackRang3ObjectPlayer2Yellow.SetActive(true);
                    FeedbackRang3Player1Yellow.Play();
                    FeedbackRang3Player2Yellow.Play();
                    break;

                case CardSymbol.BLUE:
                    FeedbackRang3ObjectPlayer1Blue.SetActive(true);
                    FeedbackRang3ObjectPlayer2Blue.SetActive(true);
                    FeedbackRang3Player1Blue.Play();
                    FeedbackRang3Player2Blue.Play();
                    break;

                case CardSymbol.ORANGE:
                    FeedbackRang3ObjectPlayer1Orange.SetActive(true);
                    FeedbackRang3ObjectPlayer2Orange.SetActive(true);
                    FeedbackRang3Player1Orange.Play();
                    FeedbackRang3Player2Orange.Play();
                    break;

                default:
                    throw new System.Exception();
            }

            if (isStartRound)
            {
                ClickButton(slotIndex);
            }
        }
        else
        {

            if (SlotManager.Instance.CheckSlotSuccess(out slotIndex, out symbolCastor, out symbolPollux))
            {
                //feedbacks de rang2 (pas le meme signe mais le meme emplacement)
                Debug.Log("Slot Success on " + slotIndex);
                audioManager.PlaySFX(feedbackSlot, volumeSlot);
                //on modifie les positions des feedbacks
                if (slotIndex == 0)
                {
                    PositionInstanceP1 = Slot1Joueur1.transform.position;
                    PositionInstanceP2 = Slot1Joueur2.transform.position;
                }
                if (slotIndex == 1)
                {
                    PositionInstanceP1 = Slot2Joueur1.transform.position;
                    PositionInstanceP2 = Slot2Joueur2.transform.position;
                }
                if (slotIndex == 2)
                {
                    PositionInstanceP1 = Slot3Joueur1.transform.position;
                    PositionInstanceP2 = Slot3Joueur2.transform.position;
                }
                //on set la posiion des feedbacks
                FeedbackRang2ObjectPlayer1Blue.transform.position = PositionInstanceP1;
                FeedbackRang2ObjectPlayer2Blue.transform.position = PositionInstanceP2;
                FeedbackRang2ObjectPlayer1Yellow.transform.position = PositionInstanceP1;
                FeedbackRang2ObjectPlayer2Yellow.transform.position = PositionInstanceP2;
                FeedbackRang2ObjectPlayer1Orange.transform.position = PositionInstanceP1;
                FeedbackRang2ObjectPlayer2Orange.transform.position = PositionInstanceP2;


                //affiche, change la couleur et joue le feedback pour le player 2 par rapport à la couleur du player 1
                switch (symbolCastor)
                {
                    case CardSymbol.PINK:
                        FeedbackRang2ObjectPlayer2Yellow.SetActive(true);
                        FeedbackRang2Player2Yellow.SetVector4("Couleur rectangles", jaune);
                        FeedbackRang2Player2Yellow.Play();
                        break;

                    case CardSymbol.ORANGE:
                        FeedbackRang2ObjectPlayer2Orange.SetActive(true);
                        FeedbackRang2Player2Orange.SetVector4("Couleur rectangles", orange);
                        FeedbackRang2Player2Orange.Play();
                        break;

                    case CardSymbol.BLUE:
                        FeedbackRang2ObjectPlayer2Blue.SetActive(true);
                        FeedbackRang2Player2Blue.SetVector4("Couleur rectangles", bleu);
                        FeedbackRang2Player2Blue.Play();
                        break;
                }
                //affiche, change la couleur et joue le feedback pour le player 1 par rapport à la couleur du player 2
                switch (symbolPollux)
                {
                    case CardSymbol.PINK:
                        FeedbackRang2ObjectPlayer1Yellow.SetActive(true);
                        FeedbackRang2Player1Yellow.SetVector4("Couleur rectangles", jaune);
                        FeedbackRang2Player1Yellow.Play();
                        break;

                    case CardSymbol.ORANGE:
                        FeedbackRang2ObjectPlayer1Orange.SetActive(true);
                        FeedbackRang2Player1Orange.SetVector4("Couleur rectangles", orange);
                        FeedbackRang2Player1Orange.Play();
                        break;

                    case CardSymbol.BLUE:
                        FeedbackRang2ObjectPlayer1Blue.SetActive(true);
                        FeedbackRang2Player1Blue.SetVector4("Couleur rectangles", bleu);
                        FeedbackRang2Player1Blue.Play();
                        break;
                }


            }
            else
            {
                //feedbacks de rang1 (pas le meme emplacement)
                Debug.Log("pas meme endroit");

                audioManager.PlaySFX(feedbackSymbol, volumeSymbol);
                SlotManager.Instance.ReturnAll(out slotCastor, out slotPollux, out symbolCastor, out symbolPollux);
                ////ci dessous on donne la position de la ou lancer les feedbacks
                if (slotCastor == 0)
                {
                    PositionInstanceP2 = Slot1Joueur2.transform.position;
                }
                if (slotCastor == 1)
                {
                    PositionInstanceP2 = Slot2Joueur2.transform.position;
                }
                if (slotCastor == 2)
                {
                    PositionInstanceP2 = Slot3Joueur2.transform.position;
                }
                if (slotPollux == 3)
                {
                    Debug.Log(slotPollux);
                    PositionInstanceP1 = Slot1Joueur1.transform.position;
                }
                if (slotPollux == 4)
                {
                    PositionInstanceP1 = Slot2Joueur1.transform.position;
                }
                if (slotPollux == 5)
                {
                    PositionInstanceP1 = Slot3Joueur1.transform.position;
                }
                FeedbackRang1ObjectPlayer1.transform.position = PositionInstanceP1;
                FeedbackRang1ObjectPlayer2.transform.position = PositionInstanceP2;
                //on set active les objets dans lesquels les feedbacks sont
                FeedbackRang1ObjectPlayer1.SetActive(true);
                FeedbackRang1ObjectPlayer2.SetActive(true);
                //on change la couleur des feedbacks
                //couleur feedback player2
                switch (symbolCastor)
                {
                    case CardSymbol.PINK:
                        FeedbackRang1Player2.SetVector4("Couleur rectangles", jaune);
                        break;

                    case CardSymbol.BLUE:
                        FeedbackRang1Player2.SetVector4("Couleur rectangles", bleu);
                        break;

                    case CardSymbol.ORANGE:
                        FeedbackRang1Player2.SetVector4("Couleur rectangles", orange);
                        break;

                    default:
                        throw new System.Exception();
                }
                //couleur feedback player1
                switch (symbolPollux)
                {

                    case CardSymbol.PINK:
                        Debug.Log(symbolPollux);
                        FeedbackRang1Player1.SetVector4("Couleur rectangles", jaune);
                        Debug.Log("colorchange");
                        break;

                    case CardSymbol.BLUE:
                        Debug.Log("blue");
                        FeedbackRang1Player1.SetVector4("Couleur rectangles", bleu);
                        Debug.Log("colorchangeblue");
                        break;

                    case CardSymbol.ORANGE:
                        Debug.Log(symbolPollux);
                        FeedbackRang1Player1.SetVector4("Couleur rectangles", orange);
                        Debug.Log("colorchange");
                        break;

                    default:
                        throw new System.Exception();
                }
                //on lance les feedbacks
                FeedbackRang1Player1.Play();
                FeedbackRang1Player2.Play();


            }
        }

        yield return new WaitForSeconds(3f);
        state = GameState.FEEDBACK;
        coroutineFeedback = false;
    }

    private void ClickButton(int slot)
    {
        switch (menuButtons[slot])
        {
            case MenuButton.Start:
                Invoke("StartGame", timeBeforeExecuteButton);
                break;


            case MenuButton.Credits:
                Invoke("GoCredits", timeBeforeExecuteButton);
                break;


            case MenuButton.Quit:
                Invoke("Quit", timeBeforeExecuteButton);
                break;
        }
    }

    private void DisableAllFeedbacks()
    {
        //feedbacks rang 1
        FeedbackRang1Player1.Stop();
        FeedbackRang1Player2.Stop();
        FeedbackRang1ObjectPlayer1.SetActive(false);
        FeedbackRang1ObjectPlayer2.SetActive(false);
        //feedbacks rang 2
        FeedbackRang2Player1Blue.Stop();
        FeedbackRang2Player2Blue.Stop();
        FeedbackRang2Player1Yellow.Stop();
        FeedbackRang2Player2Yellow.Stop();
        FeedbackRang2Player1Orange.Stop();
        FeedbackRang2Player2Orange.Stop();
        FeedbackRang2ObjectPlayer1Blue.SetActive(false);
        FeedbackRang2ObjectPlayer2Blue.SetActive(false);
        FeedbackRang2ObjectPlayer1Yellow.SetActive(false);
        FeedbackRang2ObjectPlayer2Yellow.SetActive(false);
        FeedbackRang2ObjectPlayer1Orange.SetActive(false);
        FeedbackRang2ObjectPlayer2Orange.SetActive(false);
        //feedbacks rang 3
        FeedbackRang3Player1Blue.Stop();
        FeedbackRang3Player2Blue.Stop();
        FeedbackRang3Player1Yellow.Stop();
        FeedbackRang3Player2Yellow.Stop();
        FeedbackRang3Player1Orange.Stop();
        FeedbackRang3Player2Orange.Stop();
        FeedbackRang3ObjectPlayer1Blue.SetActive(false);
        FeedbackRang3ObjectPlayer2Blue.SetActive(false);
        FeedbackRang3ObjectPlayer1Yellow.SetActive(false);
        FeedbackRang3ObjectPlayer2Yellow.SetActive(false);
        FeedbackRang3ObjectPlayer1Orange.SetActive(false);
        FeedbackRang3ObjectPlayer2Orange.SetActive(false);
    }

    private void StartGame()
    {
        StartCoroutine(LoadLevel());
        
    }
    IEnumerator LoadLevel()
    {
        //play animation
        transitionj1.SetTrigger("Changescene");
        transitionj2.SetTrigger("Changescene");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
    }
    private void GoCredits()
    {
        SceneManager.LoadScene(2);
        Debug.Log("Credit");
    }

    private void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
