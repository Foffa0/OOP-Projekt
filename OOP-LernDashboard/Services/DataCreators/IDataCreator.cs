using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LernDashboard.Services.DataCreators
{
    internal interface IDataCreator<T>
    {
        Task CreateModel(T model);
        Task DeleteModel(T model);
    }
}
