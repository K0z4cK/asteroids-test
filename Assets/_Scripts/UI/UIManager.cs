using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Top Info")]
    [SerializeField] private TMP_Text _scoreTMP;
    [SerializeField] private TMP_Text _livesTMP;

    [Header("Buttons")]
    [SerializeField] private ButtonHold _asselerateButton;
    [SerializeField] private Button _shootButton;

    [Header("Win Panel")]
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private Button _menuWinButton;

    [Header("Lose Panel")]
    [SerializeField] private GameObject _losePanel;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _menuLoseButton;

    [Header("Last Level Popup")]
    [SerializeField] private GameObject _lastLevelPopup;
    [SerializeField] private Button _menuPopupButton;

    public void Init(UnityAction accelerateStart, UnityAction accelerateStop, UnityAction shoot, UnityAction goToMenu, UnityAction restartLevel, UnityAction nextLevel)
    {
        _asselerateButton.OnPressStart += accelerateStart;
        _asselerateButton.OnPressStop += accelerateStop;
        _shootButton.onClick.AddListener(shoot);

        _menuWinButton.onClick.AddListener(goToMenu);
        _nextLevelButton.onClick.AddListener(nextLevel);

        _restartButton.onClick.AddListener(restartLevel);
        _menuLoseButton.onClick.AddListener(goToMenu);

        _menuPopupButton.onClick.AddListener(goToMenu);
    }

    public void UpdateScore(int score) => _scoreTMP.text = $"{score} pts";

    public void UpdateLives(int lives) => _livesTMP.text = $"x{lives}";

    public void ShowWinPanel() => _winPanel.SetActive(true);

    public void ShowLosePanel() => _losePanel.SetActive(true);

    public void ShowLAstLevelPopup() => _lastLevelPopup.SetActive(true);
}
