using AutoMapper;

namespace PublisherData.DataMapper
{
    public sealed class EntityMapper
    {
        private static Lazy<IMapper> iMapper = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cg =>
            {
                cg.AddProfile<PubDataMappProfile>();
            });

            var mapper = config.CreateMapper();
            return mapper;
        });

        public static IMapper Instance => iMapper.Value;

         EntityMapper()
        {
            
        }

    }
}
