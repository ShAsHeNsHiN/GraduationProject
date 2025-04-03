using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestTyping : TextMeshProUGUI
{
    private void SetSingleCharaterAlpha(int index , byte newAlpha){
        TMP_CharacterInfo charInfo = textInfo.characterInfo[index];
        int matIndex = charInfo.materialReferenceIndex;
        int vertIndex = charInfo.vertexIndex;
        for(int i = 0 ; i < 4 ; i++){
            textInfo.meshInfo[matIndex].colors32[vertIndex + i ].a = newAlpha;
        }

        UpdateVertexData();
    }
}
