using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [Header("Battle Menu")]
    [SerializeField] private PlayerData[] playableCharacters;
    [SerializeField] private GameObject menuPrefab;
    [SerializeField] private GameObject menusStorage;

    [Header("Player")]
    [SerializeField] private Controls controls;
    [SerializeField] private GameObject soul;

    private CharacterMenu[] menus = new CharacterMenu[3];

    void Start() {
        foreach (Transform child in menusStorage.transform) { Destroy(child.gameObject); }
        for (int i = 0; i < playableCharacters.Length; i++) {
            GameObject menuObject = Instantiate(menuPrefab, menusStorage.transform);
            CharacterMenu menu = menuObject.GetComponent<CharacterMenu>();
            menu.playerData = playableCharacters[i];
            menus[i] = menu;
        }
    }

    void Update() {
        
    }
}
