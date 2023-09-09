public class LevelPortalCell : MenuCell
{
    private int level_;
    public LevelPortalCell SetLevel(int _level)
    {
        level_ = _level;
        return this;
    }
    protected override void OnPlayerMove(PlayerMovement.MergeResult _result)
    {
        base.OnPlayerMove(_result);
        // unlock next level
        if (_result.targetTransform == numberCell.transform)
        {
            LevelSaveHandler.SaveLevel(level_ + 1);
        }
    }
}