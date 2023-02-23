public class Constants
{
    public const string PLAYER_DATA = "PlayerData";
}
public class ObjectTags
{
    public const string GROUND = "Ground";
    public const string SINGLE_STONE = "StoneSingle";
    public const string HORIZONTAL_STONE = "StoneHorizontal";
    public const string VERTICAL_STONE = "StoneVertical";
    public const string GREEN_TREE = "TreeGreen";
    public const string YELLOW_TREE = "TreeYellow";
    public const string ORANGE_TREE = "TreeOrange";
    public const string RED_TREE = "TreeRed";
}

public class PooledObjectTags
{
}
public class UIAnimationStates
{
    public const string COUNTDOWN_ANIMATION = "Base Layer.CountDown";
}

public enum PlayerStates
{
}
public enum ObjectsLayer
{
    Default = 0,
}

public enum ListOperation
{
    Adding,
    Subtraction
}

public enum UIPanelType
{
    MainMenuPanel = 0,
    HudPanel = 1,
    FinishPanel = 2,
}

public enum RadarType
{
    RadarLevel1 = 0,
    RadarLevel2 = 1,
    RadarLevel3 = 2,
}
public enum PlayerMinerStates
{
    IdlePlayerMinerState = 0,
    RunPlayerMinerState = 1,
}
public enum OpponentMinerStates
{
    IdleOpponentMinerState = 0,
    RunOpponentMinerState = 1,
}

public enum OpponentDifficulty
{
    Easy,
    Normal,
    Hard,
}
