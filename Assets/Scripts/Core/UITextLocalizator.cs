using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.UIElements;

public class UITextLocalizator : MonoBehaviour
{
    [SerializeField] string tableName = "GameText";
    private UIDocument uiDocument;
    private List<KeyValuePair<string, TextElement>> uiElements = new List<KeyValuePair<string, TextElement>>();

    void Start()
    {
        
        if (uiDocument == null)
            uiDocument = GetComponent<UIDocument>();

        GetUiTextElements();
        UpdateTexts(tableName);

        LocalizationSettings.SelectedLocaleChanged += ChangeLocale;
    }

    private void GetUiTextElements()
    {
        FindChildrenInHierarch(uiDocument.rootVisualElement);
    }

    private void ChangeLocale(Locale newLocale)
    {
        UpdateTexts(tableName);
    }

    void UpdateTexts(string table)
    {
        foreach (KeyValuePair<string, TextElement> entry in uiElements)
        {
            entry.Value.text =
                LocalizationSettings.StringDatabase.GetLocalizedString(table, entry.Key);
        }

    }
    void FindChildrenInHierarch(VisualElement element)
    {
        VisualElement.Hierarchy elementHierarchy = element.hierarchy;
        int numChildren = elementHierarchy.childCount;
        for (int i = 0; i < numChildren; i++)
        {
            VisualElement child = elementHierarchy.ElementAt(i);
            if (typeof(TextElement).IsInstanceOfType(child))
            {
                TextElement textElement = (TextElement)child;
                string key = textElement.text;
                if (!string.IsNullOrEmpty(key) && key[0] == '#')
                {
                    key = key.TrimStart('#');
                    var uiElemet = new KeyValuePair<string, TextElement>(key, textElement);
                    if(!uiElements.Contains(uiElemet)){
                        uiElements.Add(uiElemet);
                    }
                    
                }
            }
        }
        for (int i = 0; i < numChildren; i++)
        {
            VisualElement child = elementHierarchy.ElementAt(i);
            VisualElement.Hierarchy childHierarchy = child.hierarchy;
            int numGrandChildren = childHierarchy.childCount;
            if (numGrandChildren != 0)
                FindChildrenInHierarch(child);
        }
    }
}

