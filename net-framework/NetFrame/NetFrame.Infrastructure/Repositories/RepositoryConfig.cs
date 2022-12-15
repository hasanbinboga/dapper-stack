using Dapper;
using Dapper.FluentMap;
using NetFrame.Core.Entities;
using NetFrame.Infrastructure.Repositories;
using NetFrame.Infrastructure.Repositories;
using NetFrame.Infrasturcture.TypeWorks.EntityMappings;
using NetFrame.Infrasturcture.TypeWorks.TypeHandlers;

namespace NetFrame.Infrastructure.Repositories
{
    /// <summary>
    /// A static class that can be called FROM anywhere to enable register operations on the unitofwork of customized repository classes. If it is desired to use customized repository in UGA, the Configure function should be called once to the prepared console or web project.
    /// </summary>
    public static class RepositoryConfig
    {
        /// <summary>
        /// It handles the registration of customized repository classes on the unitofwork.
        /// </summary>
        /// <param name="unitOfWork">UnitofWork instance </param>
        public static void Configure(UnitOfWork unitOfWork)
        {
            SqlMapper.AddTypeHandler(new InetTypeHandler());
            SqlMapper.AddTypeHandler(new HistoryTypeHandler());


            unitOfWork.Repositories.Add(typeof(AuditEntity), new AuditRepository(unitOfWork));
            unitOfWork.Repositories.Add(typeof(ExceptionEntity), new ExceptionRepository(unitOfWork));
            unitOfWork.Repositories.Add(typeof(InfoLogEntity), new InfoLogRepository(unitOfWork));
            unitOfWork.Repositories.Add(typeof(ErrorLogEntity), new ErrorLogRepository(unitOfWork));
            unitOfWork.Repositories.Add(typeof(MessageEntity), new MessageRepository(unitOfWork));
            unitOfWork.Repositories.Add(typeof(NotificationEntity), new NotificationRepository(unitOfWork));
            unitOfWork.Repositories.Add(typeof(TaskEntity), new TaskRepository(unitOfWork));
            unitOfWork.Repositories.Add(typeof(TaskActionEntity), new TaskActionRepository(unitOfWork)); 
            unitOfWork.Repositories.Add(typeof(RegionEntity), new RegionRepository(unitOfWork)); 
            unitOfWork.Repositories.Add(typeof(CityEntity), new CityRepository(unitOfWork)); 

        }

        /// <summary>
        /// Required startup processes for key components. This method needs to be called once during the initialize phase of the application.
        /// </summary>
        public static void InitializeMappings()
        {
            FluentMapper.Initialize(config =>
            {
                config.AddMap(new TaskEntityMap());
                config.AddMap(new BaseGeomEntityMap());
            });

        }
    }
}
