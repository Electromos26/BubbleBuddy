using Dan.Main;
using Dan.Models;
using Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UI;

public class LeaderBoardManager : MonoBehaviour
{
    public GameEvent Event;
    [SerializeField] private LBPlayerHolder[] scoreFields;

    [SerializeField] private TextMeshProUGUI ScoreText;
    [SerializeField] private TMP_InputField userNameInput;
    [SerializeField] private Button submitButton;

    [SerializeField] private int minNameLength = 3;
    [SerializeField] private int maxNameLength = 15;

    [Header("Loading")] [SerializeField] private GameObject loadingImage;
    [SerializeField] private GameObject leaderBoardNameBox;

    [Header("Personal Entry")] [SerializeField]
    private TextMeshProUGUI personalRank;

    [SerializeField] private TextMeshProUGUI personalScore;

    [SerializeField] private UIAnimator uIAnimator;

    private void OnEnable()
    {
        uIAnimator.OnAnimateFinished.AddListener(OnSubmit);
    }

    private void OnDisable()
    {
        uIAnimator.OnAnimateFinished.RemoveListener(OnSubmit);
    }

    private void Start()
    {
        ScoreText.text = "Your Score : " + Event.Score;
        LeaderboardCreator.ResetPlayer();
    }

    private void OnSubmit() => SendLeaderBoardEntry(userNameInput.text, Event.Score);

    private void Update() => submitButton.interactable =
        userNameInput.text.Length > minNameLength && userNameInput.text.Length < maxNameLength;

    #region LEADER BOARD

    private const string PublicLeaderBoardKey = "b73c868d96e2d6f34643292011e55b954a74d7b0a90cc847c16b7a591ae411b4";

    private void GetLeaderBoard()
    {
        ToggleLoadingPanel(false);
        foreach (var playerHolder in scoreFields)
        {
            playerHolder.ClearText();
        }

        LeaderboardCreator.GetLeaderboard(PublicLeaderBoardKey, ((msg) =>
        {
            var loopLength = (msg.Length < scoreFields.Length) ? msg.Length : scoreFields.Length;
            for (var i = 0; i < loopLength; i++)
            {
                scoreFields[i].SetPlayerData(msg[i].Username, score: msg[i].Score, rank: (i + 1));
            }
        }));

        ToggleLoadingPanel(true);
    }

    private void ToggleLoadingPanel(bool state)
    {
        loadingImage.SetActive(!state);
        leaderBoardNameBox.SetActive(state);
    }

    private void SendLeaderBoardEntry(string username, int score)
    {
        LeaderboardCreator.UploadNewEntry(PublicLeaderBoardKey, username, score, ((msg) =>
        {
            GetLeaderBoard();
            GetPersonalEntry();
        }));
    }

    private void GetPersonalEntry() =>
        LeaderboardCreator.GetPersonalEntry(PublicLeaderBoardKey, OnPersonalEntryLoaded, ErrorCallback);

    private void ErrorCallback(string error) => Debug.LogError(error);


    private void OnPersonalEntryLoaded(Entry entry)
    {
        personalRank.text = $" Rank: {entry.RankSuffix()}";
        personalScore.text = $" Score: {entry.Score}";
    }

    #endregion
}