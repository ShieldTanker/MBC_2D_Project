public class ArmsSelector : BodyPartSelector
{
    public override void SetBodyLoadoutData(PartDataBase data)
    {
        LoadoutData.ArmsPartData = data as ArmsPartData;
        _statusUI.UpdateUI();
    }
}
