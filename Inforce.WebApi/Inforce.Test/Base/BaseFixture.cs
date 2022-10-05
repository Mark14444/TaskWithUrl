using AutoFixture;
using AutoMapper;
using Inforce.Service.Profiles;
using System.Collections.Generic;

namespace Fragments.Test.Base
{
    public class BaseFixture
    {
        protected Fixture Fixture { get; set; }

        protected IMapper Mapper { get; set; }

        public BaseFixture()
        {
            Fixture = new Fixture();
            Fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            Fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            Mapper = new Mapper(new MapperConfiguration(options =>
                options.AddProfiles(new List<Profile>
                    {
                        new UserProfile(),
                        new UrlProfile()
                    })));
        }
    }
}