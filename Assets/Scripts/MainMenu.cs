using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
	public Button playButton;
	public int DefaultGameLengthMinutes = 5;
	public TextMeshProUGUI MinutesUi;
	public Color HighlightColor;
	public Color DefaultColor;

	private int minutes;
	private List<Button> buttons;
	private int selectedButton = 0;
	private float mouseAxis = Mathf.Infinity;
	private bool mouselessSelect;
	private bool shouldUpdate;

	void Start()
	{
		Screen.SetResolution(1024, 1024, true);
		if (playButton)
		    playButton.Select();
		
		GlobalHolder.GameLengthMinutes = DefaultGameLengthMinutes;
		minutes = DefaultGameLengthMinutes;
		changeGameLength(0);
		buttons = GetComponentsInChildren<Button>().ToList();
		shouldUpdate = true;
		selectButton(0);
	}

	void Update()
	{
		if (!shouldUpdate) return;

		var priorSelectedButton = selectedButton;

		if (isMouseActive() || Input.GetMouseButtonDown(0))
			disableMouselessSelect();

		if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
		{
			selectedButton--;
			mouselessSelect = true;
		}

		if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
		{
			selectedButton++;
			mouselessSelect = true;
		}

		if (!mouselessSelect) return;

		if (selectedButton < 0) selectedButton = buttons.Count - 1;
		if (selectedButton >= buttons.Count) selectedButton = 0;
		if (priorSelectedButton != selectedButton) selectButton(selectedButton);

		if (Input.GetKeyDown(KeyCode.E))
			buttons[selectedButton].onClick.Invoke();

		if (selectedButton == 2)
		{
			if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
				changeGameLength(-1);
			if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
				changeGameLength(1);
		}
	}

	public void MoveToGame()
	{
		shouldUpdate = false;
		SceneManager.LoadScene("InstructScreen");
	}

	public void MoveToCredits()
	{
		shouldUpdate = false;
		SceneManager.LoadScene("Credits");
	}

	public void ExitGame()
	{
		shouldUpdate = false;
		Application.Quit();
	}

	public void IncrementGameLength()
	{
		changeGameLength(1);
	}

	private void changeGameLength(int amount)
	{
		minutes += amount;
		if (minutes > 9) minutes = 1;
		if (minutes < 1) minutes = 9;
		MinutesUi.text = minutes.ToString();
		GlobalHolder.GameLengthMinutes = minutes;
	}

	private void selectButton(int idx)
	{
		Debug.Log($"Selected button {idx}");
		for (int i = 0; i < buttons.Count; i++)
		{
			var cb = buttons[i].colors;
			if (i == idx)
				cb.normalColor = HighlightColor;
			else
				cb.normalColor = DefaultColor;

			buttons[i].colors = cb;
		}
	}

	private void enableMouselessSelect()
	{
		if (!mouselessSelect)
			selectButton(selectedButton);
		mouselessSelect = true;
	}

	private void disableMouselessSelect()
	{
		if (mouselessSelect)
			selectButton(-1);
		mouselessSelect = false;
	}

	private bool isMouseActive()
	{
		var priorMouse = mouseAxis;
		mouseAxis = Input.GetAxis("Mouse X");
		if (priorMouse is Mathf.Infinity)
			return false;
		return priorMouse != mouseAxis;
	}
}
