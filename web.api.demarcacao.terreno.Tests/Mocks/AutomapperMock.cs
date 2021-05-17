using AutoMapper;
using web.api.demarcacao.terreno.Endpoint.Models.Automapper;
using web.api.demarcacao.terreno.Service.Automapper;

namespace web.api.demarcacao.terreno.Tests.Mocks
{
    public class AutomapperMock
    {
        public static IMapper GetProfilesMapper()
        {
            return new MapperConfiguration(setup =>
            {
                setup.AddProfile(new ModelToRequestProfile());
                setup.AddProfile(new RequestToEntityProfile());
            }).CreateMapper();
        }
    }
}
