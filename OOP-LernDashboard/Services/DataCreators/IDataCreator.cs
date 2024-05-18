namespace OOP_LernDashboard.Services.DataCreators
{
    internal interface IDataCreator<T>
    {
        Task CreateModel(T model);
        Task DeleteModel(T model);
    }
}
