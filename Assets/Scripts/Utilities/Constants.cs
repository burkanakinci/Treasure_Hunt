public class Constants
{
    public const string PLAYER_DATA = "PlayerData";
}
public class ObjectTags
{
    public const string GROUND = "Ground";
    public const string STONE = "Stone";
    public const string TREE = "Tree";
    public const string RADAR = "Radar";
}

public class PooledObjectTags
{
    public const string GROUND = "Ground";
    public const string STONE = "Stone";
    public const string TREE = "Tree";
    public const string TREASURE_GENERATOR = "TreasureGenerator";
    public const string UP_LOG = "UpLog";
    public const string SIDE_LOG = "SideLog";
    public const string OPPONENT= "Opponent";
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
    Miner = 6,
    Stone = 7,
    Tree = 8,
    Log = 9,
    Radar = 10,
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
    RadarNull=-1,
    RadarLevel1 = 0,
    RadarLevel2 = 1,
    RadarLevel3 = 2,
}

public enum TriggerType
{
    Enter,
    Exit,
    Stay,
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

public enum TreeType
{
    Green = 0,
    Orange = 1,
    Red = 2,
    Yellow = 3
}
public enum StoneType
{
    SingleStone = 0,
    HorizontalStone = 1,
    VerticalStone = 2,
}

public enum ActiveParents
{
    TreasureGenerator = 0,
    Tree = 1,
    Stone = 2,
    Ground = 3,
    Log = 4,
    Opponent=5,
}
