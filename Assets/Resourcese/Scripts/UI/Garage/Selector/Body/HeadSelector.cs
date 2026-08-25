public class HeadSelector : BodyPartSelector
{
    public override void SetBodyLoadoutData(PartDataBase data)
    {
        LoadoutData.HeadPartData = data as HeadPartData;
        _statusUI.UpdateUI();
    }
}
