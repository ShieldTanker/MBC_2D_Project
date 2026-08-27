public class BodySelector : BodyPartSelector
{
    public override void SetBodyLoadoutData(PartDataBase data)
    {
        LoadoutData.BodyPartData = data as BodyPartData;
        _statusUI.UpdateUI();
    }
}
