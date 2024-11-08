using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextFade : MonoBehaviour
{
    public float fadeDuration; //Duration for each character to fade in/out
    public float delayBetweenChars; //Delay between each character's fade-in/fade-out start
    public float fadeOutDelay; //Time to wait before starting the fade-out

    private TMP_Text textMeshPro;
    
    private void Awake()
    {
        textMeshPro = GetComponent<TMP_Text>();
        textMeshPro.ForceMeshUpdate(); //Ensures the text is up-to-date
    }

    private void OnEnable()
    {
        StartCoroutine(FadeInText());
    }

    private IEnumerator FadeInText()
    {
        textMeshPro.ForceMeshUpdate();
        var textInfo = textMeshPro.textInfo;

        // Loop through each character in the text for fade-in
        for (int i = 0; i < textInfo.characterCount; i++)
        {
            if (textInfo.characterInfo[i].isVisible)
            {
                StartCoroutine(FadeCharacterIn(i));
                yield return new WaitForSeconds(delayBetweenChars);
            }
        }

        // Wait for the fade-out delay before starting the fade-out
        yield return new WaitForSeconds(fadeOutDelay);
        StartCoroutine(FadeOutText());
    }

    private IEnumerator FadeCharacterIn(int charIndex)
    {
        var textInfo = textMeshPro.textInfo;
        float elapsed = 0;

        // Gradually change alpha from 0 to 1 for fade-in
        while (elapsed < fadeDuration)
        {
            float alpha = Mathf.Lerp(0, 1, elapsed / fadeDuration);

            Color32[] vertexColors = textMeshPro.textInfo.meshInfo[textInfo.characterInfo[charIndex].materialReferenceIndex].colors32;
            int vertexIndex = textInfo.characterInfo[charIndex].vertexIndex;

            vertexColors[vertexIndex + 0].a = (byte)(alpha * 255);
            vertexColors[vertexIndex + 1].a = (byte)(alpha * 255);
            vertexColors[vertexIndex + 2].a = (byte)(alpha * 255);
            vertexColors[vertexIndex + 3].a = (byte)(alpha * 255);

            textMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

            elapsed += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator FadeOutText()
    {
        var textInfo = textMeshPro.textInfo;

        // Loop through each character in the text for fade-out
        for (int i = 0; i < textInfo.characterCount; i++)
        {
            if (textInfo.characterInfo[i].isVisible)
            {
                StartCoroutine(FadeCharacterOut(i));
                yield return new WaitForSeconds(delayBetweenChars);
            }
        }
    }

    private IEnumerator FadeCharacterOut(int charIndex)
    {
        var textInfo = textMeshPro.textInfo;
        float elapsed = 0;

        // Ensure character is within range and visible
        if (charIndex < 0 || charIndex >= textInfo.characterCount || !textInfo.characterInfo[charIndex].isVisible)
        {
            yield break;
        }

        int materialIndex = textInfo.characterInfo[charIndex].materialReferenceIndex;
        int vertexIndex = textInfo.characterInfo[charIndex].vertexIndex;

        Color32[] vertexColors = textInfo.meshInfo[materialIndex].colors32;

        // Loop to gradually fade out the character
        while (elapsed < fadeDuration)
        {
            float alpha = Mathf.Lerp(1, 0, elapsed / fadeDuration);

            // Apply alpha to all vertices of the character
            for (int i = 0; i < 4; i++)
            {
                vertexColors[vertexIndex + i].a = (byte)(alpha * 255);
            }

            textMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure the character is fully transparent at the end
        for (int i = 0; i < 4; i++)
        {
            vertexColors[vertexIndex + i].a = 0;
        }
        textMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
    }
}
