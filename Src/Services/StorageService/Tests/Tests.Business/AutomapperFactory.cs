using AutoMapper;
using StorageService.Business;

namespace Tests.Business
{
    public static class AutomapperFactory
    {
        public static IMapper Get()
        {
            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new Mappings());
            });

            return mockMapper.CreateMapper();
        }
    }
}
