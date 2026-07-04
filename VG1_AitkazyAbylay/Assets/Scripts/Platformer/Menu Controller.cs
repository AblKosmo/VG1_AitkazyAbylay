using UnityEngine;
using UnityEngine.SceneManagement;


namespace Platformer 
{
    public class MenuController : MonoBehaviour 
    {
        public static MenuController instance;

        // Outlets
        public GameObject mainMenu;
        public GameObject optionsMenu;
        public GameObject levelMenu;

        void Awake() 
        {
            instance = this;
        }

		void Start() {
			Hide();
		}
        public void Show() 
        {
            ShowMainMenu();
            gameObject.SetActive(true);
			Time.timeScale = 0;
			PlayerController.instance.isPaused = true;
        }

        public void Hide() 
        {
            gameObject.SetActive(false);
			Time.timeScale = 1;
			PlayerController.instance.isPaused = false;
        }

        void SwitchMenu(GameObject targetMenu) 
        {
            // Cleanup Menus
            mainMenu.SetActive(false);
            optionsMenu.SetActive(false);
            levelMenu.SetActive(false);

            // Turn on target menu
            targetMenu.SetActive(true);
        }

        public void ShowMainMenu() 
        {
            SwitchMenu(mainMenu);
        }

        public void ShowOptionsMenu() 
        {
            SwitchMenu(optionsMenu);
        }

        public void ShowLevelMenu() 
        {
            SwitchMenu(levelMenu);
        }

		public void LoadLevel(string sceneName) {
			SceneManager.LoadScene(sceneName);
		}
    }
}