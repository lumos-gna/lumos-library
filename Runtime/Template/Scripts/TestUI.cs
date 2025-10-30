using Lumos.DevPack;

public class TestUI : UIGlobal
{
    public override int ID => 0;
    public override void SetEnable(bool enable)
    {
        gameObject.SetActive(enable);
    }
}