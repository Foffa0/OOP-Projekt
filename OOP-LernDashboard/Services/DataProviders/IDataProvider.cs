namespace OOP_LernDashboard.Services.DataProviders
{
    internal interface IDataProvider<T>
    {
        Task<IEnumerable<T>> GetAllModels();

    }
}
