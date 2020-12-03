using AspNetCore.Identity.MongoDbCore.Models;
using System;

namespace HealthStore.Models.Identity
{
    public class ApplicationRole : MongoIdentityRole<Guid>
    {
		public ApplicationRole() : base()
		{

		}

		public ApplicationRole(string roleName) : base(roleName)
		{

		}
	}
}
