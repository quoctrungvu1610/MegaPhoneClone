using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.View;

public class Level1Controller : Controller
{
    public const string LEVEL1_SCENE_NAME = "Level1";

    public override string SceneName()
    {
        return LEVEL1_SCENE_NAME;
    }
}