using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SlotManager))]
public class FakeInputs : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SlotManager slotManager = (SlotManager)target;

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        if (GUILayout.Button("Send PINK to ALPHA"))
        {
            slotManager.SendFakeInput(SlotName.ALPHA,PlayerName.CASTOR,CardSymbol.PINK);
        }
        if (GUILayout.Button("Send BLUE to ALPHA"))
        {
            slotManager.SendFakeInput(SlotName.ALPHA, PlayerName.CASTOR, CardSymbol.BLUE);
        }
        if (GUILayout.Button("Send ORANGE to ALPHA"))
        {
            slotManager.SendFakeInput(SlotName.ALPHA, PlayerName.CASTOR, CardSymbol.ORANGE);
        }

        EditorGUILayout.Space();

        if (GUILayout.Button("Send PINK to BETA"))
        {
            slotManager.SendFakeInput(SlotName.BETA, PlayerName.CASTOR, CardSymbol.PINK);
        }
        if (GUILayout.Button("Send BLUE to BETA"))
        {
            slotManager.SendFakeInput(SlotName.BETA, PlayerName.CASTOR, CardSymbol.BLUE);
        }
        if (GUILayout.Button("Send ORANGE to BETA"))
        {
            slotManager.SendFakeInput(SlotName.BETA, PlayerName.CASTOR, CardSymbol.ORANGE);
        }

        EditorGUILayout.Space();

        if (GUILayout.Button("Send PINK to GAMMA"))
        {
            slotManager.SendFakeInput(SlotName.GAMMA, PlayerName.CASTOR, CardSymbol.PINK);
        }
        if (GUILayout.Button("Send BLUE to GAMMA"))
        {
            slotManager.SendFakeInput(SlotName.GAMMA, PlayerName.CASTOR, CardSymbol.BLUE);
        }
        if (GUILayout.Button("Send ORANGE to GAMMA"))
        {
            slotManager.SendFakeInput(SlotName.GAMMA, PlayerName.CASTOR, CardSymbol.ORANGE);
        }

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        if (GUILayout.Button("Send PINK to DELTA"))
        {
            slotManager.SendFakeInput(SlotName.DELTA, PlayerName.POLLUX, CardSymbol.PINK);
        }
        if (GUILayout.Button("Send BLUE to DELTA"))
        {
            slotManager.SendFakeInput(SlotName.DELTA, PlayerName.POLLUX, CardSymbol.BLUE);
        }
        if (GUILayout.Button("Send ORANGE to DELTA"))
        {
            slotManager.SendFakeInput(SlotName.DELTA, PlayerName.POLLUX, CardSymbol.ORANGE);
        }

        EditorGUILayout.Space();

        if (GUILayout.Button("Send PINK to EPSILON"))
        {
            slotManager.SendFakeInput(SlotName.EPSILON, PlayerName.POLLUX, CardSymbol.PINK);
        }
        if (GUILayout.Button("Send BLUE to EPSILON"))
        {
            slotManager.SendFakeInput(SlotName.EPSILON, PlayerName.POLLUX, CardSymbol.BLUE);
        }
        if (GUILayout.Button("Send ORANGE to EPSILON"))
        {
            slotManager.SendFakeInput(SlotName.EPSILON, PlayerName.POLLUX, CardSymbol.ORANGE);
        }

        EditorGUILayout.Space();

        if (GUILayout.Button("Send PINK to DZETA"))
        {
            slotManager.SendFakeInput(SlotName.DZETA, PlayerName.POLLUX, CardSymbol.PINK);
        }
        if (GUILayout.Button("Send BLUE to DZETA"))
        {
            slotManager.SendFakeInput(SlotName.DZETA, PlayerName.POLLUX, CardSymbol.BLUE);
        }
        if (GUILayout.Button("Send ORANGE to DZETA"))
        {
            slotManager.SendFakeInput(SlotName.DZETA, PlayerName.POLLUX, CardSymbol.ORANGE);
        }
    }
}
