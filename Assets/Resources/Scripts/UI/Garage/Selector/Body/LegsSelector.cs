public class LegsSelector : BodyPartSelector
{
    public override void SetBodyLoadoutData(PartDataBase data)
    {
        LoadoutData.LegsPartData = data as LegsPartData;
        _statusUI.UpdateUI();
    }
}