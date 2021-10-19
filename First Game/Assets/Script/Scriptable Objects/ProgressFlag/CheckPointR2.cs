using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "R2CP", menuName = "ProgressCheckPoint/Route2")]
public class CheckPointR2 : ScriptableObject
{
    public bool helpYellow;
    public bool openGate;
    public bool gateLogR1Move;
    public bool gateLogR2Talked;
    public bool gateLogR2LOpenGate;
    public bool getPureArrow;
    public bool r2LGateClose;
    public bool gateLogR2Move;
    public bool bossPurified;
    public bool getApple;
    public bool imoutoRecover;
    public bool attackedByRed;
    public bool findOnii;
    public bool killRed;
    public bool lastCustScene;
    public bool endGood;
}
